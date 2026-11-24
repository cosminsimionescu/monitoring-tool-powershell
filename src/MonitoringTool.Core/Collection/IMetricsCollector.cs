using MonitoringTool.Core.Checks;
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

    /// <summary>
    /// Runs a custom check script on the target and returns its raw output. Must stop the script and
    /// throw <see cref="TimeoutException"/> after <see cref="CheckDefinition.TimeoutSeconds"/>.
    /// </summary>
    Task<CheckRunOutput> RunCheckAsync(CheckDefinition check, CancellationToken cancellationToken);
}

/// <summary>What a check script produced: its output objects and the messages of any errors it wrote.</summary>
public sealed record CheckRunOutput(IReadOnlyList<CheckOutputRecord> Records, IReadOnlyList<string> Errors);

public interface IMetricsCollectorFactory
{
    IMetricsCollector Create(string target, TimeSpan commandTimeout);
}
