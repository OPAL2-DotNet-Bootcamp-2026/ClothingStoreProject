using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.RateLimiting;

public interface IEmailService
{
    Task SendAsync(string toEmail, string subject, string body);
}

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    private readonly RateLimiter _rateLimiter;

    public EmailService(IConfiguration config, RateLimiter rateLimiter)
    {
        _config = config;
        _rateLimiter = rateLimiter;
    }

    public async Task SendAsync(string toEmail, string subject, string body)
    {
        using var lease = await _rateLimiter.AcquireAsync(1);
        if (!lease.IsAcquired)
            return;

        try
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_config["Email:From"]));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body };

            if (!int.TryParse(_config["Email:Port"], out int port))
                return;

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["Email:Host"], port, true);
            await smtp.AuthenticateAsync(_config["Email:Username"], _config["Email:Password"]);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
        catch (Exception)
        {
        }
    }
}