using Dashboard.DAL.Models.Identity;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
namespace Dashboard.PL.Helper
{
    public class EmailSettings(IOptions<EmailSettingOptions> _options) : IEmailSettings
    {
        public void SendEmail(Email email)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(_options.Value.DisplayName, _options.Value.Email));
            message.To.Add(MailboxAddress.Parse(email.To));
            message.Subject = email.Subject;

            message.Body = new BodyBuilder
            {
                HtmlBody = email.Body
            }.ToMessageBody();

            using var smtp = new SmtpClient();

            smtp.Connect(
                _options.Value.Host,
                _options.Value.Port,
                SecureSocketOptions.StartTls
            );

            smtp.Authenticate(_options.Value.Email, _options.Value.Password);

            smtp.Send(message);

            smtp.Disconnect(true);
        }
    }
}

