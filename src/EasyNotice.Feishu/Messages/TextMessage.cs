using System.Linq;
using EasyNotice.Core;

namespace EasyNotice.Feishu
{
    public class TextMessage : MessageBase
    {
        public TextMessage(string text) : base("text")
        {
            content = new Content
            {
                text = text,
            };
        }
        public TextMessage(string content, EasyNoticeAtUser atUser) : this(content)
        {
            if (atUser != null)
            {
                if (atUser.UserId?.Length > 0)
                {
                    content += atUser.UserId.Select(n => $"<at user_id=\"{n}\"></at>").Aggregate((last, current) => last + "" + current);
                }

                if (atUser.IsAtAll)
                {
                    content += "<at user_id=\"all\">所有人</at>";
                }
            }
        }

        public Content content { get; set; }

        public class Content
        {
            public string text { get; set; }
        }
    }
}
