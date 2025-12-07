using System.Text;
using ttm.schwab.api;

namespace ttm.schwab
{
    public class Equity
    {
        public string Cusip { get; private set; }
        public string Symbol { get; private set; }
        public string Description { get; private set; }
        public string Exchange { get; private set; }

        private Equity() { }

        internal static Equity Copy(EQUITY equity)
        {
            if (equity == null) return null;

            return new Equity
            {
                Cusip = equity.Cusip,
                Symbol = equity.Symbol,
                Description = equity.Description,
                Exchange = equity.Exchange,
            };
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("Equity");
            sb.AppendLine();
            sb.AppendLine($"Cusip:                              {Cusip}");
            sb.AppendLine($"Symbol:                             {Symbol}");
            sb.AppendLine($"Description:                        {Description}");
            sb.AppendLine($"Exchange:                           {Exchange}");
            return sb.ToString();
        }
    }
}
