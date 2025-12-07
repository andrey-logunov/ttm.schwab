using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ttm.schwab.api
{
    internal sealed partial class EQUITY
    {
        public string Cusip { get; private set; }
        public string Symbol { get; private set; }
        public string Description { get; private set; }
        public string Exchange { get; private set; }

        [GeneratedRegex(@"^[a-zA-Z]+$", RegexOptions.Compiled)]
        private static partial Regex EquityRegex();

        public static EQUITY Parse(JToken jtoken)
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

            return new EQUITY
            {
                Cusip = cusip,
                Symbol = symbol.Trim().ToUpper(),
                Description = description,
                Exchange = exchange,
            };
        }

        public static EQUITY Parse(string json)
        {
            return Parse(JToken.Parse(json));
        }

        public static EQUITY[] ParseArray(JToken jtoken)
        {
            return jtoken != null ? jtoken.Children().Select(Parse).Where(p => p != null).ToArray() : [];
        }

        public static EQUITY[] ParseArray(string json)
        {
            return ParseArray(JToken.Parse(json));
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("EQUITY");
            sb.AppendLine();
            sb.AppendLine($"Cusip:                              {Cusip}");
            sb.AppendLine($"Symbol:                             {Symbol}");
            sb.AppendLine($"Description:                        {Description}");
            sb.AppendLine($"Exchange:                           {Exchange}");
            return sb.ToString();
        }
    }
}
