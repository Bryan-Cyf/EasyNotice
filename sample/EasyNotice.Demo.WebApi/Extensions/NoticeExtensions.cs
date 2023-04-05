using EasyNotice;
using EasyNotice.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class NoticeExtensions
    {
        public static IServiceCollection AddNotice(this IServiceCollection services, IConfiguration configuration, Action<NoticeOptions> configure = null, string sectionName = null)
        {
            sectionName ??= NoticeOptions.SectionName;

            var baseConfiguration = configuration.GetSection(sectionName);
            var noticeOptions = baseConfiguration.Get<NoticeOptions>();
            configure?.Invoke(noticeOptions);

            services.AddEsayNotice(config =>
            {
                //同一消息发送间隔 默认10秒
                config.IntervalSeconds = noticeOptions.IntervalSeconds;
                var mailOptions = baseConfiguration.GetSection(EmailOptions.SectionName).Get<EmailOptions>();
                if (mailOptions != null)
                {
                    config.UseEmail(x =>
                    {
                        x.Password = mailOptions.Password;
                        x.Host = mailOptions.Host;
                        x.FromAddress = mailOptions.FromAddress;
                        x.FromName = mailOptions.FromName;
                        x.Port = mailOptions.Port;
                        x.ToAddress = mailOptions.ToAddress;
                    });
                }

                var dingtalkOptions = baseConfiguration.GetSection(DingtalkOptions.SectionName).Get<DingtalkOptions>();
                if (dingtalkOptions != null)
                {
                    config.UseDingTalk(x =>
                    {
                        x.Secret = dingtalkOptions.Secret;
                        x.WebHook = dingtalkOptions.WebHook;
                    });
                }

                var feishuOptions = baseConfiguration.GetSection(FeishuOptions.SectionName).Get<DingtalkOptions>();
                if (feishuOptions != null)
                {
                    config.UseFeishu(x =>
                    {
                        x.Secret = feishuOptions.Secret;
                        x.WebHook = feishuOptions.WebHook;
                    });
                }

                var weixinOptions = baseConfiguration.GetSection(WeixinOptions.SectionName).Get<WeixinOptions>();
                if (weixinOptions != null)
                {
                    config.UseWeixin(x =>
                    {
                        x.WebHook = weixinOptions.WebHook;
                    });
                }
            });

            return services;
        }
    }
}
