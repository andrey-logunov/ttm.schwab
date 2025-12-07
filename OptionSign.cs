using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ttm.schwab
{
    public sealed partial class OptionSign
    {
        public string RootSymbol { get; private set; }
        public DateOnly ExpirationDate { get; private set; }
        public ContractType ContractType { get; private set; }
        public double StrikePrice { get; private set; }

        public OptionSign() { }

        public OptionSign(string rootSymbol, DateOnly expirationDate, ContractType contractType, double strikePrice)
        {
            RootSymbol = rootSymbol;
            ExpirationDate = expirationDate;
            ContractType = contractType;
            StrikePrice = strikePrice;
        }

        public static OptionSign FromOption(Option option)
        {
            return new OptionSign(option.RootSymbol, option.ExpirationDate, option.ContractType, option.StrikePrice);
        }

        public static bool TryParseSchwab(string symbol, out OptionSign value)
        {
            value = new OptionSign();

            if (string.IsNullOrWhiteSpace(symbol) || symbol.Length != 6 + 6 + 1 + 8) return false;
            value.RootSymbol = symbol[0..6].Trim().ToUpper();
            if (!DateOnly.TryParseExact(symbol.AsSpan(6, 6), "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly date)) return false;
            value.ExpirationDate = date;
            switch (symbol[6 + 6])
            {
                case 'C':
                    value.ContractType = ContractType.Call;
                    break;
                case 'P':
                    value.ContractType = ContractType.Put;
                    break;
                default:
                    return false;
            }
            if (!double.TryParse($"{symbol.AsSpan(6 + 6 + 1, 5)}.{symbol.AsSpan(6 + 6 + 1 + 5, 3)}", out double price) || price < 0.001) return false;
            value.StrikePrice = price;

            return true;
        }

        [GeneratedRegex(@"^([a-zA-Z]+)_?([0-9]{6})(C|P)([0-9][0-9]*(\.[0-9]+)?)$")]
        private static partial Regex RegexNormal();

        public static bool TryParseNormal(string symbol, out OptionSign value)
        {
            value = new OptionSign();

            if (string.IsNullOrWhiteSpace(symbol)) return false;

            Match match = RegexNormal().Match(symbol);
            if (!match.Success) return false;

            value.RootSymbol = match.Groups[1].Value.ToUpper();
            if (!DateOnly.TryParseExact(match.Groups[2].Value, "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly date)) return false;
            value.ExpirationDate = date;
            switch (match.Groups[3].Value)
            {
                case "C":
                    value.ContractType = ContractType.Call;
                    break;
                case "P":
                    value.ContractType = ContractType.Put;
                    break;
                default:
                    return false;
            }
            if (!double.TryParse(match.Groups[4].Value, out double price) || price < 0.001) return false;
            value.StrikePrice = price;

            return true;
        }

        public static bool TryParse(string symbol, out OptionSign value)
        {
            return TryParseSchwab(symbol, out value) || TryParseNormal(symbol, out value);
        }

        public string ToSchwab()
        {
            return $"{RootSymbol.Trim().ToUpper(),-6}{ExpirationDate:yyMMdd}{(ContractType == ContractType.Call ? "C" : (ContractType == ContractType.Put ? "P" : "X"))}{($"{StrikePrice:00000.000}".Remove(5, 1))}";
        }

        public string ToNormal()
        {
            return $"{RootSymbol.Trim().ToUpper()}_{ExpirationDate:yyMMdd}{(ContractType == ContractType.Call ? "C" : (ContractType == ContractType.Put ? "P" : "X"))}{StrikePrice}";
        }

        public static string FromSchwabToNormal(string symbol)
        {
            if (TryParseSchwab(symbol, out OptionSign optionSign))
            {
                return optionSign.ToNormal();
            }
            return symbol;
        }

        public static string FromNormalToSchwab(string symbol)
        {
            if (TryParseNormal(symbol, out OptionSign optionSign))
            {
                return optionSign.ToSchwab();
            }
            return symbol;
        }
    }
}
