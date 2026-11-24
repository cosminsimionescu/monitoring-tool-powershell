namespace MonitoringTool.Core.Checks;

/// <summary>
/// Turns whatever a check script printed into one status. The contract is deliberately forgiving:
/// <list type="bullet">
///   <item>Objects with <c>Status</c> ("Ok"/"Warning"/"Critical"), <c>Message</c> and optional <c>Value</c>
///   properties: the worst status wins and the problem messages are combined.</item>
///   <item><c>$true</c>/<c>$false</c>: Ok/Critical.</item>
///   <item>Plain strings: informational, reported as Ok.</item>
///   <item>No output: Ok.</item>
///   <item>Errors written by the script with no result objects: Unknown.</item>
/// </list>
/// </summary>
public static class CheckOutputInterpreter
{
    private const int MaxMessageLength = 500;

    public static (CheckStatus Status, string Message, double? Value) Interpret(
        IReadOnlyList<CheckOutputRecord> output,
        IReadOnlyList<string> errors)
    {
        ArgumentNullException.ThrowIfNull(output);
        ArgumentNullException.ThrowIfNull(errors);

        if (output.Count == 0)
        {
            return errors.Count > 0
                ? (CheckStatus.Unknown, Truncate("Script error: " + errors[0]), null)
                : (CheckStatus.Ok, "No issues found", null);
        }

        var parsed = output.Select(o => (Status: ParseStatus(o.Status), o.Message, o.Value)).ToList();
        var worst = parsed.Max(p => p.Status);

        var relevant = worst == CheckStatus.Ok ? parsed : parsed.Where(p => p.Status == worst).ToList();
        var messages = relevant.Select(p => p.Message).Where(m => !string.IsNullOrWhiteSpace(m)).Distinct().ToList();
        var message = messages.Count switch
        {
            0 => worst == CheckStatus.Ok ? "OK" : worst.ToString(),
            1 => messages[0]!,
            _ => $"{messages.Count} issues: " + string.Join("; ", messages),
        };

        var value = relevant.Select(p => p.Value).FirstOrDefault(v => v is not null);
        return (worst, Truncate(message), value);
    }

    /// <summary>Unrecognised or missing status text counts as Ok only when no status was given at all.</summary>
    public static CheckStatus ParseStatus(string? raw) => raw?.Trim().ToUpperInvariant() switch
    {
        null or "" or "OK" or "PASS" or "PASSED" or "SUCCESS" or "HEALTHY" or "TRUE" or "0" => CheckStatus.Ok,
        "WARN" or "WARNING" or "1" => CheckStatus.Warning,
        "CRIT" or "CRITICAL" or "ERROR" or "FAIL" or "FAILED" or "FALSE" or "2" => CheckStatus.Critical,
        _ => CheckStatus.Unknown,
    };

    private static string Truncate(string text) =>
        text.Length <= MaxMessageLength ? text : string.Concat(text.AsSpan(0, MaxMessageLength - 1), "…");
}
