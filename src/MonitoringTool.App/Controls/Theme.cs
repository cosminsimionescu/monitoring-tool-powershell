namespace MonitoringTool.App.Controls;

internal static class Theme
{
    public static readonly Color Ok = Color.FromArgb(31, 136, 61);
    public static readonly Color Warning = Color.FromArgb(191, 135, 0);
    public static readonly Color Critical = Color.FromArgb(207, 34, 46);
    public static readonly Color Muted = Color.FromArgb(101, 109, 118);
    public static readonly Color CardBackground = Color.FromArgb(246, 248, 250);
    public static readonly Color Border = Color.FromArgb(208, 215, 222);
    public static readonly Color Track = Color.FromArgb(225, 228, 232);

    public static Font HeaderFont(Font baseFont) => new(baseFont.FontFamily, baseFont.Size + 2f, FontStyle.Bold);

    public static Font BigValueFont(Font baseFont) => new(baseFont.FontFamily, baseFont.Size + 13f, FontStyle.Regular);

    /// <summary>Red at/over the threshold, amber from 75 % of it, green below.</summary>
    public static Color ForUsage(double value, double threshold) =>
        value >= threshold ? Critical : value >= threshold * 0.75 ? Warning : Ok;

    /// <summary>For "free space" style values where low is bad.</summary>
    public static Color ForFree(double freePercent, double threshold) =>
        freePercent < threshold ? Critical : freePercent < threshold * 2 ? Warning : Ok;
}
