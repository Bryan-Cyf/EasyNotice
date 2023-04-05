using System;
using System.Collections.Generic;
using System.Text;

namespace EasyNotice.Core
{
    public class DateTimeHelper
    {
        public static long GetTimestanp()
        {
            var expiration = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return (long)expiration.TotalSeconds;
        }
    }
}
