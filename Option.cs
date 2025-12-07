using System;
using System.Text;
using ttm.schwab.api;
using ttm.schwab.api.enumerations;

namespace ttm.schwab
{
    public sealed class Option
    {
        public string RootSymbol { get; private set; }
        public double UnderlyingPrice { get; private set; }

        public ContractType ContractType { get; private set; }
        public double StrikePrice { get; private set; }
        public DateOnly ExpirationDate { get; private set; }
        public int DaysToExpiration { get; private set; }
        public string OptionSymbol { get; private set; }
        public string Description { get; private set; }

        public double BidPrice { get; private set; }
        public double BidSize { get; private set; }
        public double AskPrice { get; private set; }
        public double AskSize { get; private set; }
        public double LastPrice { get; private set; }
        public double LastSize { get; private set; }

        public double Open { get; private set; }
        public double High { get; private set; }
        public double Low { get; private set; }
        public double Close { get; private set; }

        public double NetChange { get; private set; }
        public double TotalVolume { get; private set; }
        public double OpenInterest { get; private set; }
        public double ImpliedVolatility { get; private set; }
        public double TimeValue { get; private set; }

        public double Delta { get; private set; }
        public double Gamma { get; private set; }
        public double Theta { get; private set; }
        public double Vega { get; private set; }
        public double Rho { get; private set; }

        public double Multiplier { get; private set; }

        public bool IsRegularExpirationDate { get { return common.Calendar.IsRegularOptionDate(ExpirationDate); } }

        public double AnnualPutPct { get; private set; }
        public double SPK2 { get; private set; }

        private Option() { }

        internal static Option Copy(string rootSymbol, double underlyingPrice, OPTION option)
        {
            Option copyOption = new()
            {
                RootSymbol = rootSymbol,
                UnderlyingPrice = underlyingPrice,

                ContractType = TRANSLATE.Translate(option.ContractType),
                StrikePrice = option.StrikePrice,
                ExpirationDate = option.ExpirationDate,
                DaysToExpiration = option.DaysToExpiration,
                OptionSymbol = option.OptionSymbol,
                Description = option.Description,

                BidPrice = option.BidPrice,
                BidSize = option.BidSize,
                AskPrice = option.AskPrice,
                AskSize = option.AskSize,
                LastPrice = option.LastPrice,
                LastSize = option.LastSize,

                Open = option.Open,
                High = option.High,
                Low = option.Low,
                Close = option.Close,

                NetChange = option.NetChange,
                TotalVolume = option.TotalVolume,
                OpenInterest = option.OpenInterest,
                ImpliedVolatility = option.ImpliedVolatility,
                TimeValue = option.TimeValue,

                Delta = option.Delta,
                Gamma = option.Gamma,
                Theta = option.Theta,
                Vega = option.Vega,
                Rho = option.Rho,

                Multiplier = option.Multiplier,
            };

            copyOption.AnnualPutPct =
                copyOption.ContractType == ContractType.Put && copyOption.StrikePrice > copyOption.BidPrice ?
                ((copyOption.BidPrice - Math.Max(0, copyOption.StrikePrice - copyOption.UnderlyingPrice)) / (copyOption.StrikePrice - copyOption.BidPrice)) * (366.0 / (1.0 + copyOption.DaysToExpiration)) * 100 : 0;
            copyOption.SPK2 =
                copyOption.ContractType == ContractType.Put && copyOption.StrikePrice > 0.0 ?
                (copyOption.UnderlyingPrice * copyOption.BidPrice / (copyOption.StrikePrice * copyOption.StrikePrice)) * Math.Sqrt(31.0 / (1 + copyOption.DaysToExpiration)) : 0;

            return copyOption;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("Option");
            sb.AppendLine();
            sb.AppendLine($"Root Symbol:                        {RootSymbol}");
            sb.AppendLine($"Underlying Price:                   {UnderlyingPrice:F2}");
            sb.AppendLine();
            sb.AppendLine($"ContractType:                       {CONVERT.ToString(TRANSLATE.Translate(ContractType))}");
            sb.AppendLine($"Strike Price:                       {StrikePrice:F2}");
            sb.AppendLine($"Expiration Date:                    {ExpirationDate:yyyy-MM-dd}");
            sb.AppendLine($"Days To Expiration:                 {DaysToExpiration}");
            sb.AppendLine($"Option Symbol:                      {OptionSymbol}");
            sb.AppendLine($"Description:                        {Description}");
            sb.AppendLine();
            sb.AppendLine($"Bid Price:                          {BidPrice:F2}");
            sb.AppendLine($"Bid Size:                           {BidSize:F0}");
            sb.AppendLine($"Ask Price:                          {AskPrice:F2}");
            sb.AppendLine($"Ask Size:                           {AskSize:F0}");
            sb.AppendLine($"Last Price:                         {LastPrice:F2}");
            sb.AppendLine($"Last Size:                          {LastSize:F0}");
            sb.AppendLine();
            sb.AppendLine($"Open:                               {Open:F2}");
            sb.AppendLine($"High:                               {High:F2}");
            sb.AppendLine($"Low:                                {Low:F2}");
            sb.AppendLine($"Close:                              {Close:F2}");
            sb.AppendLine();
            sb.AppendLine($"Net Change:                         {NetChange:F2}");
            sb.AppendLine($"Total Volume:                       {TotalVolume:F0}");
            sb.AppendLine($"Open Interest:                      {OpenInterest:F0}");
            sb.AppendLine($"Implied Volatility:                 {ImpliedVolatility:F2}");
            sb.AppendLine($"Time Value:                         {TimeValue:F2}");
            sb.AppendLine();
            sb.AppendLine($"Delta:                              {Delta:F2}");
            sb.AppendLine($"Gamma:                              {Gamma:F2}");
            sb.AppendLine($"Theta:                              {Theta:F2}");
            sb.AppendLine($"Vega:                               {Vega:F2}");
            sb.AppendLine($"Rho:                                {Rho:F2}");
            return sb.ToString();
        }
    }
}
