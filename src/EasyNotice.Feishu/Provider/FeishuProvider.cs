using EasyNotice.Core;
using EasyNotice.Dingtalk;
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

        public FeishuProvider(IOptions<FeishuOptions> FeishuOptions, IOptions<NoticeOptions> noticeOptions)
        {
            _feishuOptions = FeishuOptions.Value;
            _noticeOptions = noticeOptions.Value;
        }

        /// <summary>
        /// 发送异常消息
        /// </summary>
        public Task<EasyNoticeSendResponse> SendAsync(string title, Exception exception, EasyNoticeAtUser atUser = null)
        {
            var text = $"{title}{Environment.NewLine}{exception.Message}{Environment.NewLine}{exception}";
            return SendBaseAsync(title, new TextMessage(text, atUser));
        }

        /// <summary>
        /// 发送普通消息
        /// </summary>
        public Task<EasyNoticeSendResponse> SendAsync(string title, string message, EasyNoticeAtUser atUser = null)
        {
            return SendBaseAsync(title, new TextMessage(title, atUser));
        }

        /// <summary>
        /// 发送消息公共方法
        /// </summary>
        private async Task<EasyNoticeSendResponse> SendBaseAsync(string title, MessageBase message)
        {
            try
            {
                return await IntervalHelper.IntervalExcuteAsync(async () =>
                {
                    message.timestamp = DateTimeHelper.GetTimestamp.ToString();
                    message.sign = FeishuHelper.GetSign(message.timestamp, _feishuOptions.Secret);
                    var response = await _httpClient.PostAsync(_feishuOptions.WebHook, new StringContent(message.ToString(), Encoding.UTF8, "application/json"));
                    var html = await response.Content.ReadAsStringAsync();
                    var feishuResponse = JsonConvert.DeserializeObject<FeishuResponse>(html);
                    if (feishuResponse.IsSuccess)
                    {
                        return new EasyNoticeSendResponse() { ErrCode = 0, ErrMsg = "" };
                    }
                    else
                    {
                        return new EasyNoticeSendResponse() { ErrCode = feishuResponse.Code, ErrMsg = feishuResponse.Msg };
                    }
                }, title, _noticeOptions.IntervalSeconds);
            }
            catch (Exception ex)
            {
                return new EasyNoticeSendResponse() { ErrCode = 9999, ErrMsg = $"飞书发送消息异常:{ex.Message}" };
            }
        }
    }
}
