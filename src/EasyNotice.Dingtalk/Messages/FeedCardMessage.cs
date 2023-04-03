using System;
using System.Collections.Generic;
using System.Text;

namespace EasyNotice.Dingtalk
{
    public class FeedCardMessage : MessageBase
    {
        public FeedCardMessage() : base("feedCard")
        {
        }

        public Feedcard feedCard { get; set; }
        public class Feedcard
        {
            public Link[] links { get; set; }
        }

        public class Link
        {
            public string title { get; set; }
            public string messageURL { get; set; }
            public string picURL { get; set; }
        }

    }
}
