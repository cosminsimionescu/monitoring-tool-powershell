namespace MonitoringTool.Core.Models;

/// <summary>CPU and memory usage of the monitored machine at a point in time.</summary>
public sealed record SystemSummary(
    string ComputerName,
    double CpuPercent,
    double TotalMemoryGb,
    double UsedMemoryGb,
    TimeSpan Uptime,
    DateTimeOffset Timestamp)
{
    public double MemoryPercent => TotalMemoryGb <= 0 ? 0 : Math.Round(UsedMemoryGb / TotalMemoryGb * 100, 2);
}

public sealed record ProcessUsage(int Id, string Name, double CpuPercent, double WorkingSetMb);

/// <summary>Top N processes ordered by CPU and by memory, collected in a single round trip.</summary>
public sealed record ProcessesSnapshot(
    IReadOnlyList<ProcessUsage> ByCpu,
    IReadOnlyList<ProcessUsage> ByMemory,
    DateTimeOffset Timestamp);

public sealed record DiskUsage(string Drive, string Label, double SizeGb, double FreeGb)
{
    public double FreePercent => SizeGb <= 0 ? 0 : Math.Round(FreeGb / SizeGb * 100, 2);
}

public sealed record DisksSnapshot(IReadOnlyList<DiskUsage> Disks, DateTimeOffset Timestamp);
