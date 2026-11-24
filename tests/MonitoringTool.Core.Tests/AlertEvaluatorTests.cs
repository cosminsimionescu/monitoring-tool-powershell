using Microsoft.Extensions.Time.Testing;
using MonitoringTool.Core.Alerts;
using MonitoringTool.Core.Models;
using MonitoringTool.Core.Settings;

namespace MonitoringTool.Core.Tests;

public sealed class AlertEvaluatorTests
{
    private readonly FakeTimeProvider time = new(new DateTimeOffset(2026, 1, 1, 12, 0, 0, TimeSpan.Zero));
    private readonly AlertEvaluator evaluator;
    private readonly AlertSettings settings = new()
    {
        CpuThresholdPercent = 80,
        MemoryThresholdPercent = 90,
        DiskFreeThresholdPercent = 10,
        InAppRepeatMinutes = 15,
        EmailEnabled = false,
        EmailRepeatMinutes = 60,
    };

    public AlertEvaluatorTests() => this.evaluator = new AlertEvaluator(this.time);

    [Fact]
    public void Below_threshold_raises_nothing()
    {
        Assert.Empty(this.evaluator.Evaluate("srv", Summary(cpu: 79.9), this.settings));
    }

    [Fact]
    public void Crossing_threshold_raises_once_then_repeats_after_interval_then_resolves()
    {
        var raised = Assert.Single(this.evaluator.Evaluate("srv", Summary(cpu: 80), this.settings));
        Assert.Equal(AlertKind.Raised, raised.Kind);
        Assert.Equal("cpu", raised.Key);
        Assert.Equal(AlertChannels.InApp, raised.Channels);

        this.time.Advance(TimeSpan.FromMinutes(14));
        Assert.Empty(this.evaluator.Evaluate("srv", Summary(cpu: 95), this.settings));

        this.time.Advance(TimeSpan.FromMinutes(1));
        var ongoing = Assert.Single(this.evaluator.Evaluate("srv", Summary(cpu: 95), this.settings));
        Assert.Equal(AlertKind.Ongoing, ongoing.Kind);

        var resolved = Assert.Single(this.evaluator.Evaluate("srv", Summary(cpu: 10), this.settings));
        Assert.Equal(AlertKind.Resolved, resolved.Kind);
        Assert.Equal(AlertChannels.InApp, resolved.Channels);

        Assert.Empty(this.evaluator.Evaluate("srv", Summary(cpu: 10), this.settings));
    }

    [Fact]
    public void Email_uses_its_own_repeat_interval_and_is_sent_on_resolve()
    {
        var s = this.settings with { EmailEnabled = true };

        var raised = Assert.Single(this.evaluator.Evaluate("srv", Summary(memory: 95), s));
        Assert.Equal(AlertChannels.InApp | AlertChannels.Email, raised.Channels);

        this.time.Advance(TimeSpan.FromMinutes(15));
        Assert.Equal(AlertChannels.InApp, Assert.Single(this.evaluator.Evaluate("srv", Summary(memory: 95), s)).Channels);

        this.time.Advance(TimeSpan.FromMinutes(45));
        Assert.Equal(AlertChannels.InApp | AlertChannels.Email, Assert.Single(this.evaluator.Evaluate("srv", Summary(memory: 95), s)).Channels);

        var resolved = Assert.Single(this.evaluator.Evaluate("srv", Summary(memory: 50), s));
        Assert.Equal(AlertKind.Resolved, resolved.Kind);
        Assert.True(resolved.Channels.HasFlag(AlertChannels.Email));
    }

    [Fact]
    public void Enabling_email_while_alert_is_active_sends_on_next_evaluation()
    {
        Assert.Single(this.evaluator.Evaluate("srv", Summary(cpu: 90), this.settings));

        var withEmail = this.settings with { EmailEnabled = true };
        var next = Assert.Single(this.evaluator.Evaluate("srv", Summary(cpu: 90), withEmail));
        Assert.Equal(AlertChannels.Email, next.Channels);
    }

    [Fact]
    public void Disabled_rule_never_raises_and_disabling_resolves_active_alert()
    {
        var off = this.settings with { CpuEnabled = false };
        Assert.Empty(this.evaluator.Evaluate("srv", Summary(cpu: 100), off));

        Assert.Single(this.evaluator.Evaluate("srv", Summary(cpu: 100), this.settings));
        Assert.Equal(AlertKind.Resolved, Assert.Single(this.evaluator.Evaluate("srv", Summary(cpu: 100), off)).Kind);
    }

    [Fact]
    public void Every_low_drive_is_reported_not_just_the_last_one()
    {
        // Regression: v1 re-created its drive dictionary inside the loop, so only the last drive was ever checked.
        var disks = new DisksSnapshot(
        [
            new DiskUsage("C:", "OS", 100, 5),
            new DiskUsage("D:", "Data", 100, 50),
            new DiskUsage("E:", "Logs", 200, 2),
        ], this.time.GetUtcNow());

        var alerts = this.evaluator.Evaluate("srv", disks, this.settings);

        Assert.Equal(["disk:C:", "disk:E:"], alerts.Select(a => a.Key));
        Assert.All(alerts, a => Assert.Equal(AlertKind.Raised, a.Kind));
    }

    [Fact]
    public void Drive_that_disappears_is_forgotten_without_a_resolve()
    {
        var low = new DisksSnapshot([new DiskUsage("X:", "", 10, 0.1)], this.time.GetUtcNow());
        Assert.Single(this.evaluator.Evaluate("srv", low, this.settings));

        Assert.Empty(this.evaluator.Evaluate("srv", new DisksSnapshot([], this.time.GetUtcNow()), this.settings));
        Assert.Equal(AlertKind.Raised, Assert.Single(this.evaluator.Evaluate("srv", low, this.settings)).Kind);
    }

    private SystemSummary Summary(double cpu = 10, double memory = 10) =>
        new("srv", cpu, 100, memory, TimeSpan.FromHours(1), this.time.GetUtcNow());
}
