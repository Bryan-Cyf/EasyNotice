using EasyNotice.Core;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EasyNotice.Weixin
{
    /// <summary>
    /// 配置企业微信群机器人官方文档
    /// https://developer.work.weixin.qq.com/document/path/91770
    /// </summary>
    internal class WeixinProvider : IWeixinProvider
    {
        private static HttpClient _httpClient = new HttpClient(new HttpClientHandler
        {
            AutomaticDecompression = System.Net.DecompressionMethods.GZip,
        });

        private readonly WeixinOptions _WeixinOptions;
        private readonly NoticeOptions _noticeOptions;

        public WeixinProvider(IOptionsSnapshot<WeixinOptions> WeixinOptions, IOptionsSnapshot<NoticeOptions> noticeOptions)
        {
            _WeixinOptions = WeixinOptions.Value;
            _noticeOptions = noticeOptions.Value;
        }

        /// <summary>
        /// 发送异常消息
        /// </summary>
        public Task<EasyNoticeSendResponse> SendAsync(string title, Exception exception)
        {
            var text = $"{title}{Environment.NewLine}{exception.Message}{Environment.NewLine}{exception}";
            return SendBaseAsync(title, new TextMessage(text));
        }

        /// <summary>
        /// 发送普通消息
        /// </summary>
        public Task<EasyNoticeSendResponse> SendAsync(string title, string message)
        {
            return SendBaseAsync(title, new TextMessage(message));
        }

        /// <summary>
        /// 发送Markdown类型消息
        /// </summary>
        public Task<EasyNoticeSendResponse> SendMarkdownMessageAsync(string title, string message)
        {
            return SendBaseAsync(title, new MarkdownMessage(message));
        }


        /// <summary>
        /// 发送消息公共方法
        /// </summary>
        private async Task<EasyNoticeSendResponse> SendBaseAsync(string title, MessageBase message)
        {
            var response = new EasyNoticeSendResponse();
            try
            {
                return await IntervalHelper.IntervalExcuteAsync(async () =>
                {
                    var response = await _httpClient.PostAsync(_WeixinOptions.WebHook, new StringContent(message.ToString(), Encoding.UTF8, "application/json"));
                    var html = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<EasyNoticeSendResponse>(html);
                }, title, _noticeOptions.IntervalSeconds);
            }
            catch (Exception ex)
            {
                response.ErrMsg = $"企业微信发送异常:{ex.Message}";
            }
            return response;
        }
  

    }
}
