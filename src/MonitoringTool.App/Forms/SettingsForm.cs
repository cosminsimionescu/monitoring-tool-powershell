using MonitoringTool.App.Controls;
using MonitoringTool.Core.Alerts;
using MonitoringTool.Core.Settings;

namespace MonitoringTool.App.Forms;

internal enum SettingsPage
{
    Alerts,
    Email,
    Polling,
}

/// <summary>
/// One modal dialog for all settings. Numeric inputs are NumericUpDowns with min/max, so the
/// v1 class of bugs (free-text numbers, Convert.ToInt32 crashes, operator-precedence mistakes in
/// validation) cannot happen. The dialog returns a new immutable <see cref="AppSettings"/>.
/// </summary>
internal sealed class SettingsForm : Form
{
    private readonly AppSettings original;
    private readonly IEmailSender emailSender;
    private readonly CancellationTokenSource lifetime = new();
    private readonly TabControl tabs = new() { Dock = DockStyle.Fill };

    // Alerts
    private readonly CheckBox cpuEnabled = new() { Text = "Alert on high CPU usage", AutoSize = true };
    private readonly NumericUpDown cpuThreshold = Percent();
    private readonly CheckBox memoryEnabled = new() { Text = "Alert on high memory usage", AutoSize = true };
    private readonly NumericUpDown memoryThreshold = Percent();
    private readonly CheckBox diskEnabled = new() { Text = "Alert on low free disk space", AutoSize = true };
    private readonly NumericUpDown diskThreshold = Percent(max: 99);
    private readonly NumericUpDown inAppRepeat = Number(1, 1440);
    private readonly CheckBox emailEnabled = new() { Text = "Also send alerts by e-mail", AutoSize = true };
    private readonly TextBox recipients = new() { Width = 300 };
    private readonly NumericUpDown emailRepeat = Number(1, 10080);

    // SMTP
    private readonly TextBox smtpHost = new() { Width = 260 };
    private readonly NumericUpDown smtpPort = Number(1, 65535);
    private readonly ComboBox smtpSecurity = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 160 };
    private readonly TextBox smtpUser = new() { Width = 260 };
    private readonly TextBox smtpPassword = new() { Width = 260, UseSystemPasswordChar = true };
    private readonly TextBox smtpFrom = new() { Width = 260, PlaceholderText = "defaults to the user name" };
    private readonly Button testEmailButton = new() { Text = "Send test e-mail", AutoSize = true };

    // Polling
    private readonly NumericUpDown summaryInterval = Number(2, 3600);
    private readonly NumericUpDown processesInterval = Number(2, 3600);
    private readonly NumericUpDown disksInterval = Number(10, 86400);
    private readonly NumericUpDown topCount = Number(1, 100);
    private readonly NumericUpDown commandTimeout = Number(5, 600);

    public SettingsForm(AppSettings settings, IEmailSender emailSender, SettingsPage page)
    {
        this.original = settings;
        this.emailSender = emailSender;
        this.Result = settings;

        this.Text = "Settings";
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.ShowInTaskbar = false;
        this.StartPosition = FormStartPosition.CenterParent;
        this.AutoScaleMode = AutoScaleMode.Dpi;
        this.AutoSize = true;
        this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        this.MinimumSize = new Size(560, 0);
        this.Padding = new Padding(10);

        this.smtpSecurity.Items.AddRange([.. Enum.GetValues<SmtpSecurity>().Cast<object>()]);

        this.tabs.TabPages.Add(this.BuildAlertsPage());
        this.tabs.TabPages.Add(this.BuildEmailPage());
        this.tabs.TabPages.Add(this.BuildPollingPage());
        this.tabs.SelectedIndex = (int)page;

        var ok = new Button { Text = "OK", AutoSize = true, MinimumSize = new Size(90, 0) };
        var cancel = new Button { Text = "Cancel", AutoSize = true, MinimumSize = new Size(90, 0), DialogResult = DialogResult.Cancel };
        ok.Click += (_, _) => this.Accept();
        this.AcceptButton = ok;
        this.CancelButton = cancel;

        var buttons = new FlowLayoutPanel { FlowDirection = FlowDirection.RightToLeft, Dock = DockStyle.Fill, AutoSize = true, Padding = new Padding(0, 8, 0, 0) };
        buttons.Controls.AddRange([cancel, ok]);

        var root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, AutoSize = true };
        this.tabs.Dock = DockStyle.None;
        this.tabs.Size = new Size(540, 360);
        this.tabs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
        root.Controls.Add(this.tabs, 0, 0);
        root.Controls.Add(buttons, 0, 1);
        this.Controls.Add(root);

        this.LoadValues(settings);
        this.emailEnabled.CheckedChanged += (_, _) => this.UpdateEnabledState();
        this.cpuEnabled.CheckedChanged += (_, _) => this.UpdateEnabledState();
        this.memoryEnabled.CheckedChanged += (_, _) => this.UpdateEnabledState();
        this.diskEnabled.CheckedChanged += (_, _) => this.UpdateEnabledState();
        this.testEmailButton.Click += async (_, _) => await this.SendTestEmailAsync();
        this.UpdateEnabledState();
    }

    public AppSettings Result { get; private set; }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        this.lifetime.Cancel();
        this.lifetime.Dispose();
        base.OnFormClosed(e);
    }

    private static NumericUpDown Number(int min, int max) => new() { Minimum = min, Maximum = max, Width = 90, TextAlign = HorizontalAlignment.Right };

    private static NumericUpDown Percent(int max = 100) => Number(1, max);

    private static TableLayoutPanel Page()
    {
        var table = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, AutoScroll = true, Padding = new Padding(10) };
        table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        return table;
    }

    private static void Row(TableLayoutPanel table, string? label, Control control, string? hint = null)
    {
        var row = table.RowCount++;
        table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        if (label is not null)
        {
            table.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left, Margin = new Padding(3, 7, 12, 3) }, 0, row);
        }

        Control cell = control;
        if (hint is not null)
        {
            var flow = new FlowLayoutPanel { AutoSize = true, WrapContents = false, Margin = Padding.Empty };
            flow.Controls.Add(control);
            flow.Controls.Add(new Label { Text = hint, AutoSize = true, ForeColor = Theme.Muted, Margin = new Padding(6, 7, 0, 0) });
            cell = flow;
        }

        if (label is null)
        {
            table.Controls.Add(cell, 0, row);
            table.SetColumnSpan(cell, 2);
        }
        else
        {
            table.Controls.Add(cell, 1, row);
        }
    }

    private static void Spacer(TableLayoutPanel table)
    {
        var row = table.RowCount++;
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));
        table.Controls.Add(new Label { AutoSize = false, Height = 1 }, 0, row);
    }

    private TabPage BuildAlertsPage()
    {
        var t = Page();
        Row(t, null, this.cpuEnabled);
        Row(t, "CPU usage at or above", this.cpuThreshold, "%");
        Row(t, null, this.memoryEnabled);
        Row(t, "Memory usage at or above", this.memoryThreshold, "%");
        Row(t, null, this.diskEnabled);
        Row(t, "Free space below", this.diskThreshold, "% on any fixed drive");
        Spacer(t);
        Row(t, "Repeat in-app alert every", this.inAppRepeat, "minutes while the condition lasts");
        Spacer(t);
        Row(t, null, this.emailEnabled);
        Row(t, "Recipients", this.recipients, "separate with ;");
        Row(t, "Repeat e-mail every", this.emailRepeat, "minutes while the condition lasts");
        return new TabPage("Alerts") { Controls = { t }, UseVisualStyleBackColor = true };
    }

    private TabPage BuildEmailPage()
    {
        var t = Page();
        Row(t, "SMTP server", this.smtpHost);
        Row(t, "Port", this.smtpPort, "587 = STARTTLS, 465 = SSL");
        Row(t, "Security", this.smtpSecurity);
        Row(t, "User name", this.smtpUser);
        Row(t, "Password", this.smtpPassword, "app password");
        Row(t, "Sender address", this.smtpFrom);
        Spacer(t);
        Row(t, null, new Label
        {
            AutoSize = true,
            MaximumSize = new Size(480, 0),
            ForeColor = Theme.Muted,
            Text = "The password is encrypted with Windows DPAPI for your user account before it is saved.",
        });
        Row(t, null, this.testEmailButton);
        return new TabPage("E-mail server") { Controls = { t }, UseVisualStyleBackColor = true };
    }

    private TabPage BuildPollingPage()
    {
        var t = Page();
        Row(t, "Refresh CPU / memory every", this.summaryInterval, "seconds");
        Row(t, "Refresh processes every", this.processesInterval, "seconds");
        Row(t, "Refresh disks every", this.disksInterval, "seconds");
        Row(t, "Processes to show", this.topCount);
        Row(t, "Remote command timeout", this.commandTimeout, "seconds (applies on next start)");
        return new TabPage("Polling") { Controls = { t }, UseVisualStyleBackColor = true };
    }

    private void LoadValues(AppSettings s)
    {
        this.cpuEnabled.Checked = s.Alerts.CpuEnabled;
        this.cpuThreshold.Value = Clamp(this.cpuThreshold, s.Alerts.CpuThresholdPercent);
        this.memoryEnabled.Checked = s.Alerts.MemoryEnabled;
        this.memoryThreshold.Value = Clamp(this.memoryThreshold, s.Alerts.MemoryThresholdPercent);
        this.diskEnabled.Checked = s.Alerts.DiskEnabled;
        this.diskThreshold.Value = Clamp(this.diskThreshold, s.Alerts.DiskFreeThresholdPercent);
        this.inAppRepeat.Value = Clamp(this.inAppRepeat, s.Alerts.InAppRepeatMinutes);
        this.emailEnabled.Checked = s.Alerts.EmailEnabled;
        this.recipients.Text = s.Alerts.EmailRecipients;
        this.emailRepeat.Value = Clamp(this.emailRepeat, s.Alerts.EmailRepeatMinutes);

        this.smtpHost.Text = s.Smtp.Host;
        this.smtpPort.Value = Clamp(this.smtpPort, s.Smtp.Port);
        this.smtpSecurity.SelectedItem = s.Smtp.Security;
        this.smtpUser.Text = s.Smtp.UserName;
        this.smtpPassword.Text = s.Smtp.Password;
        this.smtpFrom.Text = s.Smtp.FromAddress;

        this.summaryInterval.Value = Clamp(this.summaryInterval, s.Polling.SummaryIntervalSeconds);
        this.processesInterval.Value = Clamp(this.processesInterval, s.Polling.ProcessesIntervalSeconds);
        this.disksInterval.Value = Clamp(this.disksInterval, s.Polling.DisksIntervalSeconds);
        this.topCount.Value = Clamp(this.topCount, s.Polling.TopProcessCount);
        this.commandTimeout.Value = Clamp(this.commandTimeout, s.Polling.CommandTimeoutSeconds);
    }

    private static decimal Clamp(NumericUpDown box, int value) => Math.Clamp(value, box.Minimum, box.Maximum);

    private SmtpSettings ReadSmtp() => this.original.Smtp with
    {
        Host = this.smtpHost.Text.Trim(),
        Port = (int)this.smtpPort.Value,
        Security = this.smtpSecurity.SelectedItem is SmtpSecurity sec ? sec : SmtpSecurity.Auto,
        UserName = this.smtpUser.Text.Trim(),
        Password = this.smtpPassword.Text,
        FromAddress = this.smtpFrom.Text.Trim(),
    };

    private AppSettings ReadValues() => this.original with
    {
        Alerts = this.original.Alerts with
        {
            CpuEnabled = this.cpuEnabled.Checked,
            CpuThresholdPercent = (int)this.cpuThreshold.Value,
            MemoryEnabled = this.memoryEnabled.Checked,
            MemoryThresholdPercent = (int)this.memoryThreshold.Value,
            DiskEnabled = this.diskEnabled.Checked,
            DiskFreeThresholdPercent = (int)this.diskThreshold.Value,
            InAppRepeatMinutes = (int)this.inAppRepeat.Value,
            EmailEnabled = this.emailEnabled.Checked,
            EmailRecipients = this.recipients.Text.Trim(),
            EmailRepeatMinutes = (int)this.emailRepeat.Value,
        },
        Smtp = this.ReadSmtp(),
        Polling = this.original.Polling with
        {
            SummaryIntervalSeconds = (int)this.summaryInterval.Value,
            ProcessesIntervalSeconds = (int)this.processesInterval.Value,
            DisksIntervalSeconds = (int)this.disksInterval.Value,
            TopProcessCount = (int)this.topCount.Value,
            CommandTimeoutSeconds = (int)this.commandTimeout.Value,
        },
    };

    private void UpdateEnabledState()
    {
        this.cpuThreshold.Enabled = this.cpuEnabled.Checked;
        this.memoryThreshold.Enabled = this.memoryEnabled.Checked;
        this.diskThreshold.Enabled = this.diskEnabled.Checked;
        this.recipients.Enabled = this.emailEnabled.Checked;
        this.emailRepeat.Enabled = this.emailEnabled.Checked;
    }

    private void Accept()
    {
        var candidate = this.ReadValues();
        var errors = candidate.Validate();
        if (errors.Count > 0)
        {
            MessageBox.Show(this, string.Join(Environment.NewLine, errors), "Please correct the settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (errors.Any(e => e.Contains("SMTP", StringComparison.OrdinalIgnoreCase) || e.Contains("sender", StringComparison.OrdinalIgnoreCase)))
            {
                this.tabs.SelectedIndex = (int)SettingsPage.Email;
            }

            return;
        }

        this.Result = candidate;
        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private async Task SendTestEmailAsync()
    {
        var smtp = this.ReadSmtp();
        var errors = smtp.Validate();
        var to = this.recipients.Text.Split([';', ','], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (to.Length == 0)
        {
            errors = [.. errors, "Enter at least one recipient on the Alerts tab."];
        }

        if (errors.Count > 0)
        {
            MessageBox.Show(this, string.Join(Environment.NewLine, errors), "Cannot send test e-mail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        this.testEmailButton.Enabled = false;
        this.testEmailButton.Text = "Sending…";
        this.UseWaitCursor = true;
        try
        {
            // Runs on the network without blocking this dialog; v1 used a blocking SmtpClient.Send.
            await this.emailSender.SendAsync(smtp, to, "Monitoring Tool test e-mail",
                "If you can read this, alert e-mails are configured correctly.", this.lifetime.Token);
            MessageBox.Show(this, $"Test e-mail sent to {string.Join(", ", to)}.", "E-mail sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (OperationCanceledException) when (this.lifetime.IsCancellationRequested)
        {
            // Dialog closed while sending.
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Sending failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            if (!this.IsDisposed)
            {
                this.UseWaitCursor = false;
                this.testEmailButton.Text = "Send test e-mail";
                this.testEmailButton.Enabled = true;
            }
        }
    }
}
