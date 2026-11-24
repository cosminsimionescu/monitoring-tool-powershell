using MonitoringTool.Core.Models;

namespace MonitoringTool.Core.Collection;

/// <summary>
/// Reads metrics from one machine. Implementations must be safe to call concurrently
/// from different polling loops and must honour cancellation promptly.
/// </summary>
public interface IMetricsCollector : IAsyncDisposable
{
    string Target { get; }

    /// <summary>Opens (or re-opens) the connection. Throws when the target is unreachable.</summary>
    Task ConnectAsync(CancellationToken cancellationToken);

    Task<SystemSummary> GetSummaryAsync(CancellationToken cancellationToken);

    Task<ProcessesSnapshot> GetTopProcessesAsync(int count, CancellationToken cancellationToken);

    Task<DisksSnapshot> GetDisksAsync(CancellationToken cancellationToken);
}

public interface IMetricsCollectorFactory
{
    IMetricsCollector Create(string target, TimeSpan commandTimeout);
}
