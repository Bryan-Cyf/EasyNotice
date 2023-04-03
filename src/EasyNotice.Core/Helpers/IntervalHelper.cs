using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyNotice.Core
{
    public class IntervalHelper
    {
        public static ConcurrentDictionary<string, DateTimeOffset> _keyValuePairs = new ConcurrentDictionary<string, DateTimeOffset>();

        public static async Task<T> IntervalExcuteAsync<T>(Func<Task<T>> func, string code, int intervalSecond) where T : new()
        {
            if (intervalSecond <= 0 || string.IsNullOrEmpty(code))
            {
                return await func();
            }

            if (_keyValuePairs.TryGetValue(code, out var time) && (DateTimeOffset.Now - time).TotalSeconds < intervalSecond)
            {
                return new T();
            }

            _keyValuePairs.AddOrUpdate(code, x => DateTimeOffset.Now, (_, __) => DateTimeOffset.Now);
            return await func();
        }


        public static async Task IntervalExcuteAsync(Func<Task> func, string code, int intervalSecond)
        {
            if (intervalSecond <= 0 || string.IsNullOrEmpty(code))
            {
                await func();
                return;
            }

            if (_keyValuePairs.TryGetValue(code, out var time) && (DateTimeOffset.Now - time).TotalSeconds < intervalSecond)
            {
                return;
            }

            _keyValuePairs.AddOrUpdate(code, x => DateTimeOffset.Now, (_, __) => DateTimeOffset.Now);
            await func();
        }
    }
}
