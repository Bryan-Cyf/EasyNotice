using System;
using System.Threading.Tasks;

namespace EasyNotice.Core
{
    public interface IEasyNotice
    {
        /// <summary>
        /// 推送异常消息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="ex">异常</param>
        /// <param name="atUser">提醒指定人员</param>
        /// <returns></returns>
        Task<EasyNoticeSendResponse> SendAsync(string title, Exception ex, EasyNoticeAtUser atUser = null);

        /// <summary>
        /// 推送正常消息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容</param>
        /// <param name="atUser">提醒指定人员</param>
        /// <returns></returns>
        Task<EasyNoticeSendResponse> SendAsync(string title, string message, EasyNoticeAtUser atUser = null);
    }
}
