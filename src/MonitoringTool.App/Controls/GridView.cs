namespace MonitoringTool.App.Controls;

/// <summary>Read-only, double-buffered DataGridView with sensible defaults.</summary>
/// <remarks>
/// v1 subclassed DataGridView only to swallow exceptions thrown from OnPaint – a symptom of rows
/// being modified from background threads. All updates now happen on the UI thread, so no hack is needed.
/// </remarks>
internal sealed class GridView : DataGridView
{
    public GridView()
    {
        this.DoubleBuffered = true;
        this.Dock = DockStyle.Fill;
        this.ReadOnly = true;
        this.AllowUserToAddRows = false;
        this.AllowUserToDeleteRows = false;
        this.AllowUserToResizeRows = false;
        this.AutoGenerateColumns = false;
        this.RowHeadersVisible = false;
        this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        this.MultiSelect = false;
        this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        this.BackgroundColor = SystemColors.Window;
        this.BorderStyle = BorderStyle.None;
        this.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        this.GridColor = Theme.Border;
        this.EnableHeadersVisualStyles = false;
        this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        this.ColumnHeadersDefaultCellStyle.BackColor = Theme.CardBackground;
        this.ColumnHeadersDefaultCellStyle.ForeColor = Theme.Muted;
        this.ColumnHeadersDefaultCellStyle.SelectionBackColor = Theme.CardBackground;
        this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.DefaultCellStyle.SelectionBackColor = Color.FromArgb(221, 244, 255);
        this.DefaultCellStyle.SelectionForeColor = SystemColors.ControlText;
        this.StandardTab = true;
    }

    public GridView AddTextColumn(string header, string property, float weight = 100, string? format = null, bool alignRight = false)
    {
        var column = new DataGridViewTextBoxColumn
        {
            HeaderText = header,
            DataPropertyName = property,
            Name = property,
            FillWeight = weight,
            SortMode = DataGridViewColumnSortMode.NotSortable,
        };
        if (format is not null)
        {
            column.DefaultCellStyle.Format = format;
        }

        if (alignRight)
        {
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        this.Columns.Add(column);
        return this;
    }
}
