using Newtonsoft.Json;

namespace EasyNotice.Weixin
{
    public class WeixinResponse
    {
        public bool IsSuccess => ErrCode == 0;

        /// <summary>
        /// 错误码，为0表示成功
        /// </summary>
        [JsonProperty("errcode")]
        public int ErrCode { get; set; }

        /// <summary>
        /// 错误消息，成功时为ok
        /// </summary>
        [JsonProperty("errmsg")]
        public string ErrMsg { get; set; }
    }
}
