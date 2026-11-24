using MonitoringTool.Core.Models;
using MonitoringTool.Core.Settings;

namespace MonitoringTool.Core.Alerts;

/// <summary>
/// Turns metric samples into alert notifications.
/// A condition is reported once when it starts, repeated per channel after that channel's
/// repeat interval while it lasts, and reported once more when it clears. This replaces the
/// v1 behaviour of one free-running timer per alert type that re-sent stale values.
/// Not thread-safe: the monitoring service serialises calls.
/// </summary>
public sealed class AlertEvaluator(TimeProvider timeProvider)
{
    private readonly Dictionary<string, ActiveAlert> active = new(StringComparer.OrdinalIgnoreCase);

    public IReadOnlyList<AlertNotification> Evaluate(string server, SystemSummary summary, AlertSettings settings)
    {
        var results = new List<AlertNotification>();
        this.Check(results, server, settings, "cpu", settings.CpuEnabled,
            summary.CpuPercent, settings.CpuThresholdPercent, breachedWhenAbove: true,
            v => $"CPU usage is {v:0.#}% (threshold {settings.CpuThresholdPercent}%)",
            v => $"CPU usage is back to normal ({v:0.#}%)");
        this.Check(results, server, settings, "memory", settings.MemoryEnabled,
            summary.MemoryPercent, settings.MemoryThresholdPercent, breachedWhenAbove: true,
            v => $"Memory usage is {v:0.#}% (threshold {settings.MemoryThresholdPercent}%)",
            v => $"Memory usage is back to normal ({v:0.#}%)");
        return results;
    }

    public IReadOnlyList<AlertNotification> Evaluate(string server, DisksSnapshot disks, AlertSettings settings)
    {
        var results = new List<AlertNotification>();
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var disk in disks.Disks)
        {
            var key = "disk:" + disk.Drive;
            seen.Add(key);
            this.Check(results, server, settings, key, settings.DiskEnabled,
                disk.FreePercent, settings.DiskFreeThresholdPercent, breachedWhenAbove: false,
                v => $"Low free space on {disk.Drive}: {v:0.#}% free, {disk.FreeGb:0.#} GB (threshold {settings.DiskFreeThresholdPercent}%)",
                v => $"Free space on {disk.Drive} is back to normal ({v:0.#}% free)");
        }

        // A drive that disappeared (unmounted) can no longer be "low"; forget it silently.
        foreach (var stale in this.active.Keys.Where(k => k.StartsWith("disk:", StringComparison.OrdinalIgnoreCase) && !seen.Contains(k)).ToList())
        {
            this.active.Remove(stale);
        }

        return results;
    }

    /// <summary>Forget all state, e.g. when monitoring a different server.</summary>
    public void Reset() => this.active.Clear();

    private void Check(
        List<AlertNotification> results,
        string server,
        AlertSettings settings,
        string key,
        bool enabled,
        double value,
        double threshold,
        bool breachedWhenAbove,
        Func<double, string> raisedMessage,
        Func<double, string> resolvedMessage)
    {
        var now = timeProvider.GetUtcNow();
        var breached = enabled && (breachedWhenAbove ? value >= threshold : value < threshold);
        var emailOn = settings.EmailEnabled;

        if (!this.active.TryGetValue(key, out var state))
        {
            if (!breached)
            {
                return;
            }

            var channels = AlertChannels.InApp | (emailOn ? AlertChannels.Email : AlertChannels.None);
            this.active[key] = new ActiveAlert(now, now, emailOn ? now : null);
            results.Add(new AlertNotification(server, key, AlertKind.Raised, raisedMessage(value), value, threshold, channels, now));
            return;
        }

        if (!breached)
        {
            this.active.Remove(key);

            // Only e-mail "resolved" if somebody was e-mailed about the problem.
            var channels = AlertChannels.InApp | (emailOn && state.LastEmail is not null ? AlertChannels.Email : AlertChannels.None);
            results.Add(new AlertNotification(server, key, AlertKind.Resolved, resolvedMessage(value), value, threshold, channels, now));
            return;
        }

        var repeat = AlertChannels.None;
        if (now - state.LastInApp >= TimeSpan.FromMinutes(settings.InAppRepeatMinutes))
        {
            repeat |= AlertChannels.InApp;
            state.LastInApp = now;
        }

        if (emailOn && (state.LastEmail is null || now - state.LastEmail >= TimeSpan.FromMinutes(settings.EmailRepeatMinutes)))
        {
            repeat |= AlertChannels.Email;
            state.LastEmail = now;
        }

        if (repeat != AlertChannels.None)
        {
            results.Add(new AlertNotification(server, key, AlertKind.Ongoing, raisedMessage(value), value, threshold, repeat, now));
        }
    }

    private sealed class ActiveAlert(DateTimeOffset since, DateTimeOffset lastInApp, DateTimeOffset? lastEmail)
    {
        public DateTimeOffset Since { get; } = since;

        public DateTimeOffset LastInApp { get; set; } = lastInApp;

        public DateTimeOffset? LastEmail { get; set; } = lastEmail;
    }
}
