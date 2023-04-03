using EasyNotice.Core;
using EasyNotice.Dingtalk;
using System;
using System.Threading.Tasks;

namespace EasyNotice
{
    public interface IDingtalkProvider : IEasyNotice
    {
        Task<EasyNoticeSendResponse> SendAsync(string title, Exception ex);

        Task<EasyNoticeSendResponse> SendTextAsync(string text);

        Task<EasyNoticeSendResponse> SendTextAsync(TextMessage message);

        Task<EasyNoticeSendResponse> SendMarkdownAsync(string title, string text);

        Task<EasyNoticeSendResponse> SendMarkdownAsync(MarkdownMessage message);

        Task<EasyNoticeSendResponse> SendActionCardAsync(string title, string text, string singleTitle, string singleURL, string btnOrientation = "0");

        Task<EasyNoticeSendResponse> SendActionCardAsync(ActionCardMessage message);

    }
}
