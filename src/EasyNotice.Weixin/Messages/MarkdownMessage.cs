using System;
using System.Collections.Generic;
using System.Text;

namespace EasyNotice.Weixin
{
    public class MarkdownMessage : MessageBase
    {
        public MarkdownMessage(string content) : base("markdown")
        {
            markdown = new Markdown
            {
                content = content,
            };
        }

        public Markdown markdown { get; set; }

        public class Markdown
        {
            public string content { get; set; }
        }
    }
}
