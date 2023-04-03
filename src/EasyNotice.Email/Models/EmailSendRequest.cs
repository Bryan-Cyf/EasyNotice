using System;
using System.Collections.Generic;
using System.Text;

namespace EasyNotice.Email
{
    public class EmailSendRequest
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public byte[] Attachments { get; set; }
        public string FileName { get; set; } = "未命名文件.txt";
    }
}
