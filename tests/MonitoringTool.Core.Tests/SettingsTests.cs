using MonitoringTool.Core.Collection;
using MonitoringTool.Core.Settings;

namespace MonitoringTool.Core.Tests;

public sealed class SettingsTests
{
    [Fact]
    public void Defaults_are_valid() => Assert.Empty(new AppSettings().Validate());

    [Fact]
    public void Out_of_range_values_are_reported()
    {
        var s = new AppSettings
        {
            Alerts = new AlertSettings { CpuThresholdPercent = 0, MemoryThresholdPercent = 101 },
            Polling = new PollingSettings { SummaryIntervalSeconds = 1 },
        };

        var errors = s.Validate();

        Assert.Equal(3, errors.Count);
    }

    [Fact]
    public void Email_alerts_require_recipients_and_smtp()
    {
        var s = new AppSettings { Alerts = new AlertSettings { EmailEnabled = true } };

        var errors = s.Validate();

        Assert.Contains(errors, e => e.Contains("recipient", StringComparison.OrdinalIgnoreCase));
        Assert.Contains(errors, e => e.Contains("SMTP server", StringComparison.OrdinalIgnoreCase));
    }

    [Theory]
    [InlineData("ops@example.com", true)]
    [InlineData("ops@example.com; dba@example.com", true)]
    [InlineData("not-an-address", false)]
    [InlineData("ops@example.com;bad", false)]
    public void Recipients_are_validated(string recipients, bool valid)
    {
        var s = new AlertSettings { EmailEnabled = true, EmailRecipients = recipients };
        Assert.Equal(valid, s.Validate().Count == 0);
    }

    [Fact]
    public void Sender_falls_back_to_user_name()
    {
        var smtp = new SmtpSettings { Host = "smtp.example.com", UserName = "alerts@example.com" };
        Assert.True(smtp.IsConfigured);
        Assert.Equal("alerts@example.com", smtp.EffectiveFromAddress);
    }

    [Theory]
    [InlineData("server01", true)]
    [InlineData("server01.corp.example.com", true)]
    [InlineData("10.0.0.5", true)]
    [InlineData("::1", true)]
    [InlineData("localhost", true)]
    [InlineData("", false)]
    [InlineData("srv; Remove-Item C:\\ -Recurse", false)]
    [InlineData("srv}; Stop-Computer; {", false)]
    [InlineData("srv 01", false)]
    public void Host_names_are_validated(string value, bool valid) => Assert.Equal(valid, HostName.IsValid(value));
}
