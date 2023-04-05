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

        public FeishuProvider(IOptionsMonitor<FeishuOptions> FeishuOptions, IOptionsMonitor<NoticeOptions> noticeOptions)
        {
            _feishuOptions = FeishuOptions.CurrentValue;
            _noticeOptions = noticeOptions.CurrentValue;
        }

        /// <summary>
        /// 发送异常消息
        /// </summary>
        public Task<EasyNoticeSendResponse> SendAsync(string title, Exception exception)
        {
            var text = $"# {title}{Environment.NewLine}{exception.Message}{Environment.NewLine}{exception}";
            return SendBaseAsync(new TextMessage(text));
        }

        /// <summary>
        /// 发送普通消息
        /// </summary>
        public Task<EasyNoticeSendResponse> SendAsync(string title, string message)
        {
            return SendBaseAsync(new TextMessage(title));
        }

        /// <summary>
        /// 发送消息公共方法
        /// </summary>
        private async Task<EasyNoticeSendResponse> SendBaseAsync(MessageBase message)
        {
            var response = new EasyNoticeSendResponse();
            try
            {
                return await IntervalHelper.IntervalExcuteAsync(async () =>
                {
                    message.timestamp = DateTimeHelper.GetTimestanp().ToString();
                    message.sign = FeishuHelper.GetSign(message.timestamp, _feishuOptions.Secret);
                    var response = await _httpClient.PostAsync(_feishuOptions.WebHook, new StringContent(message.ToString(), Encoding.UTF8, "application/json"));
                    var html = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<EasyNoticeSendResponse>(html);
                }, message.msg_type, _noticeOptions.IntervalSeconds);
            }
            catch (Exception ex)
            {
                response.ErrMsg = $"钉钉发送异常:{ex.Message}";
            }
            return response;
        }
    }
}
