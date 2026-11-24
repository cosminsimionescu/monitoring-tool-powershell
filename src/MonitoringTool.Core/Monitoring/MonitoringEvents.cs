using MonitoringTool.Core.Alerts;
using MonitoringTool.Core.Models;

namespace MonitoringTool.Core.Monitoring;

public enum MonitoringState
{
    Stopped,
    Connecting,
    Running,

    /// <summary>Connected, but at least one metric could not be collected on its last attempt.</summary>
    Degraded,
    Stopping,
}

/// <summary>Everything the service tells the UI. Delivered through <see cref="IProgress{T}"/>.</summary>
public abstract record MonitoringEvent;

public sealed record StatusChanged(MonitoringState State, string? Message) : MonitoringEvent;

public sealed record SummaryUpdated(SystemSummary Summary) : MonitoringEvent;

public sealed record ProcessesUpdated(ProcessesSnapshot Processes) : MonitoringEvent;

public sealed record DisksUpdated(DisksSnapshot Disks) : MonitoringEvent;

/// <summary>Alerts that should be shown in the application.</summary>
public sealed record AlertsRaised(IReadOnlyList<AlertNotification> Alerts) : MonitoringEvent;

public sealed record CollectionFailed(string Area, string Message, DateTimeOffset Timestamp) : MonitoringEvent;

public sealed record EmailResult(AlertNotification Alert, bool Succeeded, string? Error) : MonitoringEvent;
