using System.Text;
using ttm.schwab.api;

namespace ttm.schwab
{
    public sealed class Position
    {
        public double LongQuantity { get; private set; }
        public double ShortQuantity { get; private set; }
        public double AveragePrice { get; private set; }
        public double MarketValue { get; private set; }

        public Instrument Instrument { get; private set; }

        private Position() { }

        internal static Position Copy(POSITION position)
        {
            return new Position
            {
                LongQuantity = position.LongQuantity,
                ShortQuantity = position.ShortQuantity,
                AveragePrice = position.AveragePrice,
                MarketValue = position.MarketValue,

                Instrument = Instrument.Copy(position.Instrument),
            };
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("Position");
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
