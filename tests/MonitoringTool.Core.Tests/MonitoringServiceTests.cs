using System.Collections.Concurrent;
using Microsoft.Extensions.Time.Testing;
using MonitoringTool.Core.Alerts;
using MonitoringTool.Core.Collection;
using MonitoringTool.Core.Models;
using MonitoringTool.Core.Monitoring;
using MonitoringTool.Core.Settings;

namespace MonitoringTool.Core.Tests;

public sealed class MonitoringServiceTests : IAsyncDisposable
{
    private static readonly TimeSpan WaitTimeout = TimeSpan.FromSeconds(10);

    private readonly FakeTimeProvider time = new();
    private readonly FakeCollector collector = new();
    private readonly FakeEmailSender email = new();
    private readonly RecordingProgress progress = new();
    private readonly MonitoringService service;

    public MonitoringServiceTests() =>
        this.service = new MonitoringService(new FakeFactory(this.collector), this.email, this.time);

    public async ValueTask DisposeAsync() => await this.service.DisposeAsync();

    [Fact]
    public async Task Start_connects_reports_running_and_collects_everything_immediately()
    {
        await this.service.StartAsync("srv01", new AppSettings(), this.progress, TestContext.Current.CancellationToken);

        Assert.True(this.service.IsRunning);
        await WaitUntil(() => this.progress.Of<SummaryUpdated>().Any() && this.progress.Of<ProcessesUpdated>().Any() && this.progress.Of<DisksUpdated>().Any());
        Assert.Equal([MonitoringState.Connecting, MonitoringState.Running], this.progress.Of<StatusChanged>().Select(s => s.State).Take(2));
        Assert.Equal(1, this.collector.Connects);
    }

    [Fact]
    public async Task Polls_again_on_each_interval()
    {
        var settings = new AppSettings { Polling = new PollingSettings { SummaryIntervalSeconds = 10 } };
        await this.service.StartAsync("srv01", settings, this.progress, TestContext.Current.CancellationToken);
        await WaitUntil(() => this.collector.SummaryCalls == 1);

        this.time.Advance(TimeSpan.FromSeconds(10));
        await WaitUntil(() => this.collector.SummaryCalls == 2);

        this.time.Advance(TimeSpan.FromSeconds(10));
        await WaitUntil(() => this.collector.SummaryCalls == 3);
    }

    [Fact]
    public async Task Slow_server_never_causes_overlapping_requests()
    {
        // v1 started a new thread + remote session on every timer tick regardless of whether the
        // previous one had finished; with a 30 s CPU sample and a 9 s timer they piled up without bound.
        this.collector.SummaryGate = new TaskCompletionSource();
        await this.service.StartAsync("srv01", new AppSettings(), this.progress, TestContext.Current.CancellationToken);
        await WaitUntil(() => this.collector.SummaryCalls == 1);

        for (var i = 0; i < 20; i++)
        {
            this.time.Advance(TimeSpan.FromSeconds(10));
        }

        await Task.Delay(100, TestContext.Current.CancellationToken);
        Assert.Equal(1, this.collector.SummaryCalls);
        Assert.Equal(1, this.collector.MaxConcurrentSummaryCalls);

        // The 20 missed ticks collapse into a single catch-up poll (PeriodicTimer semantics), not 20 queued ones.
        this.collector.SummaryGate.SetResult();
        await WaitUntil(() => this.collector.SummaryCalls == 2);
        await Task.Delay(100, TestContext.Current.CancellationToken);
        Assert.Equal(2, this.collector.SummaryCalls);

        this.time.Advance(TimeSpan.FromSeconds(10));
        await WaitUntil(() => this.collector.SummaryCalls == 3);
        Assert.Equal(1, this.collector.MaxConcurrentSummaryCalls);
    }

    [Fact]
    public async Task Failures_are_reported_and_polling_recovers()
    {
        this.collector.FailDisks = true;
        await this.service.StartAsync("srv01", new AppSettings(), this.progress, TestContext.Current.CancellationToken);

        await WaitUntil(() => this.progress.Of<CollectionFailed>().Any(f => f.Area == "Disks"));
        Assert.Contains(this.progress.Of<StatusChanged>(), s => s.State == MonitoringState.Degraded);

        this.collector.FailDisks = false;
        this.time.Advance(TimeSpan.FromSeconds(new PollingSettings().DisksIntervalSeconds));

        await WaitUntil(() => this.progress.Of<DisksUpdated>().Any());
        await WaitUntil(() => this.progress.Of<StatusChanged>().Last().State == MonitoringState.Running);
        Assert.True(this.service.IsRunning);
    }

    [Fact]
    public async Task Connect_failure_throws_reports_stopped_and_disposes_collector()
    {
        this.collector.FailConnect = true;

        await Assert.ThrowsAsync<InvalidOperationException>(() => this.service.StartAsync("srv01", new AppSettings(), this.progress, TestContext.Current.CancellationToken));

        Assert.False(this.service.IsRunning);
        Assert.Equal(MonitoringState.Stopped, this.progress.Of<StatusChanged>().Last().State);
        Assert.True(this.collector.Disposed);
    }

    [Fact]
    public async Task Stop_cancels_in_flight_requests_promptly()
    {
        this.collector.SummaryGate = new TaskCompletionSource(); // never completes on its own
        await this.service.StartAsync("srv01", new AppSettings(), this.progress, TestContext.Current.CancellationToken);
        await WaitUntil(() => this.collector.SummaryCalls == 1);

        await this.service.StopAsync().WaitAsync(WaitTimeout, TestContext.Current.CancellationToken);

        Assert.False(this.service.IsRunning);
        Assert.True(this.collector.Disposed);
        Assert.Equal(MonitoringState.Stopped, this.progress.Of<StatusChanged>().Last().State);
    }

    [Fact]
    public async Task Breached_threshold_raises_in_app_alert_and_sends_email()
    {
        this.collector.Cpu = 99;
        var settings = new AppSettings
        {
            Alerts = new AlertSettings { CpuThresholdPercent = 90, EmailEnabled = true, EmailRecipients = "ops@example.com" },
            Smtp = new SmtpSettings { Host = "smtp.example.com", UserName = "alerts@example.com" },
        };

        await this.service.StartAsync("srv01", settings, this.progress, TestContext.Current.CancellationToken);

        await WaitUntil(() => this.progress.Of<AlertsRaised>().Any());
        await WaitUntil(() => this.email.Sent.Count == 1);
        var alert = Assert.Single(this.progress.Of<AlertsRaised>().SelectMany(a => a.Alerts));
        Assert.Equal("cpu", alert.Key);
        Assert.Contains("[ALERT] srv01", this.email.Sent.Single().Subject, StringComparison.Ordinal);
        Assert.True(Assert.Single(this.progress.Of<EmailResult>()).Succeeded);
    }

    [Fact]
    public async Task Settings_updates_apply_without_restart()
    {
        this.collector.Cpu = 70;
        await this.service.StartAsync("srv01", new AppSettings(), this.progress, TestContext.Current.CancellationToken);
        await WaitUntil(() => this.collector.SummaryCalls == 1);
        Assert.Empty(this.progress.Of<AlertsRaised>());

        this.service.UpdateSettings(new AppSettings { Alerts = new AlertSettings { CpuThresholdPercent = 60 } });
        this.time.Advance(TimeSpan.FromSeconds(new PollingSettings().SummaryIntervalSeconds));

        await WaitUntil(() => this.progress.Of<AlertsRaised>().Any());
    }

    [Fact]
    public async Task Cannot_start_twice()
    {
        await this.service.StartAsync("srv01", new AppSettings(), this.progress, TestContext.Current.CancellationToken);
        await Assert.ThrowsAsync<InvalidOperationException>(() => this.service.StartAsync("srv02", new AppSettings(), this.progress, TestContext.Current.CancellationToken));
    }

    private static async Task WaitUntil(Func<bool> condition)
    {
        var deadline = DateTime.UtcNow + WaitTimeout;
        while (!condition())
        {
            if (DateTime.UtcNow > deadline)
            {
                throw new TimeoutException("Condition was not met in time.");
            }

            await Task.Delay(10);
        }
    }

    private sealed class RecordingProgress : IProgress<MonitoringEvent>
    {
        private readonly ConcurrentQueue<MonitoringEvent> events = new();

        public void Report(MonitoringEvent value) => this.events.Enqueue(value);

        public IReadOnlyList<T> Of<T>() => this.events.OfType<T>().ToList();
    }

    private sealed class FakeFactory(IMetricsCollector collector) : IMetricsCollectorFactory
    {
        public IMetricsCollector Create(string target, TimeSpan commandTimeout) => collector;
    }

    private sealed class FakeEmailSender : IEmailSender
    {
        public ConcurrentBag<(string Subject, string Body)> Sent { get; } = [];

        public Task SendAsync(SmtpSettings smtp, IReadOnlyList<string> recipients, string subject, string body, CancellationToken cancellationToken)
        {
            this.Sent.Add((subject, body));
            return Task.CompletedTask;
        }
    }

    private sealed class FakeCollector : IMetricsCollector
    {
        private int summaryCalls;
        private int inFlightSummary;
        private int maxInFlightSummary;

        public string Target => "srv01";

        public int Connects { get; private set; }

        public bool FailConnect { get; set; }

        public volatile bool FailDisks;

        public double Cpu { get; set; } = 10;

        public TaskCompletionSource? SummaryGate { get; set; }

        public int SummaryCalls => Volatile.Read(ref this.summaryCalls);

        public int MaxConcurrentSummaryCalls => Volatile.Read(ref this.maxInFlightSummary);

        public bool Disposed { get; private set; }

        public Task ConnectAsync(CancellationToken cancellationToken)
        {
            this.Connects++;
            return this.FailConnect ? Task.FromException(new InvalidOperationException("WinRM cannot complete the operation")) : Task.CompletedTask;
        }

        public async Task<SystemSummary> GetSummaryAsync(CancellationToken cancellationToken)
        {
            var now = Interlocked.Increment(ref this.inFlightSummary);
            InterlockedMax(ref this.maxInFlightSummary, now);
            Interlocked.Increment(ref this.summaryCalls);
            try
            {
                if (this.SummaryGate is { } gate)
                {
                    await gate.Task.WaitAsync(cancellationToken);
                }

                return new SystemSummary("SRV01", this.Cpu, 16, 8, TimeSpan.FromDays(3), DateTimeOffset.UtcNow);
            }
            finally
            {
                Interlocked.Decrement(ref this.inFlightSummary);
            }
        }

        public Task<ProcessesSnapshot> GetTopProcessesAsync(int count, CancellationToken cancellationToken) =>
            Task.FromResult(new ProcessesSnapshot([new ProcessUsage(4, "System", 1, 0)], [new ProcessUsage(4, "System", 0, 100)], DateTimeOffset.UtcNow));

        public Task<DisksSnapshot> GetDisksAsync(CancellationToken cancellationToken) => this.FailDisks
            ? Task.FromException<DisksSnapshot>(new InvalidOperationException("Access denied"))
            : Task.FromResult(new DisksSnapshot([new DiskUsage("C:", "OS", 100, 50)], DateTimeOffset.UtcNow));

        public ValueTask DisposeAsync()
        {
            this.Disposed = true;
            return ValueTask.CompletedTask;
        }

        private static void InterlockedMax(ref int target, int value)
        {
            int current;
            while (value > (current = Volatile.Read(ref target)) && Interlocked.CompareExchange(ref target, value, current) != current)
            {
            }
        }
    }
}
