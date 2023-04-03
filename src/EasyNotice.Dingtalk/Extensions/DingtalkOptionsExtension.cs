using Microsoft.Extensions.DependencyInjection;
using System;

namespace EasyNotice.Dingtalk
{
    public class DingtalkOptionsExtension : IEasyNoticeOptionsExtension
    {
        private readonly Action<DingtalkOptions> configure;

        public DingtalkOptionsExtension(Action<DingtalkOptions> configure)
        {
            this.configure = configure;
        }

        public void AddServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddOptions<DingtalkOptions>().Configure(configure);
            services.AddSingleton<IDingtalkProvider, DingtalkProvider>();
        }
    }
}
