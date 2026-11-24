# Monitoring Tool (PowerShell)

A Windows desktop tool that monitors a remote Windows server over PowerShell remoting (WinRM):
CPU and memory usage, top processes by CPU and by memory, free disk space, with in-app and
e-mail alerts.

Built on **.NET 10** (WinForms) and the **PowerShell 7.6 SDK**.

![Monitoring Tool main window (demo mode, simulated data)](docs/screenshot-demo.png)

*Screenshot taken with `MonitoringTool.exe --demo`, which shows simulated data without connecting
to a server. It was captured under Wine (no window frame, substitute font), so it looks slightly different on Windows.*

## Requirements

**On the machine running the tool**

- Windows 10/11 or Windows Server 2016+
- [.NET 10 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/10.0)

**On each monitored server** (elevated PowerShell):

```powershell
Enable-PSRemoting -Force
```

If the server is not in the same Active Directory domain as the machine running the tool,
add it to the client's trusted hosts (elevated, on the machine running the tool):

```powershell
Set-Item WSMan:\localhost\Client\TrustedHosts -Value 'server01' -Concatenate
```

Your account must be an administrator, or a member of *Remote Management Users*, on the
server. `Test-WSMan server01` and `Enter-PSSession server01` should both work before you
start the tool. The same text is under **Help → Remote server setup** in the app.

## Usage

1. Type a server name or IP address and press **Start** (or Enter).
2. CPU and memory refresh every 10 s, processes every 15 s, disks every 2 min (configurable
   under **Settings → Polling**).
3. Use the **Notify on** check boxes above the alert list to turn alert types on or off;
   thresholds and repeat intervals are under **Settings → Alerts**.
4. For e-mail alerts, configure **Settings → E-mail server** and use **Send test e-mail**.

To try the UI without a server, start it with `MonitoringTool.exe --demo`
(or `dotnet run --project src/MonitoringTool.App -- --demo`).

An alert is shown when a threshold is crossed. While the condition lasts it is repeated every
*N* minutes (separately for in-app and e-mail), and a *Resolved* entry follows when the value
goes back below the threshold.

Settings are stored in `%LOCALAPPDATA%\MonitoringTool\settings.json`; the SMTP password in that
file is encrypted with Windows DPAPI for the current user. Logs are written to
`%LOCALAPPDATA%\MonitoringTool\logs` (**File → Open log folder**).

When v2 starts for the first time, it imports the v1 `AlertsConfigFile.txt` and `configSMTP.txt`
files if it finds them next to the executable or in the working directory. Once the settings
are imported, you can delete the old files. `configSMTP.txt` contains your SMTP password in
plain text.

## Building

```powershell
dotnet build MonitoringTool.slnx
dotnet test --solution MonitoringTool.slnx
dotnet publish src/MonitoringTool.App -c Release -r win-x64 --self-contained false -o publish
```

The app project targets `net10.0-windows`. It can be *built* on Linux/macOS
(`EnableWindowsTargeting`), but it only runs on Windows. The core library and the tests run
on any OS.

## Project layout

```
src/
  MonitoringTool.Core/        platform-independent logic (unit tested)
    Models/                   SystemSummary, ProcessesSnapshot, DisksSnapshot …
    Settings/                 AppSettings records, JSON store, v1 importer
    Alerts/                   AlertEvaluator (raise / repeat / resolve), MailKit e-mail sender
    Monitoring/               MonitoringService: polling loops, alert dispatch, e-mail queue
    Collection/               IMetricsCollector abstraction, host name validation
  MonitoringTool.App/         WinForms UI + Windows-specific infrastructure
    Forms/                    MainForm, SettingsForm
    Controls/                 MetricCard, UsageBar, GridView, Section, Theme
    Infrastructure/           PowerShell remoting collector, DPAPI, file logger
    Scripts/                  the .ps1 scripts executed on the server (embedded resources)
tests/
  MonitoringTool.Core.Tests/  xUnit v3
```

### Threading model

- The UI thread only handles UI. It never blocks: there are no `.Result`, `.Wait()` or
  `Thread.Sleep` calls in the app.
- `MonitoringService` runs one async loop per metric group on the thread pool using
  `PeriodicTimer`. A loop waits for its current request to finish before it waits for the next
  tick. A slow server therefore never causes overlapping requests. Ticks missed while a request
  was running collapse into one catch-up poll.
- Results reach the UI through a `Progress<T>` created on the UI thread, so every control
  update runs on the UI thread. Cross-thread checks stay enabled.
- The collector keeps a single WinRM runspace pool open for the whole session. It does not
  create a new `New-PSSession` for every metric. If the connection breaks, the collector
  reconnects. Every remote call can be cancelled and has a timeout.
- E-mails are sent from a background queue with async MailKit. A slow SMTP server never
  delays metric collection.
