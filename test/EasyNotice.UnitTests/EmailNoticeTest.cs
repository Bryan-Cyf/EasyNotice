using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EasyNotice.UnitTests
{
    public class EmailNoticeTest
    {
        private readonly IEmailProvider _emailProvider;

        public EmailNoticeTest()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddEasyNotice(config =>
            {
                config.IntervalSeconds = 10;//同一标题的消息，10秒内只能发一条，避免短时间内大量发送重复消息
                config.UseEmail(option =>
                {
                    option.Host = "smtp.qq.com";
                    option.Port = 465;
                    option.FromName = "System";
                    option.FromAddress = "12345@qq.com";
                    option.Password = "passaword";
                    option.ToAddress = new List<string>()
                    {
                        "12345@qq.com"
                    };
                });
            });
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            _emailProvider = serviceProvider.GetService<IEmailProvider>();
        }

        [Fact]
        public async Task Email_Send_Should_Be_Succeed()
        {
            var response = await _emailProvider.SendAsync("邮件标题", new Exception("custom exception"));
            Assert.True(response.IsSuccess);
        }
    }
}
