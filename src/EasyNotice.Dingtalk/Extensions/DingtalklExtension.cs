using EasyNotice.Dingtalk;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DingtalklExtension
    {
        public static EasyNoticeOptions UseDingTalk(
            this EasyNoticeOptions options,
            Action<DingtalkOptions> configure
            )
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            options.RegisterExtension(new DingtalkOptionsExtension(configure));
            return options;
        }
    }
}
