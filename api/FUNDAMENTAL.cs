using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ttm.schwab.api
{
    internal sealed partial class FUNDAMENTAL
    {
        public string Cusip { get; private set; }
        public string Symbol { get; private set; }
        public string Description { get; private set; }
        public string Exchange { get; private set; }

        public double High52Wk { get; private set; }
        public double Low52Wk { get; private set; }

        public double SharesOutstanding { get; private set; }
        public double MarketCap { get; private set; }

        public double Beta { get; private set; }

        public double Avg3MonthVolume { get; private set; }

        public double Eps { get; private set; }

        [GeneratedRegex(@"^[a-zA-Z]+$", RegexOptions.Compiled)]
        private static partial Regex EquityRegex();

        public static FUNDAMENTAL Parse(JToken jtoken)
        {
            if (jtoken == null) return null;

            string cusip = Convert.ToString(jtoken["cusip"]);
            string symbol = Convert.ToString(jtoken["symbol"]);
            string description = Convert.ToString(jtoken["description"]);
            string exchange = Convert.ToString(jtoken["exchange"]);
            string assetType = Convert.ToString(jtoken["assetType"]);

            if (string.IsNullOrWhiteSpace(symbol)) return null;
            if (!EquityRegex().IsMatch(symbol)) return null;
            if (!assetType.Equals("EQUITY", StringComparison.OrdinalIgnoreCase)) return null;
            if (string.IsNullOrWhiteSpace(exchange)) return null;
            exchange = exchange.Trim().ToUpper();
            if (!exchange.Equals("NYSE") && !exchange.Equals("AMEX") && !exchange.Equals("NASDAQ")) return null;

            double high52Wk = Convert.ToDouble(jtoken["fundamental"]["high52"]);
            double low52Wk = Convert.ToDouble(jtoken["fundamental"]["low52"]);
            double sharesOutstanding = Convert.ToDouble(jtoken["fundamental"]["sharesOutstanding"]);
            double marketCap = Convert.ToDouble(jtoken["fundamental"]["marketCap"]);
            double beta = Convert.ToDouble(jtoken["fundamental"]["beta"]);
            double avg3MonthVolume = Convert.ToDouble(jtoken["fundamental"]["avg3MonthVolume"]);
            double eps = Convert.ToDouble(jtoken["fundamental"]["eps"]);

            return new FUNDAMENTAL
            {
                Cusip = cusip,
                Symbol = symbol.Trim().ToUpper(),
                Description = description,
                Exchange = exchange,
                High52Wk = high52Wk,
                Low52Wk = low52Wk,
                SharesOutstanding = sharesOutstanding,
                MarketCap = marketCap,
                Beta = beta,
                Avg3MonthVolume = avg3MonthVolume,
                Eps = eps,
            };
        }

        public static FUNDAMENTAL Parse(string json)
        {
            return Parse(JToken.Parse(json));
        }

        public static FUNDAMENTAL[] ParseArray(JToken jtoken)
        {
            return jtoken != null ? jtoken.Children().Select(Parse).Where(p => p != null).ToArray() : [];
        }

        public static FUNDAMENTAL[] ParseArray(string json)
        {
            return ParseArray(JToken.Parse(json));
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("FUNDAMENTAL");
            sb.AppendLine();
            sb.AppendLine($"Cusip:                              {Cusip}");
            sb.AppendLine($"Symbol:                             {Symbol}");
            sb.AppendLine($"Description:                        {Description}");
            sb.AppendLine($"Exchange:                           {Exchange}");
            sb.AppendLine();
            sb.AppendLine($"High 52 Wk:                         {High52Wk:F2}");
            sb.AppendLine($"Low 52 Wk:                          {Low52Wk:F2}");
            sb.AppendLine($"Shares Outstanding:                 {SharesOutstanding}");
            sb.AppendLine($"Market Cap:                         {MarketCap}");
            sb.AppendLine($"Beta:                               {Beta:F2}");
            sb.AppendLine($"Avg 3 Month Volume:                 {Avg3MonthVolume}");
            sb.AppendLine($"EPS:                                {Eps:F2}");
            return sb.ToString();
        }
    }
}
