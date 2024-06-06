using EasyNotice.Core;
using EasyNotice.Dingtalk;
using System.Threading.Tasks;

namespace EasyNotice
{
    public interface IDingtalkProvider : IEasyNotice
    {
        Task<EasyNoticeSendResponse> SendTextAsync(string text, EasyNoticeAtUser atUser = null);

        Task<EasyNoticeSendResponse> SendTextAsync(TextMessage message);

        Task<EasyNoticeSendResponse> SendMarkdownAsync(string title, string text, EasyNoticeAtUser atUser = null);

        Task<EasyNoticeSendResponse> SendMarkdownAsync(MarkdownMessage message);

        Task<EasyNoticeSendResponse> SendActionCardAsync(string title, string text, string singleTitle, string singleURL, string btnOrientation = "0");

        Task<EasyNoticeSendResponse> SendActionCardAsync(ActionCardMessage message);

    }
}
