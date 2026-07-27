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

}