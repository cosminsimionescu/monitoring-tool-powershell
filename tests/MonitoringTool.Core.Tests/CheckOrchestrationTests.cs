using Microsoft.Extensions.Time.Testing;
using MonitoringTool.Core.Alerts;
using MonitoringTool.Core.Checks;
using MonitoringTool.Core.Collection;
using MonitoringTool.Core.Monitoring;
using MonitoringTool.Core.Settings;
using static MonitoringTool.Core.Tests.MonitoringServiceTests;

namespace MonitoringTool.Core.Tests;

public sealed class CheckOrchestrationTests : IAsyncDisposable
{
    private readonly FakeTimeProvider time = new();
    private readonly FakeCollector collector = new();
    private readonly RecordingProgress progress = new();
    private readonly MonitoringService service;

    public CheckOrchestrationTests() =>
        this.service = new MonitoringService(new FakeFactory(this.collector), new FakeEmailSender(), this.time);

    public async ValueTask DisposeAsync() => await this.service.DisposeAsync();

    private static CheckDefinition Check(string id, int interval = 60, int timeout = 30) =>
        new(id, id.ToUpperInvariant(), "", "$true", interval, timeout, id + ".ps1");

    private static CheckRunOutput Output(string status, string message) => new([new CheckOutputRecord(status, message, null)], []);

    [Fact]
    public async Task Checks_run_on_start_and_on_their_own_interval()
    {
        await this.service.StartAsync("srv", new AppSettings(), [Check("a", interval: 60), Check("b", interval: 600)], this.progress, TestContext.Current.CancellationToken);
        await WaitUntil(() => this.progress.Of<CheckCompleted>().Count == 2);

        this.time.Advance(TimeSpan.FromSeconds(60));
        await WaitUntil(() => this.collector.CheckRuns.Count(id => id == "a") == 2);
        Assert.Equal(1, this.collector.CheckRuns.Count(id => id == "b"));
        Assert.All(this.progress.Of<CheckCompleted>(), c => Assert.Equal(CheckStatus.Ok, c.Result.Status));
    }

    [Fact]
    public async Task Concurrency_limit_is_respected()
    {
        var gate = new TaskCompletionSource<CheckRunOutput>(TaskCreationOptions.RunContinuationsAsynchronously);
        var running = 0;
        var maxRunning = 0;
        this.collector.CheckHandler = async (_, ct) =>
        {
            var now = Interlocked.Increment(ref running);
            InterlockedMax(ref maxRunning, now);
            try
            {
                return await gate.Task.WaitAsync(ct);
            }
            finally
            {
                Interlocked.Decrement(ref running);
            }
        };

        var settings = new AppSettings { Checks = new ChecksSettings { MaxConcurrentChecks = 2 } };
        await this.service.StartAsync("srv", settings, [Check("a"), Check("b"), Check("c"), Check("d"), Check("e")], this.progress, TestContext.Current.CancellationToken);

        await WaitUntil(() => Volatile.Read(ref running) == 2);
        await Task.Delay(100, TestContext.Current.CancellationToken);
        Assert.Equal(2, Volatile.Read(ref maxRunning));

        gate.SetResult(Output("Ok", "done"));
        await WaitUntil(() => this.progress.Of<CheckCompleted>().Count == 5);
        Assert.Equal(2, Volatile.Read(ref maxRunning));
    }

    [Fact]
    public async Task Failing_check_raises_an_alert_and_clears_it_when_ok()
    {
        this.collector.CheckHandler = (_, _) => Task.FromResult(Output("Critical", "Spooler stopped"));
        await this.service.StartAsync("srv", new AppSettings(), [Check("spooler")], this.progress, TestContext.Current.CancellationToken);

        await WaitUntil(() => this.progress.Of<AlertsRaised>().Any());
        var raised = this.progress.Of<AlertsRaised>().SelectMany(a => a.Alerts).Single();
        Assert.Equal("check:spooler", raised.Key);
        Assert.Equal(AlertLevel.Critical, raised.Level);
        Assert.Equal("SPOOLER: Spooler stopped", raised.Message);

        this.collector.CheckHandler = (_, _) => Task.FromResult(Output("Ok", "running"));
        this.time.Advance(TimeSpan.FromSeconds(60));
        await WaitUntil(() => this.progress.Of<AlertsRaised>().SelectMany(a => a.Alerts).Any(a => a.Kind == AlertKind.Resolved));
    }

    [Fact]
    public async Task Script_exceptions_and_timeouts_become_unknown_results_without_alerts_or_degraded_health()
    {
        this.collector.CheckHandler = (check, _) => check.Id == "slow"
            ? Task.FromException<CheckRunOutput>(new TimeoutException())
            : Task.FromException<CheckRunOutput>(new InvalidOperationException("The term 'Get-Foo' is not recognized"));

        await this.service.StartAsync("srv", new AppSettings(), [Check("slow", timeout: 15), Check("broken")], this.progress, TestContext.Current.CancellationToken);
        await WaitUntil(() => this.progress.Of<CheckCompleted>().Count == 2);

        var results = this.progress.Of<CheckCompleted>().ToDictionary(c => c.Result.CheckId, c => c.Result);
        Assert.Equal(CheckStatus.Unknown, results["slow"].Status);
        Assert.Equal("Timed out after 15 s", results["slow"].Message);
        Assert.Contains("Get-Foo", results["broken"].Message, StringComparison.Ordinal);
        Assert.Empty(this.progress.Of<AlertsRaised>());
        Assert.DoesNotContain(this.progress.Of<StatusChanged>(), s => s.State == MonitoringState.Degraded);
    }

    [Fact]
    public async Task Disabled_checks_do_not_run_on_schedule_but_can_be_run_manually()
    {
        var settings = new AppSettings { Checks = new ChecksSettings().WithEnabled("off", false) };
        await this.service.StartAsync("srv", settings, [Check("on"), Check("off")], this.progress, TestContext.Current.CancellationToken);
        await WaitUntil(() => this.progress.Of<CheckCompleted>().Count == 1);
        Assert.DoesNotContain("off", this.collector.CheckRuns);

        Assert.True(await this.service.RunCheckNowAsync("off"));
        Assert.Contains("off", this.collector.CheckRuns);
        Assert.False(await this.service.RunCheckNowAsync("does-not-exist"));
    }

    [Fact]
    public async Task Disabling_a_failing_check_resolves_its_alert()
    {
        this.collector.CheckHandler = (_, _) => Task.FromResult(Output("Warning", "Reboot pending"));
        await this.service.StartAsync("srv", new AppSettings(), [Check("reboot")], this.progress, TestContext.Current.CancellationToken);
        await WaitUntil(() => this.progress.Of<AlertsRaised>().Any());

        this.service.UpdateSettings(new AppSettings { Checks = new ChecksSettings().WithEnabled("reboot", false) });
        this.time.Advance(TimeSpan.FromSeconds(60));

        await WaitUntil(() => this.progress.Of<AlertsRaised>().SelectMany(a => a.Alerts).Any(a => a.Kind == AlertKind.Resolved));
    }

    [Fact]
    public async Task Run_now_is_refused_while_the_same_check_is_running()
    {
        var gate = new TaskCompletionSource<CheckRunOutput>(TaskCreationOptions.RunContinuationsAsynchronously);
        this.collector.CheckHandler = (_, ct) => gate.Task.WaitAsync(ct);
        await this.service.StartAsync("srv", new AppSettings(), [Check("a")], this.progress, TestContext.Current.CancellationToken);
        await WaitUntil(() => this.progress.Of<CheckStarted>().Any());

        Assert.False(await this.service.RunCheckNowAsync("a"));

        gate.SetResult(Output("Ok", "done"));
        await WaitUntil(() => this.progress.Of<CheckCompleted>().Any());
        Assert.True(await this.service.RunCheckNowAsync("a"));
    }

    [Fact]
    public async Task Reload_replaces_the_scheduled_checks_without_reconnecting()
    {
        await this.service.StartAsync("srv", new AppSettings(), [Check("old")], this.progress, TestContext.Current.CancellationToken);
        await WaitUntil(() => this.collector.CheckRuns.Contains("old"));

        await this.service.ReloadChecksAsync([Check("new1"), Check("new2")]);

        await WaitUntil(() => this.collector.CheckRuns.Contains("new1") && this.collector.CheckRuns.Contains("new2"));
        Assert.Equal(["new1", "new2"], this.service.Checks.Select(c => c.Id));
        Assert.Equal(1, this.collector.Connects);
        Assert.True(this.service.IsRunning);

        this.time.Advance(TimeSpan.FromSeconds(60));
        await WaitUntil(() => this.collector.CheckRuns.Count(id => id == "new1") == 2);
        Assert.Equal(1, this.collector.CheckRuns.Count(id => id == "old"));
    }

    [Fact]
    public async Task Stop_cancels_running_checks()
    {
        this.collector.CheckHandler = (_, ct) => Task.Delay(Timeout.Infinite, ct).ContinueWith(_ => Output("Ok", ""), TaskScheduler.Default);
        await this.service.StartAsync("srv", new AppSettings(), [Check("hang")], this.progress, TestContext.Current.CancellationToken);
        await WaitUntil(() => this.progress.Of<CheckStarted>().Any());

        await this.service.StopAsync().WaitAsync(TimeSpan.FromSeconds(10), TestContext.Current.CancellationToken);

        Assert.False(this.service.IsRunning);
        Assert.Empty(this.service.Checks);
    }

    private static void InterlockedMax(ref int target, int value)
    {
        int current;
        while (value > (current = Volatile.Read(ref target)) && Interlocked.CompareExchange(ref target, value, current) != current)
        {
        }
    }
}
