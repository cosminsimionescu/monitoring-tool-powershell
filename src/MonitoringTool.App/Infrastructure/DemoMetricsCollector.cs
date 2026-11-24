using MonitoringTool.Core.Checks;
using MonitoringTool.Core.Collection;
using MonitoringTool.Core.Models;

namespace MonitoringTool.App.Infrastructure;

internal sealed class DemoCollectorFactory : IMetricsCollectorFactory
{
    public IMetricsCollector Create(string target, TimeSpan commandTimeout) => new DemoMetricsCollector(target);
}

/// <summary>
/// Simulated data for trying the UI without a WinRM-enabled server (start with <c>--demo</c>).
/// Values drift randomly; drive D: is deliberately low on space so alerts can be seen.
/// </summary>
internal sealed class DemoMetricsCollector(string target) : IMetricsCollector
{
    private static readonly string[] ProcessNames =
        ["sqlservr", "w3wp", "MsMpEng", "svchost", "dotnet", "explorer", "lsass", "OneDrive", "Teams", "powershell", "WmiPrvSE", "spoolsv"];

    private readonly Random random = new(42);
    private double cpu = 72;

    public string Target { get; } = target;

    public Task ConnectAsync(CancellationToken cancellationToken) => Task.Delay(400, cancellationToken);

    public async Task<SystemSummary> GetSummaryAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(150, cancellationToken).ConfigureAwait(false);
        this.cpu = Math.Clamp(this.cpu + ((this.random.NextDouble() - 0.4) * 12), 5, 99);
        return new SystemSummary("DEMO-SRV01", Math.Round(this.cpu, 1), 32, Math.Round(20 + (this.random.NextDouble() * 4), 1), TimeSpan.FromHours(173.4), DateTimeOffset.Now);
    }

    public async Task<ProcessesSnapshot> GetTopProcessesAsync(int count, CancellationToken cancellationToken)
    {
        await Task.Delay(200, cancellationToken).ConfigureAwait(false);
        var all = ProcessNames
            .Select((name, i) => new ProcessUsage(1000 + (i * 412), name, Math.Round(this.random.NextDouble() * (i < 3 ? 40 : 6), 1), Math.Round((ProcessNames.Length - i) * 210 + (this.random.NextDouble() * 150), 1)))
            .ToList();
        return new ProcessesSnapshot(
            [.. all.OrderByDescending(p => p.CpuPercent).Take(count)],
            [.. all.OrderByDescending(p => p.WorkingSetMb).Take(count)],
            DateTimeOffset.Now);
    }

    public async Task<DisksSnapshot> GetDisksAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(150, cancellationToken).ConfigureAwait(false);
        return new DisksSnapshot(
        [
            new DiskUsage("C:", "System", 126.4, 41.8),
            new DiskUsage("D:", "Data", 931.5, 61.2),
            new DiskUsage("E:", "Logs", 249.9, 118.3),
        ], DateTimeOffset.Now);
    }

    public async Task<CheckRunOutput> RunCheckAsync(CheckDefinition check, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(check);
        await Task.Delay(300 + this.random.Next(700), cancellationToken).ConfigureAwait(false);
        CheckOutputRecord record = check.Id switch
        {
            "pending-reboot" => new("Warning", "Reboot pending (Windows Update)", null),
            "expiring-certificates" => new("Warning", "web01.contoso.local expires in 12 day(s)", 12),
            "recent-system-errors" => new("Warning", "3 error event(s) in the last hour: Service Control Manager x2, disk x1", 3),
            "stopped-auto-services" => new("Ok", "All automatic services are running", 0),
            _ => new("Ok", "OK (simulated)", null),
        };
        return new CheckRunOutput([record], []);
    }

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
