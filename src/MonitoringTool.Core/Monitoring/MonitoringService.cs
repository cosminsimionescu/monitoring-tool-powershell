using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MonitoringTool.Core.Alerts;
using MonitoringTool.Core.Checks;
using MonitoringTool.Core.Collection;
using MonitoringTool.Core.Settings;

namespace MonitoringTool.Core.Monitoring;

/// <summary>
/// Orchestrates everything that runs against one server. Each unit of work is a <em>job</em> with its
/// own schedule: the three built-in metric jobs (CPU/memory, processes, disks) and one job per custom
/// check script. The orchestrator owns scheduling, concurrency limits, timeouts, health tracking,
/// alert evaluation and e-mail dispatch; jobs only know how to collect.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>A job awaits its own run before waiting for its next tick, so a job never overlaps itself and
/// a slow server cannot make requests pile up. Missed ticks collapse into one catch-up run.</item>
/// <item>Custom checks share a concurrency limit (<see cref="ChecksSettings.MaxConcurrentChecks"/>), so a
/// large check library cannot starve the built-in metrics of remote capacity.</item>
/// <item>Everything runs on the thread pool; results go out through <see cref="IProgress{T}"/>, so a
/// <see cref="Progress{T}"/> created on the UI thread receives them on the UI thread.</item>
/// </list>
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
    private CheckJobs? checkJobs;
    private ChannelWriter<AlertNotification>? emailWriter;
    private IProgress<MonitoringEvent>? progress;

    public string? Server { get; private set; }

    public bool IsRunning => this.runTask is { IsCompleted: false };

    /// <summary>The checks currently scheduled (empty when stopped).</summary>
    public IReadOnlyList<CheckDefinition> Checks => this.checkJobs?.Definitions ?? [];

    /// <summary>Applies new thresholds/intervals/enabled checks to a running session without restarting it.</summary>
    public void UpdateSettings(AppSettings newSettings) => this.settings = newSettings ?? throw new ArgumentNullException(nameof(newSettings));

    /// <summary>Connects and starts all jobs. Throws if the server cannot be reached.</summary>
    public async Task StartAsync(
        string server,
        AppSettings startSettings,
        IReadOnlyList<CheckDefinition> checks,
        IProgress<MonitoringEvent> sink,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(server);
        ArgumentNullException.ThrowIfNull(startSettings);
        ArgumentNullException.ThrowIfNull(checks);
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
            this.emailWriter = emails.Writer;

            sink.Report(new StatusChanged(MonitoringState.Running, $"Monitoring {this.Server}"));

            this.runTask = Task.WhenAll(
                Task.Run(() => this.RunJobAsync("CPU/memory", s => s.Polling.SummaryIntervalSeconds, ct => this.CollectSummaryAsync(newCollector, ct), trackHealth: true, token), CancellationToken.None),
                Task.Run(() => this.RunJobAsync("Processes", s => s.Polling.ProcessesIntervalSeconds, ct => this.CollectProcessesAsync(newCollector, ct), trackHealth: true, token), CancellationToken.None),
                Task.Run(() => this.RunJobAsync("Disks", s => s.Polling.DisksIntervalSeconds, ct => this.CollectDisksAsync(newCollector, ct), trackHealth: true, token), CancellationToken.None),
                Task.Run(() => this.SendEmailsAsync(emails.Reader, token), CancellationToken.None));

            this.checkJobs = this.StartCheckJobs(checks, newCollector, token);
        }
        finally
        {
            this.lifecycle.Release();
        }
    }

    /// <summary>Replaces the scheduled checks (e.g. after scripts were edited) without reconnecting.</summary>
    public async Task ReloadChecksAsync(IReadOnlyList<CheckDefinition> checks)
    {
        ArgumentNullException.ThrowIfNull(checks);

        await this.lifecycle.WaitAsync().ConfigureAwait(false);
        try
        {
            if (!this.IsRunning || this.collector is null || this.runCts is null)
            {
                return;
            }

            if (this.checkJobs is not null)
            {
                await this.checkJobs.StopAsync().ConfigureAwait(false);
            }

            lock (this.alertGate)
            {
                this.alertEvaluator.RetainChecks(checks.Select(c => c.Id));
            }

            this.checkJobs = this.StartCheckJobs(checks, this.collector, this.runCts.Token);
        }
        finally
        {
            this.lifecycle.Release();
        }
    }

    /// <summary>
    /// Runs one check immediately, outside its schedule (also when it is disabled, to test a script).
    /// Returns false when monitoring is stopped, the check is unknown, or it is already running.
    /// </summary>
    public Task<bool> RunCheckNowAsync(string checkId)
    {
        var jobs = this.checkJobs;
        var definition = jobs?.Definitions.FirstOrDefault(c => string.Equals(c.Id, checkId, StringComparison.OrdinalIgnoreCase));
        if (jobs is null || definition is null || jobs.Token.IsCancellationRequested)
        {
            return Task.FromResult(false);
        }

        return Task.Run(() => this.RunCheckOnceAsync(definition, jobs, manual: true, jobs.Token), CancellationToken.None);
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
                if (this.checkJobs is not null)
                {
                    await this.checkJobs.StopAsync().ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                // Jobs handle their own errors; this is only a safety net.
                this.logger.LogError(ex, "A monitoring job ended with an error");
            }

            if (this.collector is not null)
            {
                await this.collector.DisposeAsync().ConfigureAwait(false);
            }

            this.runCts.Dispose();
            this.runCts = null;
            this.runTask = null;
            this.checkJobs = null;
            this.collector = null;
            this.emailWriter = null;
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

    // ------------------------------------------------------------------ scheduling

    private async Task RunJobAsync(string area, Func<AppSettings, int> intervalSeconds, Func<CancellationToken, Task> work, bool trackHealth, CancellationToken cancellationToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(intervalSeconds(this.settings)), timeProvider);
        try
        {
            do
            {
                try
                {
                    await work(cancellationToken).ConfigureAwait(false);
                    if (trackHealth)
                    {
                        this.MarkHealthy(area);
                    }
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                catch (Exception ex)
                {
                    this.logger.LogWarning(ex, "{Area} failed on {Server}", area, this.Server);
                    if (trackHealth)
                    {
                        this.MarkFailing(area, ex);
                    }
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

    private CheckJobs StartCheckJobs(IReadOnlyList<CheckDefinition> checks, IMetricsCollector source, CancellationToken runToken)
    {
        var jobs = new CheckJobs(checks, source, this.settings.Checks.MaxConcurrentChecks, runToken);
        jobs.Task = Task.WhenAll(checks.Select(check => Task.Run(
            () => this.RunJobAsync("Check " + check.Id, _ => check.IntervalSeconds, ct => this.RunCheckOnceAsync(check, jobs, manual: false, ct), trackHealth: false, jobs.Token),
            CancellationToken.None)));
        return jobs;
    }

    /// <returns>False when the check was skipped because it is already running.</returns>
    private async Task<bool> RunCheckOnceAsync(CheckDefinition check, CheckJobs jobs, bool manual, CancellationToken cancellationToken)
    {
        var current = this.settings;
        var enabled = current.Checks.IsEnabled(check.Id);
        if (!enabled && !manual)
        {
            // Switched off: clear any alert it had raised, then stay idle.
            this.EvaluateCheck(new CheckResult(check.Id, check.Name, CheckStatus.Ok, "Disabled", null, timeProvider.GetUtcNow(), TimeSpan.Zero), alertsEnabled: false);
            return true;
        }

        var perCheck = jobs.Locks[check.Id];
        if (!await perCheck.WaitAsync(0, CancellationToken.None).ConfigureAwait(false))
        {
            return false;
        }

        try
        {
            await jobs.Concurrency.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                this.progress?.Report(new CheckStarted(check.Id));
                var started = timeProvider.GetTimestamp();
                CheckResult result;
                try
                {
                    var output = await jobs.Collector.RunCheckAsync(check, cancellationToken).ConfigureAwait(false);
                    var (status, message, value) = CheckOutputInterpreter.Interpret(output.Records, output.Errors);
                    result = new CheckResult(check.Id, check.Name, status, message, value, timeProvider.GetUtcNow(), timeProvider.GetElapsedTime(started));
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    var message = ex is TimeoutException ? $"Timed out after {check.TimeoutSeconds} s" : ex.Message;
                    result = new CheckResult(check.Id, check.Name, CheckStatus.Unknown, message, null, timeProvider.GetUtcNow(), timeProvider.GetElapsedTime(started));
                }

                this.progress?.Report(new CheckCompleted(result));
                this.EvaluateCheck(result, alertsEnabled: enabled && this.settings.Checks.AlertsEnabled);
                return true;
            }
            finally
            {
                jobs.Concurrency.Release();
            }
        }
        finally
        {
            perCheck.Release();
        }
    }

    // ------------------------------------------------------------------ built-in metric jobs

    private async Task CollectSummaryAsync(IMetricsCollector source, CancellationToken cancellationToken)
    {
        var summary = await source.GetSummaryAsync(cancellationToken).ConfigureAwait(false);
        this.progress?.Report(new SummaryUpdated(summary));

        IReadOnlyList<AlertNotification> alerts;
        lock (this.alertGate)
        {
            alerts = this.alertEvaluator.Evaluate(this.Server!, summary, this.settings.Alerts);
        }

        this.Dispatch(alerts);
    }

    private async Task CollectProcessesAsync(IMetricsCollector source, CancellationToken cancellationToken)
    {
        var processes = await source.GetTopProcessesAsync(this.settings.Polling.TopProcessCount, cancellationToken).ConfigureAwait(false);
        this.progress?.Report(new ProcessesUpdated(processes));
    }

    private async Task CollectDisksAsync(IMetricsCollector source, CancellationToken cancellationToken)
    {
        var disks = await source.GetDisksAsync(cancellationToken).ConfigureAwait(false);
        this.progress?.Report(new DisksUpdated(disks));

        IReadOnlyList<AlertNotification> alerts;
        lock (this.alertGate)
        {
            alerts = this.alertEvaluator.Evaluate(this.Server!, disks, this.settings.Alerts);
        }

        this.Dispatch(alerts);
    }

    // ------------------------------------------------------------------ alerts

    private void EvaluateCheck(CheckResult result, bool alertsEnabled)
    {
        IReadOnlyList<AlertNotification> alerts;
        lock (this.alertGate)
        {
            alerts = this.alertEvaluator.Evaluate(this.Server!, result, this.settings.Alerts, alertsEnabled);
        }

        this.Dispatch(alerts);
    }

    private void Dispatch(IReadOnlyList<AlertNotification> alerts)
    {
        var inApp = alerts.Where(a => a.Channels.HasFlag(AlertChannels.InApp)).ToList();
        if (inApp.Count > 0)
        {
            this.progress?.Report(new AlertsRaised(inApp));
        }

        foreach (var alert in alerts.Where(a => a.Channels.HasFlag(AlertChannels.Email)))
        {
            this.emailWriter?.TryWrite(alert);
        }
    }

    /// <summary>Single consumer so a slow/unreachable SMTP server never delays any job.</summary>
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

    // ------------------------------------------------------------------ health

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

    /// <summary>One generation of scheduled checks; replaced as a whole on reload.</summary>
    private sealed class CheckJobs
    {
        private readonly CancellationTokenSource cts;

        public CheckJobs(IReadOnlyList<CheckDefinition> definitions, IMetricsCollector collector, int maxConcurrent, CancellationToken runToken)
        {
            this.Definitions = definitions;
            this.Collector = collector;
            this.Concurrency = new SemaphoreSlim(Math.Max(1, maxConcurrent));
            this.Locks = definitions.ToDictionary(d => d.Id, _ => new SemaphoreSlim(1, 1), StringComparer.OrdinalIgnoreCase);
            this.cts = CancellationTokenSource.CreateLinkedTokenSource(runToken);
            this.Token = this.cts.Token;
        }

        public IReadOnlyList<CheckDefinition> Definitions { get; }

        public IMetricsCollector Collector { get; }

        public SemaphoreSlim Concurrency { get; }

        public Dictionary<string, SemaphoreSlim> Locks { get; }

        public CancellationToken Token { get; }

        public Task Task { get; set; } = Task.CompletedTask;

        public async Task StopAsync()
        {
            await this.cts.CancelAsync().ConfigureAwait(false);
            await this.Task.ConfigureAwait(false);
            this.cts.Dispose();

            // A manual run may still hold a lock at this point; SemaphoreSlim.Dispose only matters when
            // AvailableWaitHandle was used, which it never is here, so these are left to the GC.
        }
    }
}
