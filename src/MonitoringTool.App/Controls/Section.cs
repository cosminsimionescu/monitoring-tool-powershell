namespace MonitoringTool.App.Controls;

/// <summary>A titled block: header line (with optional extra controls on the right) above a content control.</summary>
internal sealed class Section : Panel
{
    public Section(string title, Control content, params Control[] headerExtras)
    {
        this.Dock = DockStyle.Fill;
        this.Padding = new Padding(6);

        var header = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            WrapContents = true,
            Padding = new Padding(0, 0, 0, 4),
        };
        header.Controls.Add(new Label
        {
            Text = title,
            AutoSize = true,
            Font = Theme.HeaderFont(this.Font),
            Margin = new Padding(0, 0, 16, 0),
        });
        foreach (var extra in headerExtras)
        {
            extra.Margin = new Padding(0, 4, 10, 0);
            header.Controls.Add(extra);
        }

        var border = new Panel { Dock = DockStyle.Fill, Padding = new Padding(1), BackColor = Theme.Border };
        content.Dock = DockStyle.Fill;
        border.Controls.Add(content);

        this.Controls.Add(border);
        this.Controls.Add(header);
    }
}
