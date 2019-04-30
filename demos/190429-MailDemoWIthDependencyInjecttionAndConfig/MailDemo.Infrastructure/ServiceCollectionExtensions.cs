using MailDemo.Application;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailDemo.Infrastructure
{
    public class Options
    {
        public Options()
        {
            EmailConfig = new EmailConfig();
        }

        public EmailConfig EmailConfig { get; private set; } 
    }

    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services, Action<Options> setupAction)
        {
            var options = new Options();
            setupAction(options);

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddSingleton(options.EmailConfig);
        }
    }


}
