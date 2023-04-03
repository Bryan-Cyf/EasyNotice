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
        /// <returns></returns>
        Task<EasyNoticeSendResponse> SendAsync(string title, Exception ex);
    }
}
