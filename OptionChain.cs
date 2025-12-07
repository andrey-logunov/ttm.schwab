using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ttm.schwab.api;

namespace ttm.schwab
{
    public sealed class OptionChain
    {
        public string Symbol { get; private set; }
        public double UnderlyingPrice { get; private set; }
        public string Status { get; private set; }
        public double InterestRate { get; private set; }
        public double DividendYield { get; private set; }

        private Option[] _options;

        private OptionChain() { }

        internal static OptionChain Copy(OPTION_CHAIN optionChain)
        {
            return new OptionChain
            {
                Symbol = optionChain.Symbol,
                UnderlyingPrice = optionChain.UnderlyingPrice,
                Status = optionChain.Status,
                InterestRate = optionChain.InterestRate,
                DividendYield = optionChain.DividendYield,
                _options = optionChain.Options.Select(p => Option.Copy(optionChain.Symbol, optionChain.UnderlyingPrice, p)).ToArray(),
            };
        }

        public IEnumerable<DateOnly> GetExpirationDates(bool regularOnly = true)
        {
            return _options.Select(p => p.ExpirationDate).Where(p => !regularOnly || common.Calendar.IsRegularOptionDate(p)).Distinct().OrderBy(p => p);
        }

        public IEnumerable<double> GetStrikePrices(DateOnly expirationDate)
        {
            return _options.Where(p => p.ExpirationDate == expirationDate).Select(p => p.StrikePrice).Distinct().OrderBy(p => p);
        }

        public IEnumerable<Option> GetOptions(bool regularOnly = true)
        {
            return _options.Where(p => !regularOnly || p.IsRegularExpirationDate).OrderBy(p => p.ContractType).ThenBy(p => p.ExpirationDate).ThenBy(p => p.StrikePrice);
        }

        public IEnumerable<Option> GetOptions(ContractType contractType, bool regularOnly = true)
        {
            return _options.Where(p => (!regularOnly || p.IsRegularExpirationDate) && (p.ContractType == contractType)).OrderBy(p => p.ExpirationDate).ThenBy(p => p.StrikePrice);
        }

        public IEnumerable<Option> GetOptions(ContractType contractType, DateOnly expirationDate)
        {
            return _options.Where(p => p.ContractType == contractType && p.ExpirationDate == expirationDate).OrderBy(p => p.StrikePrice);
        }

        public Option GetOption(ContractType contractType, DateOnly expirationDate, double strikePrice)
        {
            return _options.FirstOrDefault(p => p.ContractType == contractType && p.ExpirationDate == expirationDate && Math.Abs(p.StrikePrice - strikePrice) < 0.001);
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("Option Chain");
            sb.AppendLine();
            sb.AppendLine($"Symbol:                             {Symbol}");
            sb.AppendLine($"Underlying Price:                   {UnderlyingPrice:F2}");
            sb.AppendLine($"Status:                             {Status}");
            sb.AppendLine($"Interest Rate:                      {InterestRate:F2}");
            sb.AppendLine($"Dividend Yield:                     {DividendYield:F2}");
            sb.AppendLine();
            foreach (Option option in GetOptions())
            {
                sb.AppendLine($"{option}");
            }
            return sb.ToString();
        }
    }
}
