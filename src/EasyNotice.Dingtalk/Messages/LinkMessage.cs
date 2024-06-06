namespace EasyNotice.Dingtalk
{
    public class LinkMessage : MessageBase
    {
        public LinkMessage() : base("link")
        {
        }
        public Link link { get; set; }
        public class Link
        {
            public string text { get; set; }
            public string title { get; set; }
            public string picUrl { get; set; }
            public string messageUrl { get; set; }
        }
    }
}
