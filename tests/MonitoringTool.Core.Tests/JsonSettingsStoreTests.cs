using System.Security.Cryptography;
using MonitoringTool.Core.Settings;

namespace MonitoringTool.Core.Tests;

public sealed class JsonSettingsStoreTests : IDisposable
{
    private readonly string directory = Directory.CreateTempSubdirectory("mt-settings-").FullName;

    private string FilePath => Path.Combine(this.directory, "sub", "settings.json");

    public void Dispose() => Directory.Delete(this.directory, recursive: true);

    [Fact]
    public void Missing_file_returns_defaults()
    {
        var store = new JsonSettingsStore(this.FilePath, new ReversingProtector());
        Assert.Equal(new AppSettings().Alerts, store.Load().Alerts);
    }

    [Fact]
    public void Round_trips_and_never_writes_the_password_in_clear_text()
    {
        var store = new JsonSettingsStore(this.FilePath, new ReversingProtector());
        var settings = new AppSettings
        {
            LastServer = "srv01",
            Alerts = new AlertSettings { CpuThresholdPercent = 70, EmailEnabled = true, EmailRecipients = "ops@example.com" },
            Smtp = new SmtpSettings { Host = "smtp.example.com", UserName = "u@example.com", Password = "s3cret!", Security = SmtpSecurity.StartTls },
        };

        store.Save(settings);
        var json = File.ReadAllText(this.FilePath);
        var loaded = store.Load();

        Assert.DoesNotContain("s3cret!", json, StringComparison.Ordinal);
        Assert.Contains("StartTls", json, StringComparison.Ordinal);
        Assert.Equal(settings.LastServer, loaded.LastServer);
        Assert.Equal(settings.Alerts, loaded.Alerts);
        Assert.Equal(settings.Smtp, loaded.Smtp);
    }

    [Fact]
    public void Corrupt_file_falls_back_to_defaults()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(this.FilePath)!);
        File.WriteAllText(this.FilePath, "{ not json");

        var loaded = new JsonSettingsStore(this.FilePath, new ReversingProtector()).Load();

        Assert.Equal(new AppSettings().Polling, loaded.Polling);
    }

    [Fact]
    public void Password_that_cannot_be_decrypted_is_cleared()
    {
        new JsonSettingsStore(this.FilePath, new ReversingProtector()).Save(new AppSettings { Smtp = new SmtpSettings { Password = "pw" } });

        var loaded = new JsonSettingsStore(this.FilePath, new FailingProtector()).Load();

        Assert.Equal("", loaded.Smtp.Password);
    }

    private sealed class ReversingProtector : ISecretProtector
    {
        public string Protect(string plainText) => "enc:" + new string(plainText.Reverse().ToArray());

        public string Unprotect(string protectedText) => new(protectedText["enc:".Length..].Reverse().ToArray());
    }

    private sealed class FailingProtector : ISecretProtector
    {
        public string Protect(string plainText) => plainText;

        public string Unprotect(string protectedText) => throw new CryptographicException("different user");
    }
}
