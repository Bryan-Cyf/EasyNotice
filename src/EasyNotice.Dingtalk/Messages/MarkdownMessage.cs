using EasyNotice.Core;
using Newtonsoft.Json;
using System.Linq;

namespace EasyNotice.Dingtalk
{
    public class MarkdownMessage : MessageBase
    {
        public MarkdownMessage(string title, string content) : base("markdown")
        {
            this.title = title;
            markdown = new Markdown
            {
                title = title,
                text = content,
            };
        }

        public MarkdownMessage(string title, string content, EasyNoticeAtUser atUser) : this(title, content)
        {
            if (atUser != null)
            {
                at = new At();
                if (atUser.UserId?.Length > 0)
                {
                    at.atUserIds = atUser.UserId;
                    markdown.text += at.atUserIds.Select(n => $"@{n}").Aggregate((last, current) => last + "" + current);
                }

                if (atUser.Mobile?.Length > 0)
                {
                    at.atMobiles = atUser.Mobile;
                    markdown.text += at.atMobiles.Select(n => $"@{n}").Aggregate((last, current) => last + "" + current);
                }

                at.isAtAll = atUser.IsAtAll;
            }
        }
        public Markdown markdown { get; set; }
        public At at { get; set; }
        public class Markdown
        {
            public string title { get; set; }
            public string text { get; set; }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
