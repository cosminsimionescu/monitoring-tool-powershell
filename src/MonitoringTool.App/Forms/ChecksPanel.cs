using System.ComponentModel;
using MonitoringTool.App.Controls;
using MonitoringTool.Core.Checks;
using MonitoringTool.Core.Settings;

namespace MonitoringTool.App.Forms;

/// <summary>
/// The "Checks" tab: one row per custom check with its latest result. It only displays state and
/// raises events; MainForm wires those to the orchestrator and to the settings.
/// </summary>
internal sealed class ChecksPanel : Panel
{
    private readonly BindingList<CheckRow> rows = [];
    private readonly Dictionary<string, CheckRow> rowsById = new(StringComparer.OrdinalIgnoreCase);
    private readonly GridView grid = new();
    private readonly Button runNowButton = new() { Text = "Run now", AutoSize = true };
    private readonly Button reloadButton = new() { Text = "Reload checks", AutoSize = true };
    private readonly Button openFolderButton = new() { Text = "Open checks folder", AutoSize = true };
    private readonly CheckBox alertsCheck = new() { Text = "Alert on Warning/Critical", AutoSize = true };
    private bool running;
    private bool suppressEvents;

    public ChecksPanel()
    {
        this.Dock = DockStyle.Fill;

        this.grid.ReadOnly = false;
        this.grid.Columns.Add(new DataGridViewCheckBoxColumn
        {
            HeaderText = "On",
            DataPropertyName = nameof(CheckRow.Enabled),
            Name = nameof(CheckRow.Enabled),
            FillWeight = 6,
            SortMode = DataGridViewColumnSortMode.NotSortable,
        });
        this.grid.AddTextColumn("Status", nameof(CheckRow.StatusText), 12)
                 .AddTextColumn("Check", nameof(CheckRow.Name), 26)
                 .AddTextColumn("Result", nameof(CheckRow.Message), 62)
                 .AddTextColumn("Last run", nameof(CheckRow.LastRun), 12, "T")
                 .AddTextColumn("Took", nameof(CheckRow.DurationText), 8, alignRight: true)
                 .AddTextColumn("Every", nameof(CheckRow.IntervalText), 8, alignRight: true);
        foreach (DataGridViewColumn column in this.grid.Columns)
        {
            column.ReadOnly = column.Name != nameof(CheckRow.Enabled);
        }

        this.grid.DataSource = this.rows;
        this.grid.CellFormatting += this.OnCellFormatting;
        this.grid.CurrentCellDirtyStateChanged += (_, _) =>
        {
            // Commit checkbox clicks immediately instead of when the cell loses focus.
            if (this.grid.IsCurrentCellDirty)
            {
                this.grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        };
        this.grid.CellValueChanged += this.OnCellValueChanged;
        this.grid.SelectionChanged += (_, _) => this.UpdateButtons();
        this.grid.CellDoubleClick += (_, e) =>
        {
            if (e.RowIndex >= 0 && this.SelectedRow is { } row)
            {
                this.ShowDetails(row);
            }
        };

        this.runNowButton.Click += (_, _) =>
        {
            if (this.SelectedRow is { } row)
            {
                this.RunNowRequested?.Invoke(this, row.Id);
            }
        };
        this.reloadButton.Click += (_, _) => this.ReloadRequested?.Invoke(this, EventArgs.Empty);
        this.openFolderButton.Click += (_, _) => this.OpenFolderRequested?.Invoke(this, EventArgs.Empty);
        this.alertsCheck.CheckedChanged += (_, _) =>
        {
            if (!this.suppressEvents)
            {
                this.AlertsToggled?.Invoke(this, this.alertsCheck.Checked);
            }
        };

        var hint = new Label
        {
            Text = "Double-click a row for details. Add your own checks as .ps1 files in the checks folder.",
            AutoSize = true,
            ForeColor = Theme.Muted,
        };

        this.Controls.Add(new Section("Custom checks", this.grid, this.runNowButton, this.reloadButton, this.openFolderButton, this.alertsCheck, hint));
        this.UpdateButtons();
    }

    public event EventHandler<string>? RunNowRequested;

    public event EventHandler? ReloadRequested;

    public event EventHandler? OpenFolderRequested;

    public event EventHandler<(string CheckId, bool Enabled)>? CheckEnabledChanged;

    public event EventHandler<bool>? AlertsToggled;

    /// <summary>Raised whenever the number of failing checks changes (for the tab caption).</summary>
    public event EventHandler<int>? FailingCountChanged;

    private CheckRow? SelectedRow => this.grid.CurrentRow?.DataBoundItem as CheckRow;

    public void SetDefinitions(IReadOnlyList<CheckDefinition> definitions, ChecksSettings settings)
    {
        ArgumentNullException.ThrowIfNull(definitions);
        ArgumentNullException.ThrowIfNull(settings);

        // Keep the last result of checks that survive a reload.
        var previous = new Dictionary<string, CheckRow>(this.rowsById, StringComparer.OrdinalIgnoreCase);
        this.suppressEvents = true;
        try
        {
            this.rows.RaiseListChangedEvents = false;
            this.rows.Clear();
            this.rowsById.Clear();
            foreach (var definition in definitions)
            {
                var row = previous.TryGetValue(definition.Id, out var old) ? old : new CheckRow();
                row.Definition = definition;
                row.Enabled = settings.IsEnabled(definition.Id);
                this.rows.Add(row);
                this.rowsById[definition.Id] = row;
            }

            this.alertsCheck.Checked = settings.AlertsEnabled;
        }
        finally
        {
            this.rows.RaiseListChangedEvents = true;
            this.rows.ResetBindings();
            this.suppressEvents = false;
        }

        this.RaiseFailingCount();
        this.UpdateButtons();
    }

    public void SetRunning(bool isRunning)
    {
        this.running = isRunning;
        if (!isRunning)
        {
            foreach (var row in this.rows.Where(r => r.IsRunning))
            {
                row.IsRunning = false;
            }

            this.rows.ResetBindings();
        }

        this.UpdateButtons();
    }

    public void OnCheckStarted(string checkId) => this.Update(checkId, row => row.IsRunning = true);

    public void OnCheckCompleted(CheckResult result)
    {
        ArgumentNullException.ThrowIfNull(result);
        this.Update(result.CheckId, row =>
        {
            row.IsRunning = false;
            row.Result = result;
        });
        this.RaiseFailingCount();
    }

    private void Update(string checkId, Action<CheckRow> change)
    {
        if (!this.rowsById.TryGetValue(checkId, out var row))
        {
            return;
        }

        change(row);
        this.rows.ResetItem(this.rows.IndexOf(row));
    }

    private void RaiseFailingCount() =>
        this.FailingCountChanged?.Invoke(this, this.rows.Count(r => r.Enabled && r.Result?.Status is CheckStatus.Warning or CheckStatus.Critical));

    private void UpdateButtons()
    {
        this.runNowButton.Enabled = this.running && this.SelectedRow is { IsRunning: false };
        this.reloadButton.Enabled = true;
    }

    private void OnCellValueChanged(object? sender, DataGridViewCellEventArgs e)
    {
        if (this.suppressEvents || e.RowIndex < 0 || this.grid.Columns[e.ColumnIndex].Name != nameof(CheckRow.Enabled))
        {
            return;
        }

        if (this.grid.Rows[e.RowIndex].DataBoundItem is CheckRow row)
        {
            this.CheckEnabledChanged?.Invoke(this, (row.Id, row.Enabled));
            this.rows.ResetItem(e.RowIndex);
            this.RaiseFailingCount();
        }
    }

    private void OnCellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.CellStyle is null || e.RowIndex < 0 || this.grid.Rows[e.RowIndex].DataBoundItem is not CheckRow row)
        {
            return;
        }

        if (!row.Enabled)
        {
            e.CellStyle.ForeColor = Theme.Muted;
            return;
        }

        if (this.grid.Columns[e.ColumnIndex].Name == nameof(CheckRow.StatusText))
        {
            e.CellStyle.ForeColor = row.IsRunning ? Theme.Muted : row.Result?.Status switch
            {
                CheckStatus.Ok => Theme.Ok,
                CheckStatus.Warning => Theme.Warning,
                CheckStatus.Critical => Theme.Critical,
                _ => Theme.Muted,
            };
        }
    }

    private void ShowDetails(CheckRow row)
    {
        var d = row.Definition;
        var text = string.Join(Environment.NewLine,
            d.Name,
            "",
            d.Description.Length > 0 ? d.Description : "(no description)",
            "",
            $"Status:   {row.StatusText}",
            $"Result:   {row.Message}",
            $"Schedule: every {row.IntervalText}, timeout {d.TimeoutSeconds} s",
            $"Script:   {d.Source}");
        MessageBox.Show(this, text, "Check details", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>Grid row. Mutable on purpose: rows are updated in place as results arrive.</summary>
    internal sealed class CheckRow
    {
        public CheckDefinition Definition { get; set; } = null!;

        public CheckResult? Result { get; set; }

        public bool IsRunning { get; set; }

        public bool Enabled { get; set; }

        public string Id => this.Definition.Id;

        public string Name => this.Definition.Name;

        public string StatusText => !this.Enabled ? "Off"
            : this.IsRunning ? "Running…"
            : this.Result?.Status.ToString() ?? "Not run";

        public string Message => this.Result?.Message ?? this.Definition.Description;

        public DateTime? LastRun => this.Result?.Timestamp.LocalDateTime;

        public string DurationText => this.Result is { } r ? $"{r.Duration.TotalSeconds:0.0} s" : "";

        public string IntervalText => FormatInterval(this.Definition.IntervalSeconds);

        private static string FormatInterval(int seconds) => seconds switch
        {
            >= 3600 when seconds % 3600 == 0 => $"{seconds / 3600} h",
            >= 60 when seconds % 60 == 0 => $"{seconds / 60} min",
            _ => $"{seconds} s",
        };
    }
}
