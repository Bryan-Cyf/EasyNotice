using Microsoft.Extensions.DependencyInjection;
using System;

namespace EasyNotice.Weixin
{
    public class WeixinOptionsExtension : IEasyNoticeOptionsExtension
    {
        private readonly Action<WeixinOptions> configure;

        public WeixinOptionsExtension(Action<WeixinOptions> configure)
        {
            this.configure = configure;
        }

        public void AddServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddOptions<WeixinOptions>().Configure(configure);
            services.AddTransient<IWeixinProvider, WeixinProvider>();
        }
    }
}
