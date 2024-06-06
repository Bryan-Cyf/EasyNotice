using EasyNotice.Core;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public WeixinProvider(IOptions<WeixinOptions> WeixinOptions, IOptions<NoticeOptions> noticeOptions)
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
        /// 发送图片类型消息
        /// </summary>
        public Task<EasyNoticeSendResponse> SendImageMessageAsync(string title, string base64, string md5)
        {
            return SendBaseAsync(title, new ImageMessage(base64, md5));
        }

        /// <summary>
        /// 发送图文类型的消息
        /// </summary>
        public Task<EasyNoticeSendResponse> SendNewsMessageAsync(string title, string newstitle, string description, string url, string picurl)
        {

            List<NewsContent> news = new List<NewsContent>() {
                new NewsContent() {
                    title = newstitle,
                    description = description,
                    url = url,
                    picurl = picurl
                }
            };
            return SendNewsMessageAsync(title, news);
        }


        /// <summary>
        /// 发送图文类型的消息
        /// </summary>
        public async Task<EasyNoticeSendResponse> SendNewsMessageAsync(string title, List<NewsContent> news)
        {
            var response = new EasyNoticeSendResponse();
            if (news != null && news.Count > 1 && news.Count <= 8)
            {
                return await SendBaseAsync(title, new NewsMessage(news));
            }
            else
            {
                response.ErrMsg = "图文消息仅支持1到8条图文";
            }

            return response;

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
                    var response = await _httpClient.PostAsync(_WeixinOptions.WebHook, new StringContent(message.ToString(), Encoding.UTF8, "application/json"));
                    var html = await response.Content.ReadAsStringAsync();
                    var weixinResponse = JsonConvert.DeserializeObject<WeixinResponse>(html);
                    if (weixinResponse.IsSuccess)
                    {
                        return new EasyNoticeSendResponse() { ErrCode = 0, ErrMsg = "" };
                    }
                    else
                    {
                        return new EasyNoticeSendResponse() { ErrCode = weixinResponse.ErrCode, ErrMsg = weixinResponse.ErrMsg };
                    }
                }, title, _noticeOptions.IntervalSeconds);
            }
            catch (Exception ex)
            {
                return new EasyNoticeSendResponse() {ErrCode = 9999, ErrMsg = $"企业微信发送消息异常:{ex.Message}" };
            }
        }
    }
}
