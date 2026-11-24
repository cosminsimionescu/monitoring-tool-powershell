using System.Globalization;
using System.Text.RegularExpressions;

namespace MonitoringTool.Core.Checks;

/// <summary>
/// Reads the metadata header of a check script. Metadata lives in ordinary comments so the
/// file stays a plain, runnable .ps1:
/// <code>
/// # @name: Stopped automatic services
/// # @description: Services set to start automatically that are not running.
/// # @interval: 300
/// # @timeout: 60
/// </code>
/// Every field is optional; the file name is the fallback name.
/// </summary>
public static partial class CheckScriptParser
{
    public const int DefaultIntervalSeconds = 300;
    public const int DefaultTimeoutSeconds = 60;
    public const int MinIntervalSeconds = 10;
    public const int MaxIntervalSeconds = 86_400;
    public const int MinTimeoutSeconds = 5;
    public const int MaxTimeoutSeconds = 600;

    public static CheckDefinition Parse(string id, string script, string source)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentNullException.ThrowIfNull(script);

        var meta = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (Match m in MetadataLine().Matches(script))
        {
            meta.TryAdd(m.Groups["key"].Value, m.Groups["value"].Value.Trim());
        }

        if (string.IsNullOrWhiteSpace(script.Replace("\r", "", StringComparison.Ordinal)))
        {
            throw new FormatException("The script is empty.");
        }

        return new CheckDefinition(
            id,
            meta.TryGetValue("name", out var name) && name.Length > 0 ? name : id,
            meta.GetValueOrDefault("description", ""),
            script,
            ReadSeconds(meta, "interval", DefaultIntervalSeconds, MinIntervalSeconds, MaxIntervalSeconds),
            ReadSeconds(meta, "timeout", DefaultTimeoutSeconds, MinTimeoutSeconds, MaxTimeoutSeconds),
            source);
    }

    private static int ReadSeconds(Dictionary<string, string> meta, string key, int fallback, int min, int max)
    {
        if (!meta.TryGetValue(key, out var raw))
        {
            return fallback;
        }

        if (!int.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
        {
            throw new FormatException($"@{key} must be a whole number of seconds, not '{raw}'.");
        }

        return Math.Clamp(value, min, max);
    }

    [GeneratedRegex(@"^[ \t]*#[ \t]*@(?<key>[A-Za-z]+)[ \t]*:(?<value>[^\r\n]*)$", RegexOptions.Multiline)]
    private static partial Regex MetadataLine();
}
