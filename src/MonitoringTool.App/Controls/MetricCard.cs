namespace MonitoringTool.App.Controls;

/// <summary>Title, large value, colour bar and a detail line, e.g. "MEMORY / 63.2 % / 20.1 of 32 GB used".</summary>
internal sealed class MetricCard : TableLayoutPanel
{
    private readonly Label valueLabel;
    private readonly Label detailLabel;
    private readonly UsageBar bar = new() { Dock = DockStyle.Fill, Margin = new Padding(0, 4, 0, 4) };

    public MetricCard(string title)
    {
        this.Dock = DockStyle.Fill;
        this.BackColor = Theme.CardBackground;
        this.Padding = new Padding(14, 10, 14, 10);
        this.Margin = new Padding(6);
        this.ColumnCount = 1;
        this.RowCount = 4;
        this.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        this.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        this.RowStyles.Add(new RowStyle(SizeType.Absolute, 18));
        this.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        this.AutoSize = true;
        this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

        var titleLabel = new Label { Text = title.ToUpperInvariant(), AutoSize = true, ForeColor = Theme.Muted, Font = new Font(this.Font, FontStyle.Bold) };
        this.valueLabel = new Label { Text = "—", AutoSize = true, Font = Theme.BigValueFont(this.Font), Margin = new Padding(0, 2, 0, 0) };
        this.detailLabel = new Label { Text = " ", AutoSize = true, ForeColor = Theme.Muted };

        this.Controls.Add(titleLabel, 0, 0);
        this.Controls.Add(this.valueLabel, 0, 1);
        this.Controls.Add(this.bar, 0, 2);
        this.Controls.Add(this.detailLabel, 0, 3);
    }

    public void SetValue(double percent, double threshold, string detail)
    {
        var color = Theme.ForUsage(percent, threshold);
        this.valueLabel.Text = $"{percent:0.0} %";
        this.valueLabel.ForeColor = color == Theme.Ok ? this.ForeColor : color;
        this.bar.Value = percent;
        this.bar.BarColor = color;
        this.detailLabel.Text = detail;
    }

    public void SetText(string value, string detail)
    {
        this.valueLabel.Text = value;
        this.valueLabel.ForeColor = this.ForeColor;
        this.bar.Value = 0;
        this.detailLabel.Text = detail;
    }

    public void Clear() => this.SetText("—", " ");
}
