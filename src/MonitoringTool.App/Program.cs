using Microsoft.Extensions.Logging;
using MonitoringTool.App.Forms;
using MonitoringTool.App.Infrastructure;
using MonitoringTool.Core.Alerts;
using MonitoringTool.Core.Monitoring;
using MonitoringTool.Core.Settings;

namespace MonitoringTool.App;

internal static class Program
{
    [STAThread]
    private static void Main()
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
        var service = new MonitoringService(new PowerShellCollectorFactory(loggerFactory), emailSender, TimeProvider.System, loggerFactory.CreateLogger<MonitoringService>());

        Application.Run(new MainForm(service, store, settings, emailSender, loggerFactory.CreateLogger<MainForm>()));
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
