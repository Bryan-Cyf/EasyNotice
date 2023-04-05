using EasyNotice.Core;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EasyNotice.Dingtalk
{
    /// <summary>
    /// https://developers.dingtalk.com/document/app/custom-robot-access
    /// </summary>
    internal class DingtalkProvider : IDingtalkProvider
    {
        private static HttpClient _httpClient = new HttpClient(new HttpClientHandler
        {
            AutomaticDecompression = System.Net.DecompressionMethods.GZip,
        });

        private readonly DingtalkOptions _dingTalkOptions;
        private readonly NoticeOptions _noticeOptions;

        public DingtalkProvider(IOptionsMonitor<DingtalkOptions> dingTalkOptions, IOptionsMonitor<NoticeOptions> noticeOptions)
        {
            _dingTalkOptions = dingTalkOptions.CurrentValue;
            _noticeOptions = noticeOptions.CurrentValue;
        }

        /// <summary>
        /// 发送异常消息
        /// </summary>
        public Task<EasyNoticeSendResponse> SendAsync(string title, Exception exception)
        {
            var text = $"# {title}{Environment.NewLine}{exception.Message}{Environment.NewLine}{exception}";
            return SendMarkdownAsync(title, text);
        }

        /// <summary>
        /// 发送普通消息
        /// </summary>
        public Task<EasyNoticeSendResponse> SendAsync(string title, string message)
        {
            return SendMarkdownAsync(title, message);
        }

        #region 文本消息

        /// <summary>
        /// 发送文本消息
        /// </summary>
        public Task<EasyNoticeSendResponse> SendTextAsync(string text)
        {
            return SendTextAsync(new TextMessage(text));
        }

        /// <summary>
        /// 发送文本消息
        /// </summary>
        public Task<EasyNoticeSendResponse> SendTextAsync(TextMessage message)
        {
            return SendBaseAsync(message);
        }

        #endregion

        #region MarkDown消息

        /// <summary>
        /// 发送MarkDown消息
        /// </summary>
        public Task<EasyNoticeSendResponse> SendMarkdownAsync(string title, string text)
        {
            return SendMarkdownAsync(new MarkdownMessage(title, text));
        }

        /// <summary>
        /// 发送MarkDown消息
        /// </summary>
        public Task<EasyNoticeSendResponse> SendMarkdownAsync(MarkdownMessage message)
        {
            return SendBaseAsync(message);
        }

        #endregion

        #region 消息卡片

        /// <summary>
        /// 发送消息卡片
        /// </summary>
        public Task<EasyNoticeSendResponse> SendActionCardAsync(string title, string text, string singleTitle, string singleURL, string btnOrientation = "0")
        {
            return SendActionCardAsync(new ActionCardMessage(title, text, singleTitle, singleURL, btnOrientation));
        }

        /// <summary>
        /// 发送消息卡片
        /// </summary>
        public Task<EasyNoticeSendResponse> SendActionCardAsync(ActionCardMessage message)
        {
            return SendBaseAsync(message);
        }

        #endregion

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
                    var requestUrl = DingTalkHelper.GetRequestUrl(_dingTalkOptions.WebHook, _dingTalkOptions.Secret);
                    var response = await _httpClient.PostAsync(requestUrl, new StringContent(message.ToString(), Encoding.UTF8, "application/json"));
                    var html = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<EasyNoticeSendResponse>(html);
                }, message.title, _noticeOptions.IntervalSeconds);
            }
            catch (Exception ex)
            {
                response.ErrMsg = $"钉钉发送异常:{ex.Message}";
            }
            return response;
        }
    }
}
