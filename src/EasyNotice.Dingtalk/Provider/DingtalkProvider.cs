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

        public async Task<EasyNoticeSendResponse> SendAsync(string title, Exception exception) => await SendMarkdownAsync(title, $"# {title}{Environment.NewLine}{exception.Message}{Environment.NewLine}{exception}");
        public async Task<EasyNoticeSendResponse> SendAsync(string title, string message) => await SendMarkdownAsync(title, message);


        public Task<EasyNoticeSendResponse> SendTextAsync(string text) => SendTextAsync(new TextMessage(text));

        public Task<EasyNoticeSendResponse> SendTextAsync(TextMessage message) => SendAsync(message);

        public Task<EasyNoticeSendResponse> SendMarkdownAsync(string title, string text) => SendMarkdownAsync(new MarkdownMessage(title, text));

        public Task<EasyNoticeSendResponse> SendMarkdownAsync(MarkdownMessage message) => SendAsync(message);

        public Task<EasyNoticeSendResponse> SendActionCardAsync(string title, string text, string singleTitle, string singleURL, string btnOrientation = "0") => SendActionCardAsync(new ActionCardMessage(title, text, singleTitle, singleURL, btnOrientation));

        public Task<EasyNoticeSendResponse> SendActionCardAsync(ActionCardMessage message) => SendAsync(message);

        private async Task<EasyNoticeSendResponse> SendAsync(MessageBase message)
        {
            return await IntervalHelper.IntervalExcuteAsync<EasyNoticeSendResponse>(async () =>
            {
                var requestUrl = DingTalkHelper.GetRequestUrl(_dingTalkOptions.WebHook, _dingTalkOptions.Secret);
                var response = await _httpClient.PostAsync(requestUrl, new StringContent(message.ToString(), Encoding.UTF8, "application/json"));
                var html = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<EasyNoticeSendResponse>(html);
            }, message.title, _noticeOptions.IntervalSeconds);

        }
    }
}
