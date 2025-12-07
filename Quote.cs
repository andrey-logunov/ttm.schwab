using System.Text;
using ttm.schwab.api;

namespace ttm.schwab
{
    public sealed class Quote
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

        private Quote() { }

        internal static Quote Copy(QUOTE quote)
        {
            return new Quote
            {
                Symbol = quote.Symbol,
                RealTime = quote.RealTime,

                High52Wk = quote.High52Wk,
                Low52Wk = quote.Low52Wk,
                AskPrice = quote.AskPrice,
                AskSize = quote.AskSize,
                BidPrice = quote.BidPrice,
                BidSize = quote.BidSize,
                LastPrice = quote.LastPrice,
                LastSize = quote.LastSize,

                ClosePrice = quote.ClosePrice,
                OpenPrice = quote.OpenPrice,
                HighPrice = quote.HighPrice,
                LowPrice = quote.LowPrice,
                MarkPrice = quote.MarkPrice,

                TotalVolume = quote.TotalVolume,
                SecurityStatus = quote.SecurityStatus,
            };
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("Quote");
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
