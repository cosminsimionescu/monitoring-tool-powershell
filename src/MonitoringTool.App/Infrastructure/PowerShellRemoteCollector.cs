using System.Globalization;
using System.Management.Automation;
using System.Management.Automation.Remoting;
using System.Management.Automation.Runspaces;
using System.Reflection;
using Microsoft.Extensions.Logging;
using MonitoringTool.Core.Collection;
using MonitoringTool.Core.Models;

namespace MonitoringTool.App.Infrastructure;

internal sealed class PowerShellCollectorFactory(ILoggerFactory loggerFactory) : IMetricsCollectorFactory
{
    public IMetricsCollector Create(string target, TimeSpan commandTimeout) =>
        new PowerShellRemoteCollector(target, commandTimeout, loggerFactory.CreateLogger<PowerShellRemoteCollector>());
}

/// <summary>
/// Collects metrics over WinRM using one long-lived remote runspace pool.
/// </summary>
/// <remarks>
/// v1 created a local runspace and ran New-PSSession / Remove-PSSession for every single metric,
/// i.e. a full WinRM handshake every few seconds, and blocked threads on <c>.Result</c>.
/// Here the connection is opened once, reused by all loops (up to <see cref="MaxConcurrentCommands"/>
/// in parallel), re-opened automatically if it breaks, and every call is truly asynchronous,
/// cancellable and bounded by a timeout.
/// </remarks>
internal sealed class PowerShellRemoteCollector(string target, TimeSpan commandTimeout, ILogger<PowerShellRemoteCollector> logger)
    : IMetricsCollector
{
    private const int MaxConcurrentCommands = 3;

    private static readonly string SummaryScript = LoadScript("GetSummary.ps1");
    private static readonly string ProcessesScript = LoadScript("GetTopProcesses.ps1");
    private static readonly string DisksScript = LoadScript("GetDisks.ps1");

    private readonly SemaphoreSlim connectGate = new(1, 1);
    private RunspacePool? pool;
    private bool disposed;

    public string Target { get; } = HostName.IsValid(target)
        ? target.Trim()
        : throw new ArgumentException($"'{target}' is not a valid host name or IP address.", nameof(target));

    public Task ConnectAsync(CancellationToken cancellationToken) => this.GetPoolAsync(cancellationToken);

    public async Task<SystemSummary> GetSummaryAsync(CancellationToken cancellationToken)
    {
        var result = await this.InvokeAsync(SummaryScript, null, cancellationToken).ConfigureAwait(false);
        var o = (result.Count > 0 ? result[0] : null) ?? throw new InvalidOperationException("The server returned no CPU/memory data.");

        var totalGb = GetDouble(o, "TotalMemoryKb") / 1024 / 1024;
        var freeGb = GetDouble(o, "FreeMemoryKb") / 1024 / 1024;
        return new SystemSummary(
            GetString(o, "ComputerName") is { Length: > 0 } name ? name : this.Target,
            Math.Round(GetDouble(o, "CpuPercent"), 2),
            Math.Round(totalGb, 2),
            Math.Round(totalGb - freeGb, 2),
            TimeSpan.FromSeconds(GetDouble(o, "UptimeSeconds")),
            DateTimeOffset.Now);
    }

    public async Task<ProcessesSnapshot> GetTopProcessesAsync(int count, CancellationToken cancellationToken)
    {
        var result = await this.InvokeAsync(ProcessesScript, new Dictionary<string, object> { ["Top"] = count }, cancellationToken).ConfigureAwait(false);

        var byCpu = new List<ProcessUsage>();
        var byMemory = new List<ProcessUsage>();
        foreach (var o in result)
        {
            var process = new ProcessUsage((int)GetDouble(o, "Id"), GetString(o, "Name"), GetDouble(o, "CpuPercent"), GetDouble(o, "WorkingSetMb"));
            (GetString(o, "Kind") == "Cpu" ? byCpu : byMemory).Add(process);
        }

        return new ProcessesSnapshot(byCpu, byMemory, DateTimeOffset.Now);
    }

    public async Task<DisksSnapshot> GetDisksAsync(CancellationToken cancellationToken)
    {
        var result = await this.InvokeAsync(DisksScript, null, cancellationToken).ConfigureAwait(false);
        var disks = result
            .Select(o => new DiskUsage(GetString(o, "Drive"), GetString(o, "Label"), GetDouble(o, "SizeGb"), GetDouble(o, "FreeGb")))
            .OrderBy(d => d.Drive, StringComparer.OrdinalIgnoreCase)
            .ToList();
        return new DisksSnapshot(disks, DateTimeOffset.Now);
    }

    public async ValueTask DisposeAsync()
    {
        if (this.disposed)
        {
            return;
        }

        this.disposed = true;
        var toClose = Interlocked.Exchange(ref this.pool, null);
        if (toClose is not null)
        {
            // Closing a remote pool talks to the server and can hang on a dead connection;
            // never let that block the caller (in v1 such calls ran on, and froze, the UI thread).
            try
            {
                await Task.Run(() => ClosePool(toClose)).WaitAsync(TimeSpan.FromSeconds(5)).ConfigureAwait(false);
            }
            catch (TimeoutException)
            {
                logger.LogWarning("Closing the remote session to {Target} timed out", this.Target);
            }
        }

        this.connectGate.Dispose();
    }

    private async Task<IReadOnlyList<PSObject>> InvokeAsync(string script, Dictionary<string, object>? parameters, CancellationToken cancellationToken)
    {
        var runspacePool = await this.GetPoolAsync(cancellationToken).ConfigureAwait(false);

        using var ps = PowerShell.Create();
        ps.RunspacePool = runspacePool;
        ps.AddScript(script);
        if (parameters is not null)
        {
            ps.AddParameters(parameters);
        }

        using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeoutCts.CancelAfter(commandTimeout);
        using var registration = timeoutCts.Token.Register(() =>
        {
            try
            {
                ps.BeginStop(null, null);
            }
            catch (Exception ex) when (ex is ObjectDisposedException or InvalidOperationException)
            {
                // Already finished.
            }
        });

        PSDataCollection<PSObject> output;
        try
        {
            output = await ps.InvokeAsync().ConfigureAwait(false);
        }
        catch (PipelineStoppedException) when (timeoutCts.IsCancellationRequested)
        {
            cancellationToken.ThrowIfCancellationRequested();
            throw new TimeoutException($"The command did not complete within {commandTimeout.TotalSeconds:0} seconds.");
        }
        catch (Exception ex) when (ex is PSRemotingTransportException or InvalidRunspacePoolStateException)
        {
            // The next call will reconnect (see GetPoolAsync).
            throw new InvalidOperationException($"Connection to {this.Target} lost: {ex.Message}", ex);
        }

        if (output.Count == 0 && ps.HadErrors && ps.Streams.Error.Count > 0)
        {
            var error = ps.Streams.Error[0];
            throw new InvalidOperationException(error.Exception?.Message ?? error.ToString());
        }

        return [.. output];
    }

    private async Task<RunspacePool> GetPoolAsync(CancellationToken cancellationToken)
    {
        ObjectDisposedException.ThrowIf(this.disposed, this);

        var current = this.pool;
        if (current?.RunspacePoolStateInfo.State == RunspacePoolState.Opened)
        {
            return current;
        }

        await this.connectGate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            current = this.pool;
            if (current?.RunspacePoolStateInfo.State == RunspacePoolState.Opened)
            {
                return current;
            }

            if (current is not null)
            {
                logger.LogInformation("Remote session to {Target} is {State}; reconnecting", this.Target, current.RunspacePoolStateInfo.State);
                this.pool = null;
                _ = Task.Run(() => ClosePool(current), CancellationToken.None);
            }

            var connectionInfo = new WSManConnectionInfo
            {
                ComputerName = this.Target,
                OpenTimeout = (int)TimeSpan.FromSeconds(30).TotalMilliseconds,
                OperationTimeout = (int)commandTimeout.Add(TimeSpan.FromSeconds(30)).TotalMilliseconds,
            };

            var newPool = RunspaceFactory.CreateRunspacePool(1, MaxConcurrentCommands, connectionInfo);
            var open = Task.Factory.FromAsync(newPool.BeginOpen, newPool.EndOpen, null);
            try
            {
                await open.WaitAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Let the pending open finish in the background, then discard it.
                _ = open.ContinueWith(_ => ClosePool(newPool), CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.Default);
                throw;
            }
            catch (Exception ex)
            {
                ClosePool(newPool);
                throw new InvalidOperationException(DescribeConnectError(this.Target, ex), ex);
            }

            logger.LogInformation("Connected to {Target}", this.Target);
            this.pool = newPool;
            return newPool;
        }
        finally
        {
            this.connectGate.Release();
        }
    }

    private static string DescribeConnectError(string target, Exception ex)
    {
        var root = ex is AggregateException { InnerException: { } inner } ? inner : ex;
        return $"Could not open a PowerShell remoting session to {target}. {root.Message}" + Environment.NewLine +
               "Check that WinRM is enabled on the server (Enable-PSRemoting) and that it is reachable/trusted from this machine.";
    }

    private static void ClosePool(RunspacePool runspacePool)
    {
        try
        {
            if (runspacePool.RunspacePoolStateInfo.State == RunspacePoolState.Opened)
            {
                runspacePool.Close();
            }
        }
        catch (Exception ex) when (ex is PSRemotingTransportException or InvalidRunspacePoolStateException or InvalidOperationException)
        {
            // Best effort.
        }
        finally
        {
            runspacePool.Dispose();
        }
    }

    private static double GetDouble(PSObject o, string name)
    {
        var value = o.Properties[name]?.Value;
        if (value is PSObject wrapped)
        {
            value = wrapped.BaseObject;
        }

        return value is null ? 0 : Convert.ToDouble(value, CultureInfo.InvariantCulture);
    }

    private static string GetString(PSObject o, string name) => o.Properties[name]?.Value?.ToString() ?? "";

    private static string LoadScript(string fileName)
    {
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Scripts." + fileName)
            ?? throw new InvalidOperationException($"Embedded script {fileName} is missing.");
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}
