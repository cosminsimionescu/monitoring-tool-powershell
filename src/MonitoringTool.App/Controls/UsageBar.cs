using System.ComponentModel;

namespace MonitoringTool.App.Controls;

/// <summary>A flat, colour-coded percentage bar (the stock ProgressBar cannot change colour under visual styles).</summary>
internal sealed class UsageBar : Control
{
    private double value;
    private Color barColor = Theme.Ok;

    public UsageBar()
    {
        this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        this.Height = 10;
        this.TabStop = false;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double Value
    {
        get => this.value;
        set
        {
            this.value = Math.Clamp(value, 0, 100);
            this.Invalidate();
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color BarColor
    {
        get => this.barColor;
        set
        {
            this.barColor = value;
            this.Invalidate();
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var bounds = this.ClientRectangle;
        using (var track = new SolidBrush(Theme.Track))
        {
            e.Graphics.FillRectangle(track, bounds);
        }

        var width = (int)Math.Round(bounds.Width * this.value / 100.0);
        if (width > 0)
        {
            using var fill = new SolidBrush(this.barColor);
            e.Graphics.FillRectangle(fill, bounds with { Width = width });
        }
    }
}
