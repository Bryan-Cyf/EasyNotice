using Newtonsoft.Json;

namespace EasyNotice.Weixin
{
    public class MessageBase
    {
        public MessageBase(string msgtype)
        {
            this.msgtype = msgtype;
        }

        public string msgtype { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
