using System;
using System.Collections.Generic;
using System.Text;

namespace EasyNotice.Core
{
    public class DateTimeHelper
    {
        public static long GetTimestamp => DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}
