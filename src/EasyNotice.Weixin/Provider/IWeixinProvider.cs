using EasyNotice.Core;
using EasyNotice.Weixin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyNotice
{
    public interface IWeixinProvider : IEasyNotice
    {
        /// <summary>
        /// 发送Markdown类型的消息
        /// </summary>
        /// <param name="title">主题</param>
        /// <param name="message">消息</param>
        /// <returns></returns>
        Task<EasyNoticeSendResponse> SendMarkdownMessageAsync(string title, string message);

        /// <summary>
        /// 发送图片
        /// </summary>
        /// <param name="title">主题</param>
        /// <param name="base64">图片的base64</param>
        /// <param name="md5">文件的MD5</param>
        /// <returns></returns>
        Task<EasyNoticeSendResponse> SendImageMessageAsync(string title, string base64, string md5);

        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="title">主题</param>
        /// <param name="newstitle">新闻标题</param>
        /// <param name="description">新闻详情</param>
        /// <param name="url">链接</param>
        /// <param name="picurl">图片链接</param>
        /// <returns></returns>
        Task<EasyNoticeSendResponse> SendNewsMessageAsync(string title, string newstitle, string description, string url, string picurl);

        /// <summary>
        /// 发送图文消息
        /// </summary>
        Task<EasyNoticeSendResponse> SendNewsMessageAsync(string title, List<NewsContent> news);
    }
}
