using System.Collections.Concurrent;
using System.Globalization;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;

namespace MonitoringTool.App.Infrastructure;

/// <summary>
/// Minimal daily-rolling file logger. Writes happen on a background task so logging
/// never blocks the UI thread.
/// </summary>
internal sealed class FileLoggerProvider : ILoggerProvider
{
    private readonly ConcurrentDictionary<string, FileLogger> loggers = new();
    private readonly Channel<string> queue = Channel.CreateBounded<string>(new BoundedChannelOptions(10_000) { FullMode = BoundedChannelFullMode.DropOldest, SingleReader = true });
    private readonly Task writer;

    public FileLoggerProvider(string directory)
    {
        this.Directory = directory;
        System.IO.Directory.CreateDirectory(directory);
        this.writer = Task.Run(this.WriteLoopAsync);
    }

    public string Directory { get; }

    public static string DefaultDirectory =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MonitoringTool", "logs");

    public ILogger CreateLogger(string categoryName) => this.loggers.GetOrAdd(categoryName, name => new FileLogger(name, this.queue.Writer));

    public void Dispose()
    {
        this.queue.Writer.TryComplete();
        this.writer.Wait(TimeSpan.FromSeconds(2));
    }

    private async Task WriteLoopAsync()
    {
        await foreach (var line in this.queue.Reader.ReadAllAsync().ConfigureAwait(false))
        {
            try
            {
                var path = Path.Combine(this.Directory, $"monitoring-{DateTime.Now:yyyyMMdd}.log");
                await File.AppendAllTextAsync(path, line + Environment.NewLine).ConfigureAwait(false);
            }
            catch (IOException)
            {
                // Logging must never take the application down.
            }
        }
    }

    private sealed class FileLogger(string category, ChannelWriter<string> writer) : ILogger
    {
        public IDisposable? BeginScope<TState>(TState state)
            where TState : notnull => null;

        public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel.Information;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!this.IsEnabled(logLevel))
            {
                return;
            }

            var line = string.Create(CultureInfo.InvariantCulture, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{logLevel}] {category}: {formatter(state, exception)}");
            if (exception is not null)
            {
                line += Environment.NewLine + exception;
            }

            writer.TryWrite(line);
        }
    }
}
