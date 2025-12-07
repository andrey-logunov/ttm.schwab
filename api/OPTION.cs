using Newtonsoft.Json.Linq;
using System;
using System.Text;
using ttm.schwab.api.enumerations;

namespace ttm.schwab.api
{
    internal sealed class OPTION
    {
        public CONTRACT_TYPE ContractType { get; private set; }
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

        private OPTION() { }

        public static OPTION Parse(JToken jtoken)
        {
            if (jtoken == null) return null;

            return new OPTION
            {
                ContractType = CONVERT.FromString(Convert.ToString(jtoken["putCall"]), out CONTRACT_TYPE contractType) ? contractType : CONTRACT_TYPE.UNKNOWN,
                StrikePrice = Convert.ToDouble(jtoken["strikePrice"]),
                ExpirationDate = DateOnly.FromDateTime(Convert.ToDateTime(jtoken["expirationDate"])),
                DaysToExpiration = Convert.ToInt32(jtoken["daysToExpiration"]),
                OptionSymbol = Convert.ToString(jtoken["symbol"]),
                Description = Convert.ToString(jtoken["description"]),

                BidPrice = Convert.ToDouble(jtoken["bid"]),
                BidSize = Convert.ToDouble(jtoken["bidSize"]),
                AskPrice = Convert.ToDouble(jtoken["ask"]),
                AskSize = Convert.ToDouble(jtoken["askSize"]),
                LastPrice = Convert.ToDouble(jtoken["last"]),
                LastSize = Convert.ToDouble(jtoken["lastSize"]),

                Open = Convert.ToDouble(jtoken["openPrice"]),
                High = Convert.ToDouble(jtoken["highPrice"]),
                Low = Convert.ToDouble(jtoken["lowPrice"]),
                Close = Convert.ToDouble(jtoken["closePrice"]),

                NetChange = Convert.ToDouble(jtoken["netChange"]),
                TotalVolume = Convert.ToDouble(jtoken["totalVolume"]),
                OpenInterest = Convert.ToDouble(jtoken["openInterest"]),
                ImpliedVolatility = Convert.ToDouble(jtoken["volatility"]),
                TimeValue = Convert.ToDouble(jtoken["timeValue"]),

                Delta = Convert.ToDouble(jtoken["delta"]),
                Gamma = Convert.ToDouble(jtoken["gamma"]),
                Theta = Convert.ToDouble(jtoken["theta"]),
                Vega = Convert.ToDouble(jtoken["vega"]),
                Rho = Convert.ToDouble(jtoken["rho"]),

                Multiplier = Convert.ToDouble(jtoken["multiplier"]),
            };
        }

        public static OPTION Parse(string json)
        {
            return Parse(JToken.Parse(json));
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("OPTION");
            sb.AppendLine();
            sb.AppendLine($"ContractType:                       {CONVERT.ToString(ContractType)}");
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
