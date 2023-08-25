using System.Net;
using System.Net.Mail;
using Checkpoint.Core.Interfaces.Notifications;
using Checkpoint.Core.Models.ViewModels;
using Microsoft.Extensions.Configuration;

namespace Checkpoint.Core.DomainServices.Mail
{
    public class MailDomainService : IMailDomainService
    {
        private readonly IConfiguration _configuration;
        private readonly INotifier _notifier;

        public MailDomainService(IConfiguration configuration, INotifier notifier)
        {
            _configuration = configuration;
            _notifier = notifier;
        }

        public void SendEmail(string recipientEmail, string messageBody, string subject)
        {
            try
            {
                var mailMessage = BuildMailMessage(recipientEmail, messageBody, subject);

                var smtpClient = BuildSmtpClient();

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                _notifier.Handle(
                    new NotificationModel(
                        $"A problem occurred after trying to send an email: {ex.Message}",
                        HttpStatusCode.FailedDependency
                    )
                );

                return;
            }
        }

        private SmtpClient BuildSmtpClient()
        {
            try
            {
                var userName = _configuration["SMTPNetworkCredential:UserName"];
                var password = _configuration["SMTPNetworkCredential:Password"];

                var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Timeout = 10000, // ? 10 seconds
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(userName, password)
                };

                return smtpClient;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private MailMessage BuildMailMessage(
            string recipientEmail,
            string messageBody,
            string subject
        )
        {
            try
            {
                var userName = _configuration["SMTPNetworkCredential:UserName"];

                var mailMessage = new MailMessage()
                {
                    From = new MailAddress(userName, "Checkpoint Project"),
                    Body = messageBody,
                    Subject = subject,
                    IsBodyHtml = true,
                    Priority = MailPriority.Normal,
                };

                mailMessage.To.Add(recipientEmail);

                return mailMessage;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
