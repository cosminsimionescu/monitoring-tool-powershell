namespace MonitoringTool.Core.Settings;

/// <summary>
/// Reads the plain-text files written by v1.x (AlertsConfigFile.txt / configSMTP.txt)
/// so that upgrading users keep their configuration.
/// </summary>
public static class LegacySettingsImporter
{
    public const string AlertsFileName = "AlertsConfigFile.txt";
    public const string SmtpFileName = "configSMTP.txt";

    /// <summary>Returns null when no legacy file exists in any of <paramref name="directories"/>.</summary>
    public static AppSettings? TryImport(IEnumerable<string> directories)
    {
        foreach (var dir in directories.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            var alertsPath = Path.Combine(dir, AlertsFileName);
            var smtpPath = Path.Combine(dir, SmtpFileName);
            if (!File.Exists(alertsPath) && !File.Exists(smtpPath))
            {
                continue;
            }

            var settings = new AppSettings();
            if (File.Exists(alertsPath))
            {
                settings = settings with { Alerts = ParseAlerts(File.ReadAllLines(alertsPath), settings.Alerts) };
            }

            if (File.Exists(smtpPath))
            {
                settings = settings with { Smtp = ParseSmtp(File.ReadAllLines(smtpPath), settings.Smtp) };
            }

            return settings;
        }

        return null;
    }

    /// <summary>
    /// v1 layout: emailEnabled, email, emailInterval, "minutes"|"hours",
    /// memory%, cpu%, freeVolume%, inAppInterval, "minutes"|"seconds".
    /// </summary>
    internal static AlertSettings ParseAlerts(IReadOnlyList<string> lines, AlertSettings defaults)
    {
        string Line(int i) => i < lines.Count ? lines[i].Trim() : "";
        int Int(int i, int fallback) => int.TryParse(Line(i), out var v) ? v : fallback;

        var emailEnabled = string.Equals(Line(0), "true", StringComparison.OrdinalIgnoreCase);
        var emailInterval = Int(2, 0);
        var emailRepeat = emailInterval <= 0
            ? defaults.EmailRepeatMinutes
            : Line(3) == "hours" ? emailInterval * 60 : emailInterval;

        var inAppInterval = Int(7, 0);
        var inAppRepeat = inAppInterval <= 0
            ? defaults.InAppRepeatMinutes
            : Line(8) == "seconds" ? Math.Max(1, inAppInterval / 60) : inAppInterval;

        return defaults with
        {
            EmailEnabled = emailEnabled && Line(1).Length > 0,
            EmailRecipients = emailEnabled ? Line(1) : "",
            EmailRepeatMinutes = emailRepeat,
            MemoryThresholdPercent = Int(4, defaults.MemoryThresholdPercent),
            CpuThresholdPercent = Int(5, defaults.CpuThresholdPercent),
            DiskFreeThresholdPercent = Int(6, defaults.DiskFreeThresholdPercent),
            InAppRepeatMinutes = inAppRepeat,
        };
    }

    /// <summary>v1 layout: "true", user name, password, SMTP host (port was hard-coded to 587).</summary>
    internal static SmtpSettings ParseSmtp(IReadOnlyList<string> lines, SmtpSettings defaults)
    {
        if (lines.Count < 4 || !string.Equals(lines[0].Trim(), "true", StringComparison.OrdinalIgnoreCase))
        {
            return defaults;
        }

        return defaults with
        {
            UserName = lines[1].Trim(),
            Password = lines[2],
            Host = lines[3].Trim(),
            Port = 587,
            Security = SmtpSecurity.StartTls,
        };
    }
}
