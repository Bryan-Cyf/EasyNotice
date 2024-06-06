using Newtonsoft.Json;

namespace EasyNotice.Feishu
{
    public class MessageBase
    {
        public MessageBase(string msgtype)
        {
            this.msg_type = msgtype;
        }

        public string msg_type { get; set; }

        public string timestamp { get; set; }

        public string sign { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
