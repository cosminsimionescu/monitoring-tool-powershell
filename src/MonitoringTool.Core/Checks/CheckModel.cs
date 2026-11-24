namespace MonitoringTool.Core.Checks;

public enum CheckStatus
{
    Ok,
    Warning,
    Critical,

    /// <summary>The check itself could not run (script error, timeout, connection lost).</summary>
    Unknown,
}

/// <summary>A PowerShell script that runs on the monitored server on its own schedule.</summary>
/// <param name="Id">Stable identity: the script's file name without extension.</param>
/// <param name="Source">Where the script was loaded from (for display/editing).</param>
public sealed record CheckDefinition(
    string Id,
    string Name,
    string Description,
    string Script,
    int IntervalSeconds,
    int TimeoutSeconds,
    string Source);

public sealed record CheckResult(
    string CheckId,
    string Name,
    CheckStatus Status,
    string Message,
    double? Value,
    DateTimeOffset Timestamp,
    TimeSpan Duration);

/// <summary>One object emitted by a check script, already unwrapped from PowerShell types.</summary>
/// <param name="Status">Raw status text (or "True"/"False"); null when the object had no Status.</param>
public sealed record CheckOutputRecord(string? Status, string? Message, double? Value);
