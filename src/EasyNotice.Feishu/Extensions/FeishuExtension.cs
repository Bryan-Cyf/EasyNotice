using EasyNotice;
using EasyNotice.Feishu;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FeishuExtension
    {
        public static EasyNoticeOptions UseFeishu(
            this EasyNoticeOptions options,
            Action<FeishuOptions> configure
            )
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            options.RegisterExtension(new FeishuOptionsExtension(configure));
            return options;
        }
    }
}
