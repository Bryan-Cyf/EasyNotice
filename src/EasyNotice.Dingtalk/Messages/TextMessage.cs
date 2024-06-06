using System.Linq;
using EasyNotice.Core;

namespace EasyNotice.Dingtalk
{
    public class TextMessage : MessageBase
    {
        public TextMessage(string content) : base("text")
        {
            title = "text";
            text = new Text
            {
                content = content,
            };
        }

        public TextMessage(string content, EasyNoticeAtUser atUser) : this(content)
        {
            if (atUser != null)
            {
                at = new At();
                if (atUser.UserId?.Length > 0)
                {
                    at.atUserIds = atUser.UserId;
                    text.content += at.atUserIds.Select(n => $"@{n}").Aggregate((last, current) => last + "" + current);
                }

                if (atUser.Mobile?.Length > 0)
                {
                    at.atMobiles = atUser.Mobile;
                    text.content += at.atMobiles.Select(n => $"@{n}").Aggregate((last, current) => last + "" + current);
                }

                at.isAtAll = atUser.IsAtAll;
            }
        }

        public Text text { get; set; }
        public At at { get; set; }


        public class Text
        {
            public string content { get; set; }
        }


    }
}
