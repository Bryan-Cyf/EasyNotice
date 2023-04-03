using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyNotice.Email
{
    public class EmailOptions
    {
        public const string SectionName = "Mail";

        public string Host { get; set; }

        public int Port { get; set; }

        /// <summary>
        /// 发送人名称
        /// 自定义即可
        /// </summary>
        public string FromName { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        public string FromAddress { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 收件邮箱
        /// </summary>
        public List<string> ToAddress { get; set; }
    }
}
