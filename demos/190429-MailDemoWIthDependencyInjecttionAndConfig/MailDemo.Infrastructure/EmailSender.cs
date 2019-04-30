using MailDemo.Application;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailDemo.Infrastructure
{


    internal class EmailSender : IEmailSender
    {
        private readonly EmailConfig config;

        public EmailSender(EmailConfig config)
        {
            this.config = config;
        }

        public void SendEmail(string to, string from, string subject, string htmlMessage)
        {
            // TODO: Build Message
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(to));
            message.From.Add(new MailboxAddress(from));
            message.Subject = subject;

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = htmlMessage
            };


            using (var emailClient = new SmtpClient())
            {

                if (config.UseEncryption)
                {
                    emailClient.Connect(config.SmtpServer, config.SmtpPort, SecureSocketOptions.StartTls);
                }
                else
                {
                    emailClient.Connect(config.SmtpServer, config.SmtpPort, false);
                }

                //Remove any OAuth functionality as we won't be using it. 
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                if (!string.IsNullOrEmpty(config.SmtpPassword))
                {
                    emailClient.Authenticate(config.SmtpUsername, config.SmtpPassword);
                }

                emailClient.Send(message);
                emailClient.Disconnect(true);
            }
        }
    }
}
