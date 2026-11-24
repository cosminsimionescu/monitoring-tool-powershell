using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace MonitoringTool.Core.Settings;

public interface ISettingsStore
{
    AppSettings Load();

    void Save(AppSettings settings);
}

/// <summary>
/// Persists <see cref="AppSettings"/> as JSON (by default under %LOCALAPPDATA%\MonitoringTool).
/// Writes are atomic and the SMTP password is encrypted with an <see cref="ISecretProtector"/>.
/// </summary>
public sealed class JsonSettingsStore(string filePath, ISecretProtector protector, ILogger<JsonSettingsStore>? logger = null)
    : ISettingsStore
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() },
    };

    private readonly ILogger logger = logger ?? NullLogger<JsonSettingsStore>.Instance;
    private readonly Lock gate = new();

    public string FilePath { get; } = filePath;

    public static string DefaultPath =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MonitoringTool", "settings.json");

    public AppSettings Load()
    {
        lock (this.gate)
        {
            if (!File.Exists(this.FilePath))
            {
                return new AppSettings();
            }

            AppSettings? settings;
            try
            {
                settings = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(this.FilePath), JsonOptions);
            }
            catch (Exception ex) when (ex is JsonException or IOException or UnauthorizedAccessException)
            {
                this.logger.LogError(ex, "Settings file {Path} could not be read; using defaults", this.FilePath);
                return new AppSettings();
            }

            settings ??= new AppSettings();
            return settings with { Smtp = settings.Smtp with { Password = this.Decrypt(settings.Smtp.Password) } };
        }
    }

    public void Save(AppSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        var onDisk = settings with
        {
            Smtp = settings.Smtp with
            {
                Password = settings.Smtp.Password.Length == 0 ? "" : protector.Protect(settings.Smtp.Password),
            },
        };

        lock (this.gate)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(this.FilePath))!);
            var tempPath = this.FilePath + ".tmp";
            File.WriteAllText(tempPath, JsonSerializer.Serialize(onDisk, JsonOptions));
            File.Move(tempPath, this.FilePath, overwrite: true);
        }
    }

    private string Decrypt(string stored)
    {
        if (stored.Length == 0)
        {
            return "";
        }

        try
        {
            return protector.Unprotect(stored);
        }
        catch (Exception ex) when (ex is CryptographicException or FormatException)
        {
            // Typically: settings copied from another Windows account/machine.
            this.logger.LogWarning(ex, "Stored SMTP password could not be decrypted; it has to be entered again");
            return "";
        }
    }
}
