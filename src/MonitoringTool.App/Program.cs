using Microsoft.Extensions.Logging;
using MonitoringTool.App.Forms;
using MonitoringTool.App.Infrastructure;
using MonitoringTool.Core.Alerts;
using MonitoringTool.Core.Checks;
using MonitoringTool.Core.Collection;
using MonitoringTool.Core.Monitoring;
using MonitoringTool.Core.Settings;

namespace MonitoringTool.App;

internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        // Generated from the Application* properties in the .csproj (DPI mode, visual styles, default font).
        ApplicationConfiguration.Initialize();

        using var fileLogger = new FileLoggerProvider(FileLoggerProvider.DefaultDirectory);
        using var loggerFactory = LoggerFactory.Create(builder => builder.SetMinimumLevel(LogLevel.Information).AddProvider(fileLogger));
        var log = loggerFactory.CreateLogger("MonitoringTool");

        // Last-chance handlers: log and keep running instead of silently dying (v1 swallowed or crashed).
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (_, e) =>
        {
            log.LogError(e.Exception, "Unhandled UI exception");
            MessageBox.Show(e.Exception.Message, "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        };
        AppDomain.CurrentDomain.UnhandledException += (_, e) => log.LogCritical(e.ExceptionObject as Exception, "Unhandled exception");
        TaskScheduler.UnobservedTaskException += (_, e) =>
        {
            log.LogError(e.Exception, "Unobserved task exception");
            e.SetObserved();
        };

        var store = new JsonSettingsStore(JsonSettingsStore.DefaultPath, new DpapiSecretProtector(), loggerFactory.CreateLogger<JsonSettingsStore>());
        var settings = LoadSettings(store, log);

        var emailSender = new MailKitEmailSender();
        var demo = args.Contains("--demo", StringComparer.OrdinalIgnoreCase);
        IMetricsCollectorFactory collectors = demo ? new DemoCollectorFactory() : new PowerShellCollectorFactory(loggerFactory);
        var service = new MonitoringService(collectors, emailSender, TimeProvider.System, loggerFactory.CreateLogger<MonitoringService>());

        // Built-in checks ship next to the exe; the user's folder can add checks or override them by name.
        var builtInChecks = Path.Combine(AppContext.BaseDirectory, "Checks");
        var userChecks = PrepareUserChecksFolder(builtInChecks, log);
        CheckLibrary LoadChecks() => CheckLibrary.Load([builtInChecks, userChecks]);

        Application.Run(new MainForm(service, store, settings, emailSender, loggerFactory.CreateLogger<MainForm>(), LoadChecks, userChecks, demo));
    }

    private static string PrepareUserChecksFolder(string builtInChecks, ILogger log)
    {
        var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MonitoringTool", "checks");
        try
        {
            Directory.CreateDirectory(folder);
            var template = Path.Combine(builtInChecks, "_template.ps1");
            var target = Path.Combine(folder, "_template.ps1");
            if (File.Exists(template) && !File.Exists(target))
            {
                File.Copy(template, target);
            }
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            log.LogWarning(ex, "Could not prepare the custom checks folder {Folder}", folder);
        }

        return folder;
    }

    private static AppSettings LoadSettings(JsonSettingsStore store, ILogger log)
    {
        if (File.Exists(store.FilePath))
        {
            return store.Load();
        }

        // First run of v2: carry over v1's plain-text configuration if it is next to the exe / in the working directory.
        var legacy = LegacySettingsImporter.TryImport([AppContext.BaseDirectory, Environment.CurrentDirectory]);
        if (legacy is null)
        {
            return new AppSettings();
        }

        log.LogInformation("Imported v1 settings into {Path}", store.FilePath);
        try
        {
            store.Save(legacy);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            log.LogError(ex, "Could not save imported settings");
        }

        return legacy;
    }
}
