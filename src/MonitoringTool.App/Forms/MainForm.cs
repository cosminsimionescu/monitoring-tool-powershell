using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using MonitoringTool.App.Controls;
using MonitoringTool.App.Infrastructure;
using MonitoringTool.Core.Alerts;
using MonitoringTool.Core.Collection;
using MonitoringTool.Core.Models;
using MonitoringTool.Core.Monitoring;
using MonitoringTool.Core.Settings;

namespace MonitoringTool.App.Forms;

/// <summary>
/// Main window. Threading rule: this class only ever runs on the UI thread. All remote work is done
/// by <see cref="MonitoringService"/> on the thread pool, and its results arrive here through a
/// <see cref="Progress{T}"/> created on the UI thread, which marshals them back automatically.
/// Nothing here blocks (no .Result, .Wait(), Thread.Sleep) and no control is touched from another thread.
/// </summary>
internal sealed class MainForm : Form
{
    private const int MaxAlertLogEntries = 500;

    private readonly MonitoringService service;
    private readonly ISettingsStore settingsStore;
    private readonly IEmailSender emailSender;
    private readonly ILogger logger;
    private readonly Progress<MonitoringEvent> progress;
    private readonly BindingList<AlertLogEntry> alertLog = [];

    private readonly TextBox serverBox = new() { Width = 260, PlaceholderText = "server name or IP address" };
    private readonly Button startStopButton = new() { Text = "Start", AutoSize = true, MinimumSize = new Size(100, 0) };
    private readonly Label stateBadge = new() { AutoSize = true, Padding = new Padding(8, 3, 8, 3), Margin = new Padding(12, 5, 0, 0) };
    private readonly MetricCard cpuCard = new("CPU");
    private readonly MetricCard memoryCard = new("Memory");
    private readonly MetricCard serverCard = new("Server");
    private readonly GridView cpuGrid = new();
    private readonly GridView memoryGrid = new();
    private readonly GridView diskGrid = new();
    private readonly GridView alertGrid = new();
    private readonly CheckBox cpuAlertCheck = new() { Text = "CPU", AutoSize = true };
    private readonly CheckBox memoryAlertCheck = new() { Text = "Memory", AutoSize = true };
    private readonly CheckBox diskAlertCheck = new() { Text = "Disk space", AutoSize = true };
    private readonly CheckBox emailAlertCheck = new() { Text = "E-mail", AutoSize = true };
    private readonly ToolStripStatusLabel statusMessage = new() { Spring = true, TextAlign = ContentAlignment.MiddleLeft };
    private readonly ToolStripStatusLabel statusUpdated = new() { ForeColor = Theme.Muted };
    private readonly ToolStripMenuItem remoteSessionMenu = new("Open remote PowerShell session to server");

    private Font? boldGridFont;
    private AppSettings settings;
    private MonitoringState state = MonitoringState.Stopped;
    private string? lastProblem;
    private bool transitioning;
    private bool closeAfterStop;
    private bool suppressAlertToggles;
    private readonly bool demoMode;

    public MainForm(MonitoringService service, ISettingsStore settingsStore, AppSettings settings, IEmailSender emailSender, ILogger<MainForm> logger, bool demoMode = false)
    {
        this.demoMode = demoMode;
        this.service = service;
        this.settingsStore = settingsStore;
        this.settings = settings;
        this.emailSender = emailSender;
        this.logger = logger;

        // Must be created on the UI thread: callbacks are posted to this thread's SynchronizationContext.
        this.progress = new Progress<MonitoringEvent>(this.OnMonitoringEvent);

        this.Text = this.WindowTitle(null);
        this.AutoScaleMode = AutoScaleMode.Dpi;
        this.MinimumSize = new Size(980, 680);
        this.Size = new Size(1240, 820);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.BackColor = SystemColors.Window;

        this.BuildLayout();
        this.serverBox.Text = settings.LastServer;
        this.ApplySettingsToUi();
        this.SetState(MonitoringState.Stopped, "Enter a server name and press Start.");

        if (demoMode)
        {
            // Simulated data, no remote connection: start straight away so the UI can be explored.
            this.serverBox.Text = "demo-server";
            this.Shown += async (_, _) => await this.ToggleMonitoringAsync();
        }
    }

    private string WindowTitle(string? computer) =>
        (computer is null ? "Monitoring Tool" : $"Monitoring Tool — {computer}") + (this.demoMode ? "  [DEMO DATA]" : "");

    protected override async void OnFormClosing(FormClosingEventArgs e)
    {
        if (!this.closeAfterStop && this.service.IsRunning)
        {
            // Stop gracefully (closes the remote session) without blocking the UI thread,
            // then close for real. v1 called Environment.Exit from FormClosed instead.
            e.Cancel = true;
            this.closeAfterStop = true;
            this.Enabled = false;
            try
            {
                await this.service.StopAsync().WaitAsync(TimeSpan.FromSeconds(10));
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(ex, "Stopping on exit did not complete cleanly");
            }

            this.Close();
            return;
        }

        base.OnFormClosing(e);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.boldGridFont?.Dispose();
        }

        base.Dispose(disposing);
    }

    // ---------------------------------------------------------------- layout

    private void BuildLayout()
    {
        var menu = this.BuildMenu();
        var status = new StatusStrip { SizingGrip = true };
        status.Items.AddRange([this.statusMessage, this.statusUpdated]);

        // Connection bar
        var connectionBar = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, WrapContents = false, Padding = new Padding(6, 8, 6, 4) };
        connectionBar.Controls.Add(new Label { Text = "Server", AutoSize = true, Margin = new Padding(0, 7, 6, 0), Font = new Font(this.Font, FontStyle.Bold) });
        connectionBar.Controls.Add(this.serverBox);
        connectionBar.Controls.Add(this.startStopButton);
        connectionBar.Controls.Add(this.stateBadge);
        if (this.demoMode)
        {
            connectionBar.Controls.Add(new Label
            {
                Text = "DEMO MODE: simulated data, no server connection",
                AutoSize = true,
                ForeColor = Theme.Critical,
                Font = new Font(this.Font, FontStyle.Bold),
                Margin = new Padding(16, 7, 0, 0),
            });
        }
        this.serverBox.Margin = new Padding(0, 3, 6, 0);
        this.startStopButton.Click += async (_, _) => await this.ToggleMonitoringAsync();
        this.serverBox.KeyDown += async (_, e) =>
        {
            if (e.KeyCode == Keys.Enter && !this.service.IsRunning)
            {
                e.SuppressKeyPress = true;
                await this.ToggleMonitoringAsync();
            }
        };

        // Summary cards
        var cards = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1, AutoSize = true, Padding = new Padding(0) };
        for (var i = 0; i < 3; i++)
        {
            cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3f));
        }

        cards.Controls.Add(this.cpuCard, 0, 0);
        cards.Controls.Add(this.memoryCard, 1, 0);
        cards.Controls.Add(this.serverCard, 2, 0);

        // Metric grids
        this.cpuGrid.AddTextColumn("PID", nameof(ProcessUsage.Id), 22, alignRight: true)
                    .AddTextColumn("Process", nameof(ProcessUsage.Name), 58)
                    .AddTextColumn("CPU %", nameof(ProcessUsage.CpuPercent), 25, "N1", alignRight: true);
        this.memoryGrid.AddTextColumn("PID", nameof(ProcessUsage.Id), 22, alignRight: true)
                       .AddTextColumn("Process", nameof(ProcessUsage.Name), 58)
                       .AddTextColumn("Memory (MB)", nameof(ProcessUsage.WorkingSetMb), 30, "N0", alignRight: true);
        this.diskGrid.AddTextColumn("Drive", nameof(DiskUsage.Drive), 18)
                     .AddTextColumn("Label", nameof(DiskUsage.Label), 30)
                     .AddTextColumn("Size (GB)", nameof(DiskUsage.SizeGb), 25, "N1", alignRight: true)
                     .AddTextColumn("Free (GB)", nameof(DiskUsage.FreeGb), 25, "N1", alignRight: true)
                     .AddTextColumn("Free %", nameof(DiskUsage.FreePercent), 20, "N1", alignRight: true);
        this.cpuGrid.CellFormatting += this.OnCpuGridCellFormatting;
        this.diskGrid.CellFormatting += this.OnDiskGridCellFormatting;

        var grids = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1 };
        grids.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3f));
        grids.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3f));
        grids.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.4f));
        grids.Controls.Add(new Section("Top processes by CPU", this.cpuGrid), 0, 0);
        grids.Controls.Add(new Section("Top processes by memory", this.memoryGrid), 1, 0);
        grids.Controls.Add(new Section("Disk space", this.diskGrid), 2, 0);

        // Alerts
        this.alertGrid.AddTextColumn("Time", nameof(AlertLogEntry.Time), 18, "G")
                      .AddTextColumn("Status", nameof(AlertLogEntry.Severity), 14)
                      .AddTextColumn("Message", nameof(AlertLogEntry.Message), 70)
                      .AddTextColumn("Value", nameof(AlertLogEntry.Value), 12, alignRight: true);
        this.alertGrid.DataSource = this.alertLog;
        this.alertGrid.CellFormatting += this.OnAlertGridCellFormatting;

        var clearButton = new Button { Text = "Clear", AutoSize = true };
        clearButton.Click += (_, _) => this.alertLog.Clear();
        var alertsLabel = new Label { Text = "Notify on:", AutoSize = true, ForeColor = Theme.Muted };
        foreach (var check in new[] { this.cpuAlertCheck, this.memoryAlertCheck, this.diskAlertCheck, this.emailAlertCheck })
        {
            check.CheckedChanged += this.OnAlertToggleChanged;
        }

        var alertsSection = new Section("Alerts", this.alertGrid, alertsLabel, this.cpuAlertCheck, this.memoryAlertCheck, this.diskAlertCheck, this.emailAlertCheck, clearButton);

        // Resizable split between live data and the alert log.
        var split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterWidth = 6 };
        split.Panel1.Controls.Add(grids);
        split.Panel2.Controls.Add(alertsSection);
        this.Load += (_, _) => split.SplitterDistance = (int)(split.Height * 0.58);

        var root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3, Padding = new Padding(8, 0, 8, 4) };
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.Controls.Add(connectionBar, 0, 0);
        root.Controls.Add(cards, 0, 1);
        root.Controls.Add(split, 0, 2);

        this.Controls.Add(root);
        this.Controls.Add(status);
        this.Controls.Add(menu);
        this.MainMenuStrip = menu;
        root.BringToFront();
    }

    private MenuStrip BuildMenu()
    {
        var file = new ToolStripMenuItem("&File");
        file.DropDownItems.Add("Open &log folder", null, (_, _) => OpenFolder(FileLoggerProvider.DefaultDirectory));
        file.DropDownItems.Add(new ToolStripSeparator());
        file.DropDownItems.Add("E&xit", null, (_, _) => this.Close());

        var settingsMenu = new ToolStripMenuItem("&Settings");
        settingsMenu.DropDownItems.Add("&Alerts…", null, (_, _) => this.ShowSettings(SettingsPage.Alerts));
        settingsMenu.DropDownItems.Add("&E-mail server…", null, (_, _) => this.ShowSettings(SettingsPage.Email));
        settingsMenu.DropDownItems.Add("&Polling…", null, (_, _) => this.ShowSettings(SettingsPage.Polling));

        var tools = new ToolStripMenuItem("&Tools");
        tools.DropDownItems.Add("PowerShell console (administrator)", null, (_, _) => this.LaunchPowerShell(null));
        this.remoteSessionMenu.Click += (_, _) => this.LaunchPowerShell(this.serverBox.Text.Trim());
        tools.DropDownItems.Add(this.remoteSessionMenu);
        tools.DropDownOpening += (_, _) => this.remoteSessionMenu.Enabled = HostName.IsValid(this.serverBox.Text);

        var help = new ToolStripMenuItem("&Help");
        help.DropDownItems.Add("Remote server &setup…", null, (_, _) => ShowSetupHelp(this));

        var menu = new MenuStrip();
        menu.Items.AddRange([file, settingsMenu, tools, help]);
        return menu;
    }

    // ---------------------------------------------------------------- start / stop

    private async Task ToggleMonitoringAsync()
    {
        if (this.transitioning)
        {
            return;
        }

        this.transitioning = true;
        this.startStopButton.Enabled = false;
        try
        {
            if (this.service.IsRunning)
            {
                await this.service.StopAsync();
                return;
            }

            var server = this.serverBox.Text.Trim();
            if (!HostName.IsValid(server))
            {
                MessageBox.Show(this, "Enter a valid server name or IP address.", "Monitoring Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.serverBox.Focus();
                return;
            }

            this.ClearLiveData();
            this.SaveSettings(this.settings with { LastServer = server });
            this.serverBox.Enabled = false;
            this.UseWaitCursor = true;

            // Connecting can take many seconds on an unreachable host; the UI stays responsive meanwhile.
            await this.service.StartAsync(server, this.settings, this.progress);
            this.AddLogEntry("Info", $"Started monitoring {server}", "");
        }
        catch (Exception ex)
        {
            this.logger.LogWarning(ex, "Start/stop failed");
            this.AddLogEntry("Error", ex.Message, "");
            MessageBox.Show(this, ex.Message, "Could not start monitoring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        finally
        {
            this.UseWaitCursor = false;
            this.transitioning = false;
            this.startStopButton.Enabled = true;
            this.serverBox.Enabled = !this.service.IsRunning;
            this.startStopButton.Text = this.service.IsRunning ? "Stop" : "Start";
        }
    }

    // ---------------------------------------------------------------- monitoring events (UI thread)

    private void OnMonitoringEvent(MonitoringEvent e)
    {
        if (this.IsDisposed)
        {
            return;
        }

        switch (e)
        {
            case StatusChanged s:
                this.SetState(s.State, s.Message);
                break;
            case SummaryUpdated s:
                this.ShowSummary(s.Summary);
                break;
            case ProcessesUpdated p:
                this.cpuGrid.DataSource = p.Processes.ByCpu;
                this.memoryGrid.DataSource = p.Processes.ByMemory;
                this.cpuGrid.ClearSelection();
                this.memoryGrid.ClearSelection();
                this.MarkUpdated(p.Processes.Timestamp);
                break;
            case DisksUpdated d:
                this.diskGrid.DataSource = d.Disks.Disks;
                this.diskGrid.ClearSelection();
                this.MarkUpdated(d.Disks.Timestamp);
                break;
            case AlertsRaised a:
                foreach (var alert in a.Alerts)
                {
                    var severity = alert.Kind switch
                    {
                        AlertKind.Raised => "Alert",
                        AlertKind.Ongoing => "Still active",
                        _ => "Resolved",
                    };
                    this.AddLogEntry(severity, alert.Message, $"{alert.Value:0.#} %", alert.Timestamp.LocalDateTime);
                }

                break;
            case EmailResult { Succeeded: false } r:
                this.AddLogEntry("E-mail failed", r.Error ?? "Unknown error", "");
                break;
            case CollectionFailed:
            case EmailResult:
                // Status bar already shows collection problems; successful e-mails need no noise.
                break;
        }
    }

    private void ShowSummary(SystemSummary s)
    {
        this.cpuCard.SetValue(s.CpuPercent, this.settings.Alerts.CpuThresholdPercent, $"Alert at {this.settings.Alerts.CpuThresholdPercent} %");
        this.memoryCard.SetValue(s.MemoryPercent, this.settings.Alerts.MemoryThresholdPercent, $"{s.UsedMemoryGb:N1} GB of {s.TotalMemoryGb:N1} GB used");
        this.serverCard.SetText(s.ComputerName, $"Up {FormatUptime(s.Uptime)}");
        this.Text = this.WindowTitle(s.ComputerName);
        this.MarkUpdated(s.Timestamp);
    }

    private void SetState(MonitoringState newState, string? message)
    {
        var previous = this.state;
        this.state = newState;
        (this.stateBadge.Text, this.stateBadge.BackColor) = newState switch
        {
            MonitoringState.Connecting => ("Connecting…", Theme.Warning),
            MonitoringState.Running => ("Monitoring", Theme.Ok),
            MonitoringState.Degraded => ("Problems", Theme.Critical),
            MonitoringState.Stopping => ("Stopping…", Theme.Muted),
            _ => ("Stopped", Theme.Muted),
        };
        this.stateBadge.ForeColor = Color.White;
        this.statusMessage.Text = message ?? "";
        this.statusMessage.ForeColor = newState == MonitoringState.Degraded ? Theme.Critical : SystemColors.ControlText;

        // Log connectivity problems once per distinct problem rather than on every failed poll.
        if (newState == MonitoringState.Degraded && message != this.lastProblem)
        {
            this.AddLogEntry("Error", message ?? "Collection failed", "");
        }
        else if (newState == MonitoringState.Running && previous == MonitoringState.Degraded)
        {
            this.AddLogEntry("Info", "Collection recovered", "");
        }

        this.lastProblem = newState == MonitoringState.Degraded ? message : null;

        if (newState == MonitoringState.Stopped)
        {
            this.Text = this.WindowTitle(null);
            this.startStopButton.Text = "Start";
            this.serverBox.Enabled = !this.transitioning;
        }
    }

    private void MarkUpdated(DateTimeOffset timestamp) => this.statusUpdated.Text = $"Last update {timestamp.LocalDateTime:T}";

    private void ClearLiveData()
    {
        this.cpuCard.Clear();
        this.memoryCard.Clear();
        this.serverCard.Clear();
        this.cpuGrid.DataSource = null;
        this.memoryGrid.DataSource = null;
        this.diskGrid.DataSource = null;
        this.statusUpdated.Text = "";
    }

    private void AddLogEntry(string severity, string message, string value, DateTime? time = null)
    {
        this.alertLog.Insert(0, new AlertLogEntry(time ?? DateTime.Now, severity, message, value));
        while (this.alertLog.Count > MaxAlertLogEntries)
        {
            this.alertLog.RemoveAt(this.alertLog.Count - 1);
        }
    }

    // ---------------------------------------------------------------- grid colouring

    private void OnCpuGridCellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.CellStyle is not null && e.Value is double cpu && this.cpuGrid.Columns[e.ColumnIndex].Name == nameof(ProcessUsage.CpuPercent))
        {
            e.CellStyle.ForeColor = Theme.ForUsage(cpu, this.settings.Alerts.CpuThresholdPercent);
        }
    }

    private void OnDiskGridCellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.CellStyle is not null && e.Value is double free && this.diskGrid.Columns[e.ColumnIndex].Name == nameof(DiskUsage.FreePercent))
        {
            e.CellStyle.ForeColor = Theme.ForFree(free, this.settings.Alerts.DiskFreeThresholdPercent);
            e.CellStyle.Font = this.boldGridFont ??= new Font(this.diskGrid.Font, FontStyle.Bold);
        }
    }

    private void OnAlertGridCellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.CellStyle is null || e.Value is not string text || this.alertGrid.Columns[e.ColumnIndex].Name != nameof(AlertLogEntry.Severity))
        {
            return;
        }

        e.CellStyle.ForeColor = text switch
        {
            "Alert" or "Error" or "E-mail failed" => Theme.Critical,
            "Still active" => Theme.Warning,
            "Resolved" => Theme.Ok,
            _ => Theme.Muted,
        };
    }

    // ---------------------------------------------------------------- settings

    private void ShowSettings(SettingsPage page)
    {
        using var dialog = new SettingsForm(this.settings, this.emailSender, page);
        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            this.SaveSettings(dialog.Result);
            this.ApplySettingsToUi();
        }
    }

    private void OnAlertToggleChanged(object? sender, EventArgs e)
    {
        if (this.suppressAlertToggles)
        {
            return;
        }

        var alerts = this.settings.Alerts with
        {
            CpuEnabled = this.cpuAlertCheck.Checked,
            MemoryEnabled = this.memoryAlertCheck.Checked,
            DiskEnabled = this.diskAlertCheck.Checked,
            EmailEnabled = this.emailAlertCheck.Checked,
        };

        if (alerts.EmailEnabled && (!this.settings.Smtp.IsConfigured || alerts.RecipientList.Count == 0))
        {
            this.suppressAlertToggles = true;
            this.emailAlertCheck.Checked = false;
            this.suppressAlertToggles = false;
            MessageBox.Show(this, "Configure the e-mail server and at least one recipient first.", "E-mail alerts", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.ShowSettings(this.settings.Smtp.IsConfigured ? SettingsPage.Alerts : SettingsPage.Email);
            return;
        }

        this.SaveSettings(this.settings with { Alerts = alerts });
    }

    private void ApplySettingsToUi()
    {
        this.suppressAlertToggles = true;
        this.cpuAlertCheck.Checked = this.settings.Alerts.CpuEnabled;
        this.memoryAlertCheck.Checked = this.settings.Alerts.MemoryEnabled;
        this.diskAlertCheck.Checked = this.settings.Alerts.DiskEnabled;
        this.emailAlertCheck.Checked = this.settings.Alerts.EmailEnabled;
        this.suppressAlertToggles = false;
        this.cpuGrid.Invalidate();
        this.diskGrid.Invalidate();
    }

    private void SaveSettings(AppSettings newSettings)
    {
        this.settings = newSettings;
        this.service.UpdateSettings(newSettings);
        try
        {
            this.settingsStore.Save(newSettings);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            this.logger.LogError(ex, "Saving settings failed");
            this.statusMessage.Text = "Settings could not be saved: " + ex.Message;
        }
    }

    // ---------------------------------------------------------------- tools

    private void LaunchPowerShell(string? remoteServer)
    {
        var exe = Path.Combine(Environment.SystemDirectory, @"WindowsPowerShell\v1.0\powershell.exe");
        var startInfo = new ProcessStartInfo(exe) { UseShellExecute = true };
        if (remoteServer is null)
        {
            startInfo.Verb = "runas";
        }
        else if (HostName.IsValid(remoteServer))
        {
            // Host name is validated, so it cannot carry extra PowerShell.
            startInfo.ArgumentList.Add("-NoExit");
            startInfo.ArgumentList.Add("-Command");
            startInfo.ArgumentList.Add($"Enter-PSSession -ComputerName '{remoteServer}'");
        }
        else
        {
            return;
        }

        try
        {
            Process.Start(startInfo)?.Dispose();
        }
        catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode == 1223)
        {
            // User cancelled the UAC prompt.
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Could not start PowerShell", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private static void OpenFolder(string path)
    {
        Directory.CreateDirectory(path);
        Process.Start(new ProcessStartInfo(path) { UseShellExecute = true })?.Dispose();
    }

    private static void ShowSetupHelp(IWin32Window owner) => MessageBox.Show(
        owner,
        "The monitored server must accept PowerShell remoting (WinRM):" + Environment.NewLine + Environment.NewLine +
        "1. On the server, in an elevated PowerShell:" + Environment.NewLine +
        "      Enable-PSRemoting -Force" + Environment.NewLine + Environment.NewLine +
        "2. If the server is not in the same domain, on THIS machine (elevated):" + Environment.NewLine +
        "      Set-Item WSMan:\\localhost\\Client\\TrustedHosts -Value '<server>' -Concatenate" + Environment.NewLine + Environment.NewLine +
        "3. Your account needs to be an administrator or a member of 'Remote Management Users' on the server." + Environment.NewLine + Environment.NewLine +
        "Test with:  Test-WSMan <server>   and   Enter-PSSession <server>",
        "Remote server setup",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information);

    private static string FormatUptime(TimeSpan uptime) =>
        uptime.TotalDays >= 1 ? $"{(int)uptime.TotalDays} d {uptime.Hours} h" : $"{uptime.Hours} h {uptime.Minutes} min";
}
