namespace MonitoringTool.App.Forms;

internal sealed record AlertLogEntry(DateTime Time, string Severity, string Message, string Value);
