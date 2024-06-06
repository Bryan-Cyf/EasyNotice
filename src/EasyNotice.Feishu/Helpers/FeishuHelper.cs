using System;
using System.Security.Cryptography;
using System.Text;

namespace EasyNotice.Feishu
{
    internal class FeishuHelper
    {
        public static string GetSign(string timestamp,string secret)
        {
            return GetHmac(timestamp, secret);
        }

        private static string GetHmac(string timestamp, string secret)
        {
            var stringToSign = $"{timestamp}\n{secret}";
            using (var hmacsha256 = new HMACSHA256Final(Encoding.UTF8.GetBytes(stringToSign)))
            {
                return Convert.ToBase64String(hmacsha256.GetHashFinal());
            }
        }
    }

    public class HMACSHA256Final : HMACSHA256
    {
        public HMACSHA256Final(byte[] bytes) : base(bytes)
        {

        }
        public byte[] GetHashFinal()
        {

            return base.HashFinal();
        }
    }
}
