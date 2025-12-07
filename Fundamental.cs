using System.Text;
using ttm.schwab.api;

namespace ttm.schwab
{
    public class Fundamental
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

        private Fundamental() { }

        internal static Fundamental Copy(FUNDAMENTAL fundamental)
        {
            if (fundamental == null) return null;

            return new Fundamental
            {
                Cusip = fundamental.Cusip,
                Symbol = fundamental.Symbol,
                Description = fundamental.Description,
                Exchange = fundamental.Exchange,
                High52Wk = fundamental.High52Wk,
                Low52Wk = fundamental.Low52Wk,
                SharesOutstanding = fundamental.SharesOutstanding,
                MarketCap = fundamental.MarketCap,
                Beta = fundamental.Beta,
                Avg3MonthVolume = fundamental.Avg3MonthVolume,
                Eps = fundamental.Eps,
            };
        }

        internal static Fundamental Copy(Equity equity)
        {
            if (equity == null) return null;

            return new Fundamental
            {
                Cusip = equity.Cusip,
                Symbol = equity.Symbol,
                Description = equity.Description,
                Exchange = equity.Exchange,
                High52Wk = 0,
                Low52Wk = 0,
                SharesOutstanding = 0,
                MarketCap = 0,
                Beta = 0,
                Avg3MonthVolume = 0,
                Eps = 0,
            };
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("Fundamental");
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
