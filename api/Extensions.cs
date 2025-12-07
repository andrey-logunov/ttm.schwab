using Newtonsoft.Json.Linq;
using System;

namespace ttm.schwab.api
{
    internal static class Extensions
    {
        public static DateTime AsDateTimeFromUnixTimeMilliseconds(this JToken jtoken, string key, DateTime defaultValue)
        {
            if (long.TryParse(jtoken.AsString(key, ""), out long milliseconds))
            {
                return common.DateConverter.FromUnixTimeMilliseconds(milliseconds);
            }
            return defaultValue;
        }

        public static double AsDouble(this JToken jtoken, string key, double defaultValue = 0.0)
        {
            return double.TryParse(jtoken.AsString(key, ""), out double number) ? number : defaultValue;
        }

        public static long AsLong(this JToken jtoken, string key, long defaultValue = 0)
        {
            return long.TryParse(jtoken.AsString(key, ""), out long number) ? number : defaultValue;
        }

        public static int AsInteger(this JToken jtoken, string key, int defaultValue = 0)
        {
            return int.TryParse(jtoken.AsString(key, ""), out int number) ? number : defaultValue;
        }

        public static string AsString(this JToken jtoken, string key, string defaultValue = "")
        {
            return jtoken.Value<string>(key) ?? defaultValue;
        }
    }
}
