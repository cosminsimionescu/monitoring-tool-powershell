namespace MonitoringTool.Core.Collection;

public static class HostName
{
    /// <summary>
    /// True for a DNS name, NetBIOS name or IP address. Anything else (spaces, quotes, semicolons…)
    /// is rejected, so a server name can never smuggle extra PowerShell into a command line.
    /// </summary>
    public static bool IsValid(string? value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 253)
        {
            return false;
        }

        return Uri.CheckHostName(value.Trim()) != UriHostNameType.Unknown;
    }
}
