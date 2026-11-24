namespace MonitoringTool.Core.Alerts;

public enum AlertKind
{
    /// <summary>The condition just started (or got worse, e.g. Warning → Critical).</summary>
    Raised,

    /// <summary>The condition is still active and the repeat interval has elapsed.</summary>
    Ongoing,

    /// <summary>The condition cleared.</summary>
    Resolved,
}

public enum AlertLevel
{
    Warning,
    Critical,
}

[Flags]
public enum AlertChannels
{
    None = 0,
    InApp = 1,
    Email = 2,
}

/// <param name="Key">Stable identity of the monitored condition, e.g. "cpu", "disk:C:" or "check:pending-reboot".</param>
/// <param name="Value">Measured value, when the condition has one.</param>
/// <param name="Threshold">Configured limit, for threshold-based conditions.</param>
/// <param name="Unit">Unit for <paramref name="Value"/>/<paramref name="Threshold"/>, e.g. "%".</param>
public sealed record AlertNotification(
    string Server,
    string Key,
    AlertKind Kind,
    AlertLevel Level,
    string Message,
    double? Value,
    double? Threshold,
    string Unit,
    AlertChannels Channels,
    DateTimeOffset Timestamp)
{
    public string FormatValue() => this.Value is { } v ? $"{v:0.##}{(this.Unit.Length > 0 ? " " + this.Unit : "")}" : "";
}
