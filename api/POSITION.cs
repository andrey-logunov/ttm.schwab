using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Text;

namespace ttm.schwab.api
{
    internal sealed class POSITION
    {
        public double LongQuantity { get; private set; }
        public double ShortQuantity { get; private set; }
        public double AveragePrice { get; private set; }
        public double MarketValue { get; private set; }

        public INSTRUMENT Instrument { get; private set; }

        private POSITION() { }

        public static POSITION Parse(JToken jtoken)
        {
            if (jtoken == null) return null;
            if (jtoken["instrument"] == null) return null;

            return new POSITION
            {
                LongQuantity = Convert.ToDouble(jtoken["longQuantity"]),
                ShortQuantity = Convert.ToDouble(jtoken["shortQuantity"]),
                AveragePrice = Convert.ToDouble(jtoken["averagePrice"]),
                MarketValue = Convert.ToDouble(jtoken["marketValue"]),

                Instrument = INSTRUMENT.Parse(jtoken["instrument"]),
            };
        }

        public static POSITION Parse(string json)
        {
            return Parse(JToken.Parse(json));
        }

        public static POSITION[] ParseArray(JToken jtoken)
        {
            return jtoken.Children().Select(Parse).Where(p => p != null).ToArray();
        }

        public static POSITION[] ParseArray(string json)
        {
            return ParseArray(JToken.Parse(json));
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("POSITION");
            sb.AppendLine();
            sb.AppendLine($"Long Quantity:                      {LongQuantity:F2}");
            sb.AppendLine($"Short Quantity:                     {ShortQuantity:F2}");
            sb.AppendLine($"Average Price:                      {AveragePrice:F2}");
            sb.AppendLine($"Market Value:                       {MarketValue:F2}");
            sb.AppendLine();
            sb.AppendLine($"{Instrument}");
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
