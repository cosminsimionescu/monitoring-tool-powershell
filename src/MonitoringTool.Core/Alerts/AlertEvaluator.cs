using MonitoringTool.Core.Checks;
using MonitoringTool.Core.Models;
using MonitoringTool.Core.Settings;

namespace MonitoringTool.Core.Alerts;

/// <summary>
/// Turns samples and check results into alert notifications, using one state machine for every
/// kind of condition. A condition is reported once when it starts, again if it escalates
/// (Warning → Critical), repeated per channel after that channel's repeat interval while it lasts,
/// and reported once more when it clears.
/// Not thread-safe: the monitoring service serialises calls.
/// </summary>
public sealed class AlertEvaluator(TimeProvider timeProvider)
{
    private readonly Dictionary<string, ActiveAlert> active = new(StringComparer.OrdinalIgnoreCase);

    public IReadOnlyList<AlertNotification> Evaluate(string server, SystemSummary summary, AlertSettings settings)
    {
        ArgumentNullException.ThrowIfNull(summary);
        ArgumentNullException.ThrowIfNull(settings);

        var results = new List<AlertNotification>();
        this.Threshold(results, server, settings, "cpu", settings.CpuEnabled,
            summary.CpuPercent, settings.CpuThresholdPercent, breachedWhenAbove: true,
            v => $"CPU usage is {v:0.#}% (threshold {settings.CpuThresholdPercent}%)",
            v => $"CPU usage is back to normal ({v:0.#}%)");
        this.Threshold(results, server, settings, "memory", settings.MemoryEnabled,
            summary.MemoryPercent, settings.MemoryThresholdPercent, breachedWhenAbove: true,
            v => $"Memory usage is {v:0.#}% (threshold {settings.MemoryThresholdPercent}%)",
            v => $"Memory usage is back to normal ({v:0.#}%)");
        return results;
    }

    public IReadOnlyList<AlertNotification> Evaluate(string server, DisksSnapshot disks, AlertSettings settings)
    {
        ArgumentNullException.ThrowIfNull(disks);
        ArgumentNullException.ThrowIfNull(settings);

        var results = new List<AlertNotification>();
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var disk in disks.Disks)
        {
            var key = "disk:" + disk.Drive;
            seen.Add(key);
            this.Threshold(results, server, settings, key, settings.DiskEnabled,
                disk.FreePercent, settings.DiskFreeThresholdPercent, breachedWhenAbove: false,
                v => $"Low free space on {disk.Drive} {v:0.#}% free ({disk.FreeGb:0.#} GB), threshold {settings.DiskFreeThresholdPercent}%",
                v => $"Free space on {disk.Drive} is back to normal ({v:0.#}% free)");
        }

        // A drive that disappeared (unmounted) can no longer be "low"; forget it silently.
        this.Forget(k => k.StartsWith("disk:", StringComparison.OrdinalIgnoreCase) && !seen.Contains(k));
        return results;
    }

    /// <summary>
    /// Warning and Critical results raise alerts. Unknown (the check could not run) does not change
    /// the alert state: a flaky script or a dropped connection should neither raise nor resolve.
    /// </summary>
    public IReadOnlyList<AlertNotification> Evaluate(string server, CheckResult result, AlertSettings settings, bool alertsEnabled)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(settings);

        if (result.Status == CheckStatus.Unknown && alertsEnabled)
        {
            return [];
        }

        AlertLevel? level = alertsEnabled
            ? result.Status switch
            {
                CheckStatus.Warning => AlertLevel.Warning,
                CheckStatus.Critical => AlertLevel.Critical,
                _ => null,
            }
            : null;

        var results = new List<AlertNotification>();
        this.Track(results, server, settings, "check:" + result.CheckId, level,
            $"{result.Name}: {result.Message}",
            $"{result.Name} is OK again",
            result.Value, threshold: null, unit: "");
        return results;
    }

    /// <summary>Drops state for checks that no longer exist (e.g. after a reload).</summary>
    public void RetainChecks(IEnumerable<string> checkIds)
    {
        var keep = new HashSet<string>(checkIds.Select(id => "check:" + id), StringComparer.OrdinalIgnoreCase);
        this.Forget(k => k.StartsWith("check:", StringComparison.OrdinalIgnoreCase) && !keep.Contains(k));
    }

    /// <summary>Forget all state, e.g. when monitoring a different server.</summary>
    public void Reset() => this.active.Clear();

    private void Forget(Func<string, bool> predicate)
    {
        foreach (var key in this.active.Keys.Where(predicate).ToList())
        {
            this.active.Remove(key);
        }
    }

    private void Threshold(
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
        var breached = enabled && (breachedWhenAbove ? value >= threshold : value < threshold);
        this.Track(results, server, settings, key, breached ? AlertLevel.Critical : null,
            raisedMessage(value), resolvedMessage(value), value, threshold, "%");
    }

    /// <param name="level">Current severity, or null when the condition is not (or no longer) present.</param>
    private void Track(
        List<AlertNotification> results,
        string server,
        AlertSettings settings,
        string key,
        AlertLevel? level,
        string raisedMessage,
        string resolvedMessage,
        double? value,
        double? threshold,
        string unit)
    {
        var now = timeProvider.GetUtcNow();
        var emailOn = settings.EmailEnabled;

        AlertNotification Notify(AlertKind kind, AlertLevel lvl, string message, AlertChannels channels) =>
            new(server, key, kind, lvl, message, value, threshold, unit, channels, now);

        if (!this.active.TryGetValue(key, out var state))
        {
            if (level is null)
            {
                return;
            }

            this.active[key] = new ActiveAlert(level.Value, now, emailOn ? now : null);
            results.Add(Notify(AlertKind.Raised, level.Value, raisedMessage, AlertChannels.InApp | (emailOn ? AlertChannels.Email : AlertChannels.None)));
            return;
        }

        if (level is null)
        {
            this.active.Remove(key);

            // Only e-mail "resolved" if somebody was e-mailed about the problem.
            var channels = AlertChannels.InApp | (emailOn && state.LastEmail is not null ? AlertChannels.Email : AlertChannels.None);
            results.Add(Notify(AlertKind.Resolved, state.Level, resolvedMessage, channels));
            return;
        }

        if (level.Value > state.Level)
        {
            // Escalation is news: notify on every channel straight away and restart the repeat clocks.
            state.Level = level.Value;
            state.LastInApp = now;
            state.LastEmail = emailOn ? now : state.LastEmail;
            results.Add(Notify(AlertKind.Raised, level.Value, raisedMessage, AlertChannels.InApp | (emailOn ? AlertChannels.Email : AlertChannels.None)));
            return;
        }

        state.Level = level.Value; // A de-escalation (Critical → Warning) is reported with the next repeat.

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
            results.Add(Notify(AlertKind.Ongoing, level.Value, raisedMessage, repeat));
        }
    }

    private sealed class ActiveAlert(AlertLevel level, DateTimeOffset lastInApp, DateTimeOffset? lastEmail)
    {
        public AlertLevel Level { get; set; } = level;

        public DateTimeOffset LastInApp { get; set; } = lastInApp;

        public DateTimeOffset? LastEmail { get; set; } = lastEmail;
    }
}
