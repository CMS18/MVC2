namespace MailDemo.Application
{
    public interface IEmailSender
    {
        void SendEmail(string to, string from, string subject, string htmlMessage);
    }
}