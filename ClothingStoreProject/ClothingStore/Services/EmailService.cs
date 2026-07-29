using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

public interface IEmailService
{
    Task SendAsync(
        string toEmail,
        string subject,
        string body);
}

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    private readonly ILogger<EmailService> _logger;

    public EmailService(
        IConfiguration config,
        ILogger<EmailService> logger)
    {
        _config = config;
        _logger = logger;
    }

    public async Task SendAsync(
        string toEmail,
        string subject,
        string body)
    {
        try
        {
            string? from = _config["Email:From"];
            string? host = _config["Email:Host"];
            string? username = _config["Email:Username"];
            string? password = _config["Email:Password"];

            if (string.IsNullOrWhiteSpace(from))
            {
                throw new InvalidOperationException(
                    "Email:From is missing.");
            }

            if (string.IsNullOrWhiteSpace(host))
            {
                throw new InvalidOperationException(
                    "Email:Host is missing.");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new InvalidOperationException(
                    "Email:Username is missing.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException(
                    "Email:Password is missing.");
            }

            if (!int.TryParse(
                    _config["Email:Port"],
                    out int port))
            {
                throw new InvalidOperationException(
                    "Email:Port is missing or invalid.");
            }

            var message = new MimeMessage();

            message.From.Add(
                MailboxAddress.Parse(from));

            message.To.Add(
                MailboxAddress.Parse(toEmail));

            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = body
            };

            using var smtp = new SmtpClient();

            // Port 587 uses STARTTLS
            await smtp.ConnectAsync(
                host,
                port,
                SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(
                username,
                password);

            await smtp.SendAsync(message);

            await smtp.DisconnectAsync(true);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "Failed to send email to {EmailAddress}.",
                toEmail);

            throw;
        }
    }
}