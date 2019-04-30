using System;
using System.Collections.Generic;
using System.Text;

namespace MailDemo.Infrastructure
{
    public class EmailConfig
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public bool UseEncryption { get; set; }
    }
}
