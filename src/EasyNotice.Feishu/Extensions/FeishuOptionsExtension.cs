using Microsoft.Extensions.DependencyInjection;
using System;

namespace EasyNotice.Feishu
{
    public class FeishuOptionsExtension : IEasyNoticeOptionsExtension
    {
        private readonly Action<FeishuOptions> configure;

        public FeishuOptionsExtension(Action<FeishuOptions> configure)
        {
            this.configure = configure;
        }

        public void AddServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddOptions<FeishuOptions>().Configure(configure);
            services.AddSingleton<IFeishuProvider, FeishuProvider>();
        }
    }
}
