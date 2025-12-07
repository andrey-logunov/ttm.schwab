using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace ttm.schwab.api
{
    internal sealed class PRICE
    {
        public DateTime Date { get; private set; }
        public double Open { get; private set; }
        public double High { get; private set; }
        public double Low { get; private set; }
        public double Close { get; private set; }
        public double Volume { get; private set; }

        private PRICE() { }

        public static PRICE Parse(JToken jtoken)
        {
            if (jtoken == null) return null;

            return new PRICE
            {
                Date = common.DateConverter.FromUnixTimeMilliseconds(jtoken.AsLong("datetime")),
                Open = jtoken.AsDouble("open"),
                High = jtoken.AsDouble("high"),
                Low = jtoken.AsDouble("low"),
                Close = jtoken.AsDouble("close"),
                Volume = jtoken.AsDouble("volume")
            };
        }

        public static PRICE Parse(string json)
        {
            return Parse(JToken.Parse(json));
        }

        public static PRICE[] ParseArray(JToken jtoken)
        {
            return jtoken.Children().Select(Parse).Where(p => p != null).ToArray();
        }

        public static PRICE[] ParseArray(string json)
        {
            return ParseArray(JToken.Parse(json));
        }

        public override string ToString()
        {
            return $"[{Date:yyyy-MM-dd HH:mm:ss}],[{Open}],[{High}],[{Low}],[{Close}],[{Volume}]";
        }
    }
}
