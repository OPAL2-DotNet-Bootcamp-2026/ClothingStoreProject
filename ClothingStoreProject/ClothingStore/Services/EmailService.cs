using MailKit.Net.Smtp;
using MimeKit;

public interface IEmailService
{
    Task SendAsync(string toEmail, string subject, string body);
}