using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using EasyNotice.Core;
using Xunit;

namespace EasyNotice.UnitTests
{
    /// <summary>
    /// 配置企业微信群机器人官方文档
    /// https://developer.work.weixin.qq.com/document/path/91770
    /// </summary>
    public class WeixinNoticeTest
    {
        private readonly IWeixinProvider _weixinProvider;

        public WeixinNoticeTest()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddEasyNotice(config =>
            {
                config.IntervalSeconds = 10;//同一标题的消息，10秒内只能发一条，避免短时间内大量发送重复消息
                config.UseWeixin(option =>
                {
                    option.WebHook = "https://qyapi.weixin.qq.com/cgi-bin/webhook/send?key=xxxxx";//通知地址
                });
            });
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            _weixinProvider = serviceProvider.GetService<IWeixinProvider>();
        }

        [Fact]
        public async Task Weixin_Send_Should_Be_Succeed()
        {
            var response = await _weixinProvider.SendAsync("通知标题", new Exception("custom exception"));
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task Weixin_Send_AtUser_Should_Be_Succeed()
        {
            var response = await _weixinProvider.SendAsync("通知标题", new Exception("custom exception"), new EasyNoticeAtUser(){IsAtAll = false, Mobile = new []{"138xxxxxxxx"}});
            Assert.True(response.IsSuccess);
        }
    }
}
