using EasyNotice.Email;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EasyNotice.UnitTests
{
    /// <summary>
    /// 配置飞书群机器人官方文档
    /// https://open.feishu.cn/document/ukTMukTMukTM/ucTM5YjL3ETO24yNxkjN?lang=zh-CN
    /// </summary>
    public class FeishuNoticeTest
    {
        private readonly IFeishuProvider _feishuProvider;

        public FeishuNoticeTest()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddEasyNotice(config =>
            {
                config.IntervalSeconds = 10;//同一标题的消息，10秒内只能发一条，避免短时间内大量发送重复消息
                config.UseFeishu(option =>
                {
                    option.WebHook = "https://open.feishu.cn/open-apis/bot/v2/hook/xxxxx";//通知地址
                    option.Secret = "secret";//签名校验
                });
            });
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            _feishuProvider = serviceProvider.GetService<IFeishuProvider>();
        }

        [Fact]
        public async Task Feishu_Send_Should_Be_Succeed()
        {
            var response = await _feishuProvider.SendAsync("通知标题", new Exception("custom exception"));
            Assert.True(response.IsSuccess);
        }
    }
}
