namespace MonitoringTool.Core.Alerts;

public enum AlertKind
{
    /// <summary>The threshold was just crossed.</summary>
    Raised,

    /// <summary>The condition is still active and the repeat interval has elapsed.</summary>
    Ongoing,

    /// <summary>The value is back within the threshold.</summary>
    Resolved,
}

[Flags]
public enum AlertChannels
{
    None = 0,
    InApp = 1,
    Email = 2,
}

/// <param name="Key">Stable identity of the monitored condition, e.g. "cpu" or "disk:C:".</param>
public sealed record AlertNotification(
    string Server,
    string Key,
    AlertKind Kind,
    string Message,
    double Value,
    double Threshold,
    AlertChannels Channels,
    DateTimeOffset Timestamp);
