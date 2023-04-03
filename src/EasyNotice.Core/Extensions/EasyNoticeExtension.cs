using EasyNotice.Core;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EasyNoticeExtension
    {
        public static IServiceCollection AddEsayNotice(this IServiceCollection services,  Action<NoticeOptions> configure)
        {
            var options = new NoticeOptions();
            configure?.Invoke(options);

            services.AddOptions();
            services.AddOptions<NoticeOptions>().Configure(configure);

            foreach (var serviceExtension in options.Extensions)
            {
                serviceExtension.AddServices(services);
            }
            return services;
        }
    }
}

