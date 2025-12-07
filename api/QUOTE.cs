using Newtonsoft.Json.Linq;
using System.Text;

namespace ttm.schwab.api
{
    internal sealed class QUOTE
    {
        public string Symbol { get; private set; }
        public bool RealTime { get; private set; }

        public double High52Wk { get; private set; }
        public double Low52Wk { get; private set; }
        public double AskPrice { get; private set; }
        public int AskSize { get; private set; }
        public double BidPrice { get; private set; }
        public int BidSize { get; private set; }
        public double LastPrice { get; private set; }
        public int LastSize { get; private set; }

        public double ClosePrice { get; private set; }
        public double OpenPrice { get; private set; }
        public double HighPrice { get; private set; }
        public double LowPrice { get; private set; }
        public double MarkPrice { get; private set; }

        public long TotalVolume { get; private set; }
        public string SecurityStatus { get; private set; }

        private QUOTE() { }

        public static QUOTE[] Parse(JObject jobject)
        {
            List<QUOTE> quotes = [];

            if (jobject != null)
            {
                foreach (var keyValue in jobject)
                {
                    JToken jtoken = keyValue.Value;
                    if (jtoken["symbol"] == null || jtoken["quote"] == null) continue;

                    quotes.Add(new QUOTE
                    {
                        Symbol = Convert.ToString(jtoken["symbol"]).Trim().ToUpper(),
                        RealTime = Convert.ToBoolean(jtoken["realtime"]),

                        High52Wk = Convert.ToDouble(jtoken["quote"]["52WeekHigh"]),
                        Low52Wk = Convert.ToDouble(jtoken["quote"]["52WeekLow"]),
                        AskPrice = Convert.ToDouble(jtoken["quote"]["askPrice"]),
                        AskSize = Convert.ToInt32(jtoken["quote"]["askSize"]),
                        BidPrice = Convert.ToDouble(jtoken["quote"]["bidPrice"]),
                        BidSize = Convert.ToInt32(jtoken["quote"]["bidSize"]),
                        LastPrice = Convert.ToDouble(jtoken["quote"]["lastPrice"]),
                        LastSize = Convert.ToInt32(jtoken["quote"]["lastSize"]),

                        ClosePrice = Convert.ToDouble(jtoken["quote"]["closePrice"]),
                        OpenPrice = Convert.ToDouble(jtoken["quote"]["openPrice"]),
                        HighPrice = Convert.ToDouble(jtoken["quote"]["highPrice"]),
                        LowPrice = Convert.ToDouble(jtoken["quote"]["lowPrice"]),
                        MarkPrice = Convert.ToDouble(jtoken["quote"]["mark"]),

                        TotalVolume = Convert.ToInt64(jtoken["quote"]["totalVolume"]),
                        SecurityStatus = Convert.ToString(jtoken["quote"]["securityStatus"]),
                    });
                }
            }

            return [.. quotes];
        }

        public static QUOTE[] Parse(string json)
        {
            return Parse(JObject.Parse(json));
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("QUOTE");
            sb.AppendLine();
            sb.AppendLine($"Symbol:                             {Symbol}");
            sb.AppendLine($"Real Time:                          {RealTime}");
            sb.AppendLine();
            sb.AppendLine($"High52Wk:                           {High52Wk:F2}");
            sb.AppendLine($"Low52Wk:                            {Low52Wk:F2}");
            sb.AppendLine($"Ask Price:                          {AskPrice:F2}");
            sb.AppendLine($"Ask Size:                           {AskSize}");
            sb.AppendLine($"Bid Price:                          {BidPrice:F2}");
            sb.AppendLine($"Bid Size:                           {BidSize}");
            sb.AppendLine($"Last Price:                         {LastPrice:F2}");
            sb.AppendLine($"Last Size:                          {LastSize}");

            sb.AppendLine($"Close Price:                        {ClosePrice:F2}");
            sb.AppendLine($"Open Price:                         {OpenPrice:F2}");
            sb.AppendLine($"High Price:                         {HighPrice:F2}");
            sb.AppendLine($"Low Price:                          {LowPrice:F2}");
            sb.AppendLine($"Mark Price:                         {MarkPrice:F2}");

            sb.AppendLine($"TotalVolume:                        {TotalVolume}");
            sb.AppendLine($"SecurityStatus:                     {SecurityStatus}");
            return sb.ToString();
        }
    }
}
