using System.Collections.Generic;

namespace EasyNotice.Weixin
{
    /// <summary>
    /// 图文类型消息
    /// </summary>
    public class NewsMessage : MessageBase
    {
        public NewsMessage(List<NewsContent> newsCotent) : base("news")
        {
            news = new News
            {
                articles = newsCotent,
            };
        }

        public News news { get; set; }
    }

    public class News
    {
        public List<NewsContent> articles { get; set; }
    }

    public class NewsContent
    {
        public string title { get; set; }
        public string description { get; set; }

        public string url { get; set; }

        public string picurl { get; set; }
    }
}
