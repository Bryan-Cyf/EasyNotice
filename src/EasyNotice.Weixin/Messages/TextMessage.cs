using EasyNotice.Core;

namespace EasyNotice.Weixin
{
    public class TextMessage : MessageBase
    {
        public TextMessage(string content) : base("text")
        {
            text = new Text
            {
                content = content,
            };
        }
        public TextMessage(string content, EasyNoticeAtUser atUser) : this(content)
        {
            if (atUser != null)
            {
                if (atUser.UserId?.Length > 0)
                {
                    text.mentioned_list = atUser.UserId;
                }

                if (atUser.Mobile?.Length > 0)
                {
                    text.mentioned_mobile_list = atUser.Mobile;
                }

                if (atUser.IsAtAll)
                {
                    text.mentioned_list = new string[] { "@all" };
                }
            }
        }

        public Text text { get; set; }

        public class Text
        {
            /// <summary>
            /// 文本内容，最长不超过2048个字节，必须是utf8编码
            /// </summary>
            public string content { get; set; }

            /// <summary>
            /// userid的列表，提醒群中的指定成员(@某个成员)，@all表示提醒所有人，如果开发者获取不到userid，可以使用mentioned_mobile_lis
            /// </summary>
            public string[] mentioned_list { get; set; }

            /// <summary>
            /// 手机号列表，提醒手机号对应的群成员(@某个成员)，@all表示提醒所有人
            /// </summary>
            public string[] mentioned_mobile_list { get; set; }
        }
    }
}
