using EasyNotice.Core;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EasyNotice.Feishu
{
    /// <summary>
    /// 配置飞书群机器人官方文档
    /// https://open.feishu.cn/document/ukTMukTMukTM/ucTM5YjL3ETO24yNxkjN?lang=zh-CN
    /// </summary>
    internal class FeishuProvider : IFeishuProvider
    {
        private static HttpClient _httpClient = new HttpClient(new HttpClientHandler
        {
            AutomaticDecompression = System.Net.DecompressionMethods.GZip,
        });

        private readonly FeishuOptions _feishuOptions;
        private readonly NoticeOptions _noticeOptions;

        public FeishuProvider(IOptionsSnapshot<FeishuOptions> FeishuOptions, IOptionsSnapshot<NoticeOptions> noticeOptions)
        {
            _feishuOptions = FeishuOptions.Value;
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
            return SendBaseAsync(title, new TextMessage(title));
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
                    message.timestamp = DateTimeHelper.GetTimestamp.ToString();
                    message.sign = FeishuHelper.GetSign(message.timestamp, _feishuOptions.Secret);
                    var response = await _httpClient.PostAsync(_feishuOptions.WebHook, new StringContent(message.ToString(), Encoding.UTF8, "application/json"));
                    var html = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<EasyNoticeSendResponse>(html);
                }, title, _noticeOptions.IntervalSeconds);
            }
            catch (Exception ex)
            {
                response.ErrMsg = $"飞书发送异常:{ex.Message}";
            }
            return response;
        }
    }
}
