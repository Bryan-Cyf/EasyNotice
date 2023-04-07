using Microsoft.Extensions.DependencyInjection;
using System;

namespace EasyNotice.Email
{
    internal class EmailOptionsExtension : IEasyNoticeOptionsExtension
    {
        private readonly Action<EmailOptions> configure;

        public EmailOptionsExtension(Action<EmailOptions> configure)
        {
            this.configure = configure;
        }

        public void AddServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddOptions<EmailOptions>().Configure(configure);
            services.AddTransient<IEmailProvider, EmailProvider>();
        }
    }
}
