using CleanArchitecture.Application.Abstractions.Email;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace CleanArchitecture.Infrastructure.Email;

internal sealed class EmailService : IEmailService
{
    public EmailService(IOptions<GmailSettings> gmailSettings)
    {
        _gmailSettings = gmailSettings.Value;
    }

    public GmailSettings _gmailSettings { get; }

    public void Send(string recipient, string subject, string body)
    {
        try
        {
            var fromEmail = _gmailSettings.Username;
            var password = _gmailSettings.Password;
            var message = new MailMessage();

            message.From = new MailAddress(fromEmail!);
            message.Subject = subject;
            message.To.Add(new MailAddress(recipient));
            message.Body = body;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = _gmailSettings.Port,
                Credentials = new NetworkCredential(fromEmail, password),
                EnableSsl = true
            };

            smtpClient.Send(message);
        }
        catch (Exception ex)
        {
            throw new Exception("No se pudo enviar el email", ex);
        }
    }   
}
