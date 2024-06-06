using Newtonsoft.Json;

namespace EasyNotice.Dingtalk
{
    public class FeishuResponse
    {
        public bool IsSuccess => Code == 0;

        /// <summary>
        /// 错误码，为0表示成功
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>
        /// 错误消息，成功时为ok
        /// </summary>
        [JsonProperty("msg")]
        public string Msg { get; set; }


    }
}
