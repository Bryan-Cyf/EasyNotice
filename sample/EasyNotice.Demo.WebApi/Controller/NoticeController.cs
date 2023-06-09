﻿using Microsoft.AspNetCore.Mvc;

namespace EasyNotice.Demo.WebApi.Controller
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class NoticeController : ControllerBase
    {
        private readonly IEmailProvider _mailProvider;
        private readonly IDingtalkProvider _dingtalkProvider;
        private readonly IFeishuProvider _feishuProvider;
        private readonly IWeixinProvider _weixinProvider;

        public NoticeController(IEmailProvider provider,
            IDingtalkProvider dingtalkProvider,
            IFeishuProvider feishuProvider, 
            IWeixinProvider weixinProvider)
        {
            _mailProvider = provider;
            _dingtalkProvider = dingtalkProvider;
            _feishuProvider = feishuProvider;
            _weixinProvider = weixinProvider;
        }

        /// <summary>
        /// 邮件发送
        /// </summary>
        [HttpGet]
        public async Task SendMail([FromQuery] string str)
        {
            await _mailProvider.SendAsync(str, new Exception(str));
        }

        /// <summary>
        /// 钉钉发送
        /// </summary>
        [HttpGet]
        public async Task SendDingTalk([FromQuery] string str)
        {
            await _dingtalkProvider.SendAsync(str, new Exception(str));
        }

        /// <summary>
        /// 飞书发送
        /// </summary>
        [HttpGet]
        public async Task SendFeishu([FromQuery] string str)
        {
            await _feishuProvider.SendAsync(str, new Exception(str));
        }

        /// <summary>
        /// 企业微信发送
        /// </summary>
        [HttpGet]
        public async Task SendWexin([FromQuery] string str)
        {
            await _weixinProvider.SendAsync(str, new Exception(str));
        }
    }
}
