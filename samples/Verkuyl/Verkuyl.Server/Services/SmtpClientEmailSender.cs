using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Verkuyl.Server.Services
{
    // This class is used by the application to send Email
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class SmtpClientEmailSender : IEmailSender
    {
        private readonly IOptions<SmtpClientEmailSenderOptions> smtpClientEmailSenderOptions;

        public SmtpClientEmailSender(IOptions<SmtpClientEmailSenderOptions> smtpClientEmailSenderOptions)
        {
            this.smtpClientEmailSenderOptions = smtpClientEmailSenderOptions;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mailMessage = new MailMessage(smtpClientEmailSenderOptions.Value.From, email, subject, message)
            {
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8,
            };

            using var smtp = new SmtpClient();
            ConfigureSmtpClient(smtp);

            await smtp.SendMailAsync(mailMessage);
        }

        private void ConfigureSmtpClient(SmtpClient smtpClient)
        {
            var options = smtpClientEmailSenderOptions.Value;

            smtpClient.Host = options.Host;
            smtpClient.Port = options.Port;
            smtpClient.EnableSsl = options.EnableSsl;

            if (options.UseDefaultCredentials)
            {
                smtpClient.UseDefaultCredentials = true;
            }
            else if (options.NetworkCredential != null)
            {
                smtpClient.Credentials = new System.Net.NetworkCredential(options.NetworkCredential.Username, options.NetworkCredential.Password, options.NetworkCredential.Domain);
            }
        }
    }
}
