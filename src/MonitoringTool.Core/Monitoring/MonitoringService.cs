using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MonitoringTool.Core.Alerts;
using MonitoringTool.Core.Collection;
using MonitoringTool.Core.Settings;

namespace MonitoringTool.Core.Monitoring;

/// <summary>
/// Polls one server on independent schedules (CPU/memory, processes, disks), evaluates alerts and
/// sends e-mails. Everything runs off the UI thread; results are pushed through
/// <see cref="IProgress{T}"/>, so a <see cref="Progress{T}"/> created on the UI thread receives them
/// on the UI thread.
/// </summary>
/// <remarks>
/// Each loop awaits its own collection before waiting for the next tick, so a slow server can never
/// cause overlapping requests to pile up (the main cause of the v1 freezes and WinRM shell exhaustion).
/// </remarks>
public sealed class MonitoringService(
    IMetricsCollectorFactory collectorFactory,
    IEmailSender emailSender,
    TimeProvider timeProvider,
    ILogger<MonitoringService>? logger = null) : IAsyncDisposable
{
    private static readonly TimeSpan ConnectTimeout = TimeSpan.FromSeconds(60);

    private readonly ILogger logger = logger ?? NullLogger<MonitoringService>.Instance;
    private readonly AlertEvaluator alertEvaluator = new(timeProvider);
    private readonly Lock alertGate = new();
    private readonly Lock statusGate = new();
    private readonly HashSet<string> failingAreas = [];
    private readonly SemaphoreSlim lifecycle = new(1, 1);

    private volatile AppSettings settings = new();
    private IMetricsCollector? collector;
    private CancellationTokenSource? runCts;
    private Task? runTask;
    private IProgress<MonitoringEvent>? progress;

    public string? Server { get; private set; }

    public bool IsRunning => this.runTask is { IsCompleted: false };

    /// <summary>Applies new thresholds/intervals to a running session without restarting it.</summary>
    public void UpdateSettings(AppSettings newSettings) => this.settings = newSettings ?? throw new ArgumentNullException(nameof(newSettings));

    /// <summary>Connects and starts polling. Throws if the server cannot be reached.</summary>
    public async Task StartAsync(string server, AppSettings startSettings, IProgress<MonitoringEvent> sink, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(server);
        ArgumentNullException.ThrowIfNull(startSettings);
        ArgumentNullException.ThrowIfNull(sink);

        await this.lifecycle.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            if (this.IsRunning)
            {
                throw new InvalidOperationException("Monitoring is already running. Stop it first.");
            }

            this.settings = startSettings;
            this.progress = sink;
            this.Server = server.Trim();
            this.failingAreas.Clear();
            this.alertEvaluator.Reset();

            sink.Report(new StatusChanged(MonitoringState.Connecting, $"Connecting to {this.Server}…"));
            var newCollector = collectorFactory.Create(this.Server, TimeSpan.FromSeconds(startSettings.Polling.CommandTimeoutSeconds));
            try
            {
                using var connectCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                connectCts.CancelAfter(ConnectTimeout);
                await newCollector.ConnectAsync(connectCts.Token).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await newCollector.DisposeAsync().ConfigureAwait(false);
                this.logger.LogWarning(ex, "Could not connect to {Server}", this.Server);
                var message = ex is OperationCanceledException && !cancellationToken.IsCancellationRequested
                    ? $"Timed out connecting to {this.Server}."
                    : ex.Message;
                sink.Report(new StatusChanged(MonitoringState.Stopped, message));
                throw;
            }

            this.collector = newCollector;
            this.runCts = new CancellationTokenSource();
            var token = this.runCts.Token;
            var emails = Channel.CreateBounded<AlertNotification>(new BoundedChannelOptions(100)
            {
                SingleReader = true,
                FullMode = BoundedChannelFullMode.DropOldest,
            });

            sink.Report(new StatusChanged(MonitoringState.Running, $"Monitoring {this.Server}"));

            this.runTask = Task.WhenAll(
                Task.Run(() => this.RunLoopAsync("CPU/memory", s => s.Polling.SummaryIntervalSeconds, ct => this.CollectSummaryAsync(newCollector, emails.Writer, ct), token), CancellationToken.None),
                Task.Run(() => this.RunLoopAsync("Processes", s => s.Polling.ProcessesIntervalSeconds, ct => this.CollectProcessesAsync(newCollector, ct), token), CancellationToken.None),
                Task.Run(() => this.RunLoopAsync("Disks", s => s.Polling.DisksIntervalSeconds, ct => this.CollectDisksAsync(newCollector, emails.Writer, ct), token), CancellationToken.None),
                Task.Run(() => this.SendEmailsAsync(emails.Reader, token), CancellationToken.None));
        }
        finally
        {
            this.lifecycle.Release();
        }
    }

    public async Task StopAsync()
    {
        await this.lifecycle.WaitAsync().ConfigureAwait(false);
        try
        {
            if (this.runTask is null)
            {
                return;
            }

            this.progress?.Report(new StatusChanged(MonitoringState.Stopping, "Stopping…"));
            await this.runCts!.CancelAsync().ConfigureAwait(false);
            try
            {
                await this.runTask.ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Loops handle their own errors; this is only a safety net.
                this.logger.LogError(ex, "Monitoring loop ended with an error");
            }

            if (this.collector is not null)
            {
                await this.collector.DisposeAsync().ConfigureAwait(false);
            }

            this.runCts.Dispose();
            this.runCts = null;
            this.runTask = null;
            this.collector = null;
            this.progress?.Report(new StatusChanged(MonitoringState.Stopped, $"Stopped monitoring {this.Server}"));
        }
        finally
        {
            this.lifecycle.Release();
        }
    }

    public async ValueTask DisposeAsync()
    {
        await this.StopAsync().ConfigureAwait(false);
        this.lifecycle.Dispose();
    }

    private async Task RunLoopAsync(string area, Func<AppSettings, int> intervalSeconds, Func<CancellationToken, Task> collect, CancellationToken cancellationToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(intervalSeconds(this.settings)), timeProvider);
        try
        {
            do
            {
                try
                {
                    await collect(cancellationToken).ConfigureAwait(false);
                    this.MarkHealthy(area);
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                catch (Exception ex)
                {
                    this.logger.LogWarning(ex, "{Area} collection failed on {Server}", area, this.Server);
                    this.MarkFailing(area, ex);
                }

                // Picks up interval changes made in the settings dialog while running.
                var period = TimeSpan.FromSeconds(intervalSeconds(this.settings));
                if (timer.Period != period)
                {
                    timer.Period = period;
                }
            }
            while (await timer.WaitForNextTickAsync(cancellationToken).ConfigureAwait(false));
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            // Normal shutdown.
        }
    }

    private async Task CollectSummaryAsync(IMetricsCollector source, ChannelWriter<AlertNotification> emails, CancellationToken cancellationToken)
    {
        var summary = await source.GetSummaryAsync(cancellationToken).ConfigureAwait(false);
        this.progress?.Report(new SummaryUpdated(summary));

        IReadOnlyList<AlertNotification> alerts;
        lock (this.alertGate)
        {
            alerts = this.alertEvaluator.Evaluate(this.Server!, summary, this.settings.Alerts);
        }

        this.Dispatch(alerts, emails);
    }

    private async Task CollectProcessesAsync(IMetricsCollector source, CancellationToken cancellationToken)
    {
        var processes = await source.GetTopProcessesAsync(this.settings.Polling.TopProcessCount, cancellationToken).ConfigureAwait(false);
        this.progress?.Report(new ProcessesUpdated(processes));
    }

    private async Task CollectDisksAsync(IMetricsCollector source, ChannelWriter<AlertNotification> emails, CancellationToken cancellationToken)
    {
        var disks = await source.GetDisksAsync(cancellationToken).ConfigureAwait(false);
        this.progress?.Report(new DisksUpdated(disks));

        IReadOnlyList<AlertNotification> alerts;
        lock (this.alertGate)
        {
            alerts = this.alertEvaluator.Evaluate(this.Server!, disks, this.settings.Alerts);
        }

        this.Dispatch(alerts, emails);
    }

    private void Dispatch(IReadOnlyList<AlertNotification> alerts, ChannelWriter<AlertNotification> emails)
    {
        var inApp = alerts.Where(a => a.Channels.HasFlag(AlertChannels.InApp)).ToList();
        if (inApp.Count > 0)
        {
            this.progress?.Report(new AlertsRaised(inApp));
        }

        foreach (var alert in alerts.Where(a => a.Channels.HasFlag(AlertChannels.Email)))
        {
            emails.TryWrite(alert);
        }
    }

    /// <summary>Single consumer so a slow/unreachable SMTP server never delays metric collection.</summary>
    private async Task SendEmailsAsync(ChannelReader<AlertNotification> reader, CancellationToken cancellationToken)
    {
        try
        {
            await foreach (var alert in reader.ReadAllAsync(cancellationToken).ConfigureAwait(false))
            {
                var current = this.settings;
                if (!current.Alerts.EmailEnabled || !current.Smtp.IsConfigured)
                {
                    continue;
                }

                var (subject, body) = MailKitEmailSender.Format(alert);
                try
                {
                    await emailSender.SendAsync(current.Smtp, current.Alerts.RecipientList, subject, body, cancellationToken).ConfigureAwait(false);
                    this.progress?.Report(new EmailResult(alert, true, null));
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                catch (Exception ex)
                {
                    this.logger.LogWarning(ex, "Sending alert e-mail failed");
                    this.progress?.Report(new EmailResult(alert, false, ex.Message));
                }
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            // Normal shutdown; queued e-mails are dropped.
        }
    }

    private void MarkFailing(string area, Exception ex)
    {
        this.progress?.Report(new CollectionFailed(area, ex.Message, timeProvider.GetUtcNow()));
        lock (this.statusGate)
        {
            this.failingAreas.Add(area);
            this.progress?.Report(new StatusChanged(MonitoringState.Degraded, $"{area}: {ex.Message}"));
        }
    }

    private void MarkHealthy(string area)
    {
        lock (this.statusGate)
        {
            if (this.failingAreas.Remove(area) && this.failingAreas.Count == 0)
            {
                this.progress?.Report(new StatusChanged(MonitoringState.Running, $"Monitoring {this.Server}"));
            }
        }
    }
}
