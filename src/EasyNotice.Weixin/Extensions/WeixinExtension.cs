using EasyNotice;
using EasyNotice.Weixin;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WeixinExtension
    {
        public static EasyNoticeOptions UseWeixin(
            this EasyNoticeOptions options,
            Action<WeixinOptions> configure
            )
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            options.RegisterExtension(new WeixinOptionsExtension(configure));
            return options;
        }
    }
}
