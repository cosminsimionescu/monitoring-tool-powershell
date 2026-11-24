using System.Net.Mail;

namespace MonitoringTool.Core.Settings;

/// <summary>All user configuration. Immutable: edit with <c>with</c> expressions and save the result.</summary>
public sealed record AppSettings
{
    public string LastServer { get; init; } = "";

    public PollingSettings Polling { get; init; } = new();

    public AlertSettings Alerts { get; init; } = new();

    public SmtpSettings Smtp { get; init; } = new();

    public ChecksSettings Checks { get; init; } = new();

    public IReadOnlyList<string> Validate() =>
        [.. Polling.Validate(), .. Alerts.Validate(), .. Checks.Validate(), .. Alerts.EmailEnabled ? Smtp.Validate() : []];
}

public sealed record ChecksSettings
{
    /// <summary>Raise in-app/e-mail alerts when a check reports Warning or Critical.</summary>
    public bool AlertsEnabled { get; init; } = true;

    /// <summary>Check ids (script file names) the user switched off.</summary>
    public IReadOnlyList<string> DisabledCheckIds { get; init; } = [];

    /// <summary>
    /// How many custom checks may run on the server at the same time. Keeps a large check library
    /// from saturating the remote session and delaying the built-in metrics.
    /// </summary>
    public int MaxConcurrentChecks { get; init; } = 2;

    public bool IsEnabled(string checkId) => !DisabledCheckIds.Contains(checkId, StringComparer.OrdinalIgnoreCase);

    public ChecksSettings WithEnabled(string checkId, bool enabled) => this with
    {
        DisabledCheckIds = enabled
            ? [.. DisabledCheckIds.Where(id => !string.Equals(id, checkId, StringComparison.OrdinalIgnoreCase))]
            : [.. DisabledCheckIds.Where(id => !string.Equals(id, checkId, StringComparison.OrdinalIgnoreCase)), checkId],
    };

    public IReadOnlyList<string> Validate()
    {
        var errors = new List<string>();
        PollingSettings.Check(errors, MaxConcurrentChecks, 1, 5, "Checks running at the same time");
        return errors;
    }
}

public sealed record PollingSettings
{
    /// <summary>CPU + memory refresh interval.</summary>
    public int SummaryIntervalSeconds { get; init; } = 10;

    public int ProcessesIntervalSeconds { get; init; } = 15;

    public int DisksIntervalSeconds { get; init; } = 120;

    public int TopProcessCount { get; init; } = 10;

    /// <summary>A remote command running longer than this is stopped and reported as an error.</summary>
    public int CommandTimeoutSeconds { get; init; } = 60;

    public IReadOnlyList<string> Validate()
    {
        var errors = new List<string>();
        Check(errors, SummaryIntervalSeconds, 2, 3600, "CPU/memory refresh interval (seconds)");
        Check(errors, ProcessesIntervalSeconds, 2, 3600, "Process refresh interval (seconds)");
        Check(errors, DisksIntervalSeconds, 10, 86400, "Disk refresh interval (seconds)");
        Check(errors, TopProcessCount, 1, 100, "Number of top processes");
        Check(errors, CommandTimeoutSeconds, 5, 600, "Command timeout (seconds)");
        return errors;
    }

    internal static void Check(List<string> errors, int value, int min, int max, string name)
    {
        if (value < min || value > max)
        {
            errors.Add($"{name} must be between {min} and {max}.");
        }
    }
}

public sealed record AlertSettings
{
    public bool CpuEnabled { get; init; } = true;

    public int CpuThresholdPercent { get; init; } = 85;

    public bool MemoryEnabled { get; init; } = true;

    public int MemoryThresholdPercent { get; init; } = 85;

    public bool DiskEnabled { get; init; } = true;

    /// <summary>Alert when a drive has less than this percentage of free space.</summary>
    public int DiskFreeThresholdPercent { get; init; } = 10;

    /// <summary>While a condition persists, repeat the in-app alert this often.</summary>
    public int InAppRepeatMinutes { get; init; } = 15;

    public bool EmailEnabled { get; init; }

    /// <summary>One or more addresses separated by ';' or ','.</summary>
    public string EmailRecipients { get; init; } = "";

    /// <summary>While a condition persists, repeat the e-mail this often.</summary>
    public int EmailRepeatMinutes { get; init; } = 60;

    public IReadOnlyList<string> RecipientList =>
        EmailRecipients.Split([';', ','], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    public IReadOnlyList<string> Validate()
    {
        var errors = new List<string>();
        PollingSettings.Check(errors, CpuThresholdPercent, 1, 100, "CPU threshold (%)");
        PollingSettings.Check(errors, MemoryThresholdPercent, 1, 100, "Memory threshold (%)");
        PollingSettings.Check(errors, DiskFreeThresholdPercent, 1, 99, "Free disk space threshold (%)");
        PollingSettings.Check(errors, InAppRepeatMinutes, 1, 1440, "In-app alert repeat (minutes)");
        PollingSettings.Check(errors, EmailRepeatMinutes, 1, 10080, "E-mail alert repeat (minutes)");

        if (EmailEnabled)
        {
            if (RecipientList.Count == 0)
            {
                errors.Add("Enter at least one e-mail recipient.");
            }

            errors.AddRange(RecipientList.Where(r => !EmailAddress.IsValid(r)).Select(r => $"'{r}' is not a valid e-mail address."));
        }

        return errors;
    }
}

public enum SmtpSecurity
{
    /// <summary>STARTTLS when the server offers it, implicit TLS on port 465.</summary>
    Auto,
    StartTls,
    SslOnConnect,
    None,
}

public sealed record SmtpSettings
{
    public string Host { get; init; } = "";

    public int Port { get; init; } = 587;

    public SmtpSecurity Security { get; init; } = SmtpSecurity.Auto;

    public string UserName { get; init; } = "";

    /// <summary>Plain text in memory only; <see cref="JsonSettingsStore"/> encrypts it on disk.</summary>
    public string Password { get; init; } = "";

    /// <summary>Sender address. Falls back to <see cref="UserName"/> when empty.</summary>
    public string FromAddress { get; init; } = "";

    public string EffectiveFromAddress => string.IsNullOrWhiteSpace(FromAddress) ? UserName : FromAddress;

    public bool IsConfigured => Validate().Count == 0;

    public IReadOnlyList<string> Validate()
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(Host))
        {
            errors.Add("SMTP server is required.");
        }

        PollingSettings.Check(errors, Port, 1, 65535, "SMTP port");

        if (!EmailAddress.IsValid(EffectiveFromAddress))
        {
            errors.Add("A valid sender address (or an e-mail user name) is required.");
        }

        return errors;
    }
}

internal static class EmailAddress
{
    public static bool IsValid(string? value) => MailAddress.TryCreate(value, out var parsed) && parsed.Address == value;
}
