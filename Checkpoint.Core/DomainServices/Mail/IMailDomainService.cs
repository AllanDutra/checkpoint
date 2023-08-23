namespace Checkpoint.Core.DomainServices.Mail
{
    public interface IMailDomainService
    {
        void SendEmail(string recipientEmail, string messageBody, string subject);
    }
}
