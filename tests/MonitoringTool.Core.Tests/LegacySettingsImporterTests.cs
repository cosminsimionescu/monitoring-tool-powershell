using MonitoringTool.Core.Settings;

namespace MonitoringTool.Core.Tests;

public sealed class LegacySettingsImporterTests : IDisposable
{
    private readonly string directory = Directory.CreateTempSubdirectory("mt-legacy-").FullName;

    public void Dispose() => Directory.Delete(this.directory, recursive: true);

    [Fact]
    public void Returns_null_when_no_legacy_files_exist() => Assert.Null(LegacySettingsImporter.TryImport([this.directory]));

    [Fact]
    public void Imports_v1_alert_and_smtp_files()
    {
        File.WriteAllLines(Path.Combine(this.directory, LegacySettingsImporter.AlertsFileName),
            ["true", "ops@example.com", "2", "hours", "75", "80", "15", "30", "seconds"]);
        File.WriteAllLines(Path.Combine(this.directory, LegacySettingsImporter.SmtpFileName),
            ["true", "alerts@example.com", "app-password", "smtp.example.com"]);

        var settings = LegacySettingsImporter.TryImport([this.directory]);

        Assert.NotNull(settings);
        Assert.True(settings.Alerts.EmailEnabled);
        Assert.Equal("ops@example.com", settings.Alerts.EmailRecipients);
        Assert.Equal(120, settings.Alerts.EmailRepeatMinutes);
        Assert.Equal(75, settings.Alerts.MemoryThresholdPercent);
        Assert.Equal(80, settings.Alerts.CpuThresholdPercent);
        Assert.Equal(15, settings.Alerts.DiskFreeThresholdPercent);
        Assert.Equal(1, settings.Alerts.InAppRepeatMinutes);
        Assert.Equal("smtp.example.com", settings.Smtp.Host);
        Assert.Equal("app-password", settings.Smtp.Password);
        Assert.Equal(587, settings.Smtp.Port);
        Assert.Empty(settings.Validate());
    }

    [Fact]
    public void Tolerates_truncated_and_email_disabled_files()
    {
        var alerts = LegacySettingsImporter.ParseAlerts(["false", "", "", "", "70"], new AlertSettings());

        Assert.False(alerts.EmailEnabled);
        Assert.Equal(70, alerts.MemoryThresholdPercent);
        Assert.Equal(new AlertSettings().CpuThresholdPercent, alerts.CpuThresholdPercent);
    }
}
