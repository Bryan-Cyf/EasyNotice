using System;
using Newtonsoft.Json;

namespace EasyNotice.Dingtalk
{
    public class DingtalkResponse
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

        /// <summary>
        /// 错误描述，钉钉返回错误格式为：
        /// {"errcode":310000,"errmsg":"description:ip 不在白名单中;solution:请联系对应群的管理员，设置机器人安全ip地址段"}
        /// </summary>
        public string Description
        {
            get
            {
                if (!string.IsNullOrEmpty(ErrMsg) && ErrMsg.Contains("description:"))
                {
                    return ErrMsg.Split(new []{ "description:" }, StringSplitOptions.None)[1].Split(new[] { ';' })[0];
                }
                return string.Empty;
            }
        }

        public string Solution
        {
            get
            {
                if (!string.IsNullOrEmpty(ErrMsg) && ErrMsg.Contains("solution:"))
                {
                    return ErrMsg.Split(new []{ "solution:" }, StringSplitOptions.None)[1];
                }
                return string.Empty;
            }
        }
    }
}
