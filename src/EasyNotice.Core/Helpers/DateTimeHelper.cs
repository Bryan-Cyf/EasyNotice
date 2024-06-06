using System;

namespace EasyNotice.Core
{
    public class DateTimeHelper
    {
        public static long GetTimestamp => DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}
