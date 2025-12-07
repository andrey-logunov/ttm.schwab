using System;
using ttm.schwab.api;

namespace ttm.schwab
{
    public sealed class Price
    {
        public DateTime Date { get; private set; }
        public double Open { get; private set; }
        public double High { get; private set; }
        public double Low { get; private set; }
        public double Close { get; private set; }
        public double Volume { get; private set; }

        private Price() { }

        internal static Price Copy(PRICE price)
        {
            return new Price
            {
                Date = price.Date,
                Open = price.Open,
                High = price.High,
                Low = price.Low,
                Close = price.Close,
                Volume = price.Volume,
            };
        }

        public override string ToString()
        {
            return $"[{Date:yyyy-MM-dd HH:mm:ss}],[{Open}],[{High}],[{Low}],[{Close}],[{Volume}]";
        }
    }
}
