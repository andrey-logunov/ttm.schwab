using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ttm.schwab.api
{
    internal sealed class OPTION_CHAIN
    {
        public string Symbol { get; private set; }
        public double UnderlyingPrice { get; private set; }
        public string Status { get; private set; }
        public double InterestRate { get; private set; }
        public double DividendYield { get; private set; }

        private OPTION[] _options;
        public IEnumerable<OPTION> Options { get { foreach (OPTION option in _options) yield return option; } }

        private OPTION_CHAIN() { }

        public static OPTION_CHAIN Parse(JToken jtoken)
        {
            if (jtoken == null) return null;

            OPTION_CHAIN optionChain = new()
            {
                Symbol = Convert.ToString(jtoken["symbol"]),
                UnderlyingPrice = Convert.ToDouble(jtoken["underlyingPrice"]),
                Status = Convert.ToString(jtoken["status"]),
                InterestRate = Convert.ToDouble(jtoken["interestRate"]),
                DividendYield = Convert.ToDouble(jtoken["dividendYield"])
            };

            List<OPTION> options = [];
            foreach (JProperty expDateToken in jtoken["callExpDateMap"].Cast<JProperty>())
            {
                foreach (JProperty strikePriceToken in expDateToken.Value.Cast<JProperty>())
                {
                    foreach (JToken optionToken in strikePriceToken.Value.Children())
                    {
                        OPTION option = OPTION.Parse(optionToken);
                        if (option != null && string.Equals(option.OptionSymbol?[..6].TrimEnd(), optionChain.Symbol, StringComparison.OrdinalIgnoreCase))
                        {
                            options.Add(option);
                        }
                    }
                }
            }

            foreach (JProperty expDateToken in jtoken["putExpDateMap"].Cast<JProperty>())
            {
                foreach (JProperty strikePriceToken in expDateToken.Value.Cast<JProperty>())
                {
                    foreach (JToken optionToken in strikePriceToken.Value.Children())
                    {
                        OPTION option = OPTION.Parse(optionToken);
                        if (option != null && string.Equals(option.OptionSymbol?[..6].TrimEnd(), optionChain.Symbol, StringComparison.OrdinalIgnoreCase))
                        {
                            options.Add(option);
                        }
                    }
                }
            }
            optionChain._options = [.. options];

            return optionChain;
        }

        public static OPTION_CHAIN Parse(string json)
        {
            return Parse(JToken.Parse(json));
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("OPTION CHAIN");
            sb.AppendLine();
            sb.AppendLine($"Symbol:                             {Symbol}");
            sb.AppendLine($"Underlying Price:                   {UnderlyingPrice:F2}");
            sb.AppendLine($"Status:                             {Status}");
            sb.AppendLine();
            foreach (OPTION option in Options.OrderBy(p => p.ContractType).ThenBy(p => p.ExpirationDate).ThenBy(p => p.StrikePrice))
            {
                sb.AppendLine($"{option}");
            }
            return sb.ToString();
        }
    }
}
