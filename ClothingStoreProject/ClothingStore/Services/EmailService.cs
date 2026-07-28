using MailKit.Net.Smtp;
using MimeKit;

public interface IEmailService
{
    Task SendAsync(string toEmail, string subject, string body);
}

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    public EmailService(IConfiguration config)
    {
        _config = config;
    }
    public async Task SendAsync(string toEmail, string subject, string body)
    {
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
            // Email failure must not affect the calling operation (checkout, status update, etc.)
        }
    }

}