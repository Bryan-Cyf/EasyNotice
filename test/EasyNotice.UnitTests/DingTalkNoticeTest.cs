using EasyNotice.Email;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EasyNotice.UnitTests
{
    public class DingTalkNoticeTest
    {
        private readonly IDingtalkProvider _dingtalkProvider;

        public DingTalkNoticeTest()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddEsayNotice(config =>
            {
                config.IntervalSeconds = 10;//同一标题的消息，10秒内只能发一条，避免短时间内大量发送重复消息
                config.UseDingTalk(option =>
                {
                    option.WebHook = "https://oapi.dingtalk.com/robot/send?access_token=xxx";
                    option.Secret = "secret";
                });
            });
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            _dingtalkProvider = serviceProvider.GetService<IDingtalkProvider>();
        }

        [Fact]
        public async Task DingTalk_Send_Should_Be_Succeed()
        {
            var response = await _dingtalkProvider.SendAsync("通知标题", new Exception("custom exception"));
            Assert.True(response.IsSuccess);
        }
    }
}
