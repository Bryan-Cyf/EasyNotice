using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace EasyNotice.Dingtalk
{
    internal class DingTalkHelper
    {
        public static string GetRequestUrl(string webhook, string secret)
        {
            var timeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var stringToSign = $"{timeStamp}\n{secret}";
            var b64 = HMACSHA256Bytes(stringToSign, secret);
            var b64Str = Convert.ToBase64String(b64);
            var sign = HttpUtility.UrlEncode(b64Str, Encoding.UTF8);
            var newUrl = $"{webhook}&timestamp={timeStamp}&sign={sign}";
            return newUrl;
        }

        public static byte[] HMACSHA256Bytes(string srcString, string key)
        {
            using (HMACSHA256 hMACSHA = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                hMACSHA.Initialize();
                byte[] bytes = Encoding.UTF8.GetBytes(srcString);
                return hMACSHA.ComputeHash(bytes);
            }
        }
    }
}
