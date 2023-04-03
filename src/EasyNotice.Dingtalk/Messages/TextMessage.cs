using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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

        public Text text { get; set; }
        public At at { get; set; }


        public class Text
        {
            public string content { get; set; }
        }


    }
}
