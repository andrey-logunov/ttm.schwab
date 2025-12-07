using System;

namespace ttm.schwab.common
{
    internal static class DateConverter
    {
        readonly static DateTime epoch = new (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long ToUnixTimeMilliseconds(DateTime date)
        {
            return (long)(date.ToUniversalTime() - epoch).TotalMilliseconds;
        }

        public static DateTime FromUnixTimeMilliseconds(long milliseconds)
        {
            return epoch.Add(TimeSpan.FromMilliseconds(milliseconds)).ToLocalTime();
        }
    }
}
