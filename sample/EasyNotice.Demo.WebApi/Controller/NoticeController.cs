using Microsoft.AspNetCore.Mvc;

namespace EasyNotice.Demo.WebApi.Controller
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class NoticeController : ControllerBase
    {
        private readonly IEmailProvider _mailProvider;
        private readonly IDingtalkProvider _dingtalkProvider;
        private readonly IFeishuProvider _feishuProvider;

        public NoticeController(IEmailProvider provider,
            IDingtalkProvider dingtalkProvider,
            IFeishuProvider feishuProvider)
        {
            _mailProvider = provider;
            _dingtalkProvider = dingtalkProvider;
            _feishuProvider = feishuProvider;
        }

        [HttpGet]
        public async Task SendMail([FromQuery] string str)
        {
            await _mailProvider.SendAsync(str, new Exception(str));
        }

        [HttpGet]
        public async Task SendDingTalk([FromQuery] string str)
        {
            await _dingtalkProvider.SendAsync(str, new Exception(str));
        }

        [HttpGet]
        public async Task SendFeishu([FromQuery] string str)
        {
            await _feishuProvider.SendAsync(str, new Exception(str));
        }
    }
}
