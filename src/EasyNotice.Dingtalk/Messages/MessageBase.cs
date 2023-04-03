using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyNotice.Dingtalk
{
    public class MessageBase
    {
        public MessageBase(string msgtype)
        {
            this.msgtype = msgtype;
        }

        public string title { get; set; }

        public string msgtype { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }


        public class At
        {
            public string[] atMobiles { get; set; }
            public string[] atUserIds { get; set; }
            public bool isAtAll { get; set; }
        }
    }
}
