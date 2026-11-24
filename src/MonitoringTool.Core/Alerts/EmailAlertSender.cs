using System.Globalization;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MonitoringTool.Core.Settings;

namespace MonitoringTool.Core.Alerts;

public interface IEmailSender
{
    Task SendAsync(SmtpSettings smtp, IReadOnlyList<string> recipients, string subject, string body, CancellationToken cancellationToken);
}

/// <summary>Fully asynchronous SMTP client (MailKit), replacing the blocking System.Net.Mail.SmtpClient.</summary>
public sealed class MailKitEmailSender : IEmailSender
{
    public async Task SendAsync(SmtpSettings smtp, IReadOnlyList<string> recipients, string subject, string body, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(smtp);
        ArgumentNullException.ThrowIfNull(recipients);

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Monitoring Tool", smtp.EffectiveFromAddress));
        foreach (var recipient in recipients)
        {
            message.To.Add(MailboxAddress.Parse(recipient));
        }

        message.Subject = subject;
        message.Body = new TextPart("plain") { Text = body };

        var security = smtp.Security switch
        {
            SmtpSecurity.StartTls => SecureSocketOptions.StartTls,
            SmtpSecurity.SslOnConnect => SecureSocketOptions.SslOnConnect,
            SmtpSecurity.None => SecureSocketOptions.None,
            _ => SecureSocketOptions.Auto,
        };

        using var client = new SmtpClient { Timeout = (int)TimeSpan.FromSeconds(30).TotalMilliseconds };
        await client.ConnectAsync(smtp.Host, smtp.Port, security, cancellationToken).ConfigureAwait(false);
        if (!string.IsNullOrEmpty(smtp.UserName))
        {
            await client.AuthenticateAsync(smtp.UserName, smtp.Password, cancellationToken).ConfigureAwait(false);
        }

        await client.SendAsync(message, cancellationToken).ConfigureAwait(false);
        await client.DisconnectAsync(quit: true, cancellationToken).ConfigureAwait(false);
    }

    public static (string Subject, string Body) Format(AlertNotification alert)
    {
        ArgumentNullException.ThrowIfNull(alert);
        var tag = alert.Kind == AlertKind.Resolved ? "[RESOLVED]" : "[ALERT]";
        var subject = $"{tag} {alert.Server}: {alert.Message}";
        var body = string.Join(Environment.NewLine,
            $"Server:    {alert.Server}",
            $"Status:    {alert.Kind}",
            $"Message:   {alert.Message}",
            $"Value:     {alert.Value.ToString("0.##", CultureInfo.InvariantCulture)}%",
            $"Threshold: {alert.Threshold.ToString("0.##", CultureInfo.InvariantCulture)}%",
            $"Time:      {alert.Timestamp.ToLocalTime():yyyy-MM-dd HH:mm:ss zzz}",
            "",
            "Sent by Monitoring Tool.");
        return (subject, body);
    }
}
