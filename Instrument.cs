using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using ttm.schwab.api;
using ttm.schwab.api.enumerations;

namespace ttm.schwab
{
    public partial class Instrument
    {
        public AssetType AssetType { get; private set; }
        public string Cusip { get; private set; }
        public string Symbol { get; private set; }

        public ContractType OptionContractType { get; private set; }
        public OptionType OptionType { get; private set; }
        public string OptionUnderlyingSymbol { get; private set; }

        public DateTime OptionExpirationDate { get; private set; }
        public double OptionStrikePrice { get; private set; }

        public double NetChange { get; private set; }

        private Instrument() { }

        internal static Instrument Copy(INSTRUMENT instrument)
        {
            if (instrument == null) return null;

            return new()
            {
                AssetType = TRANSLATE.Translate(instrument.AssetType),
                Cusip = instrument.Cusip,
                Symbol = instrument.Symbol,

                OptionContractType = TRANSLATE.Translate(instrument.OptionContractType),
                OptionType = TRANSLATE.Translate(instrument.OptionType),
                OptionUnderlyingSymbol = instrument.OptionUnderlyingSymbol,

                OptionExpirationDate =
                    instrument.AssetType == ASSET_TYPE.OPTION &&
                    DateTime.TryParseExact(instrument.Symbol.AsSpan(6, 6), "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date) ?
                    date : DateTime.MinValue,
                OptionStrikePrice =
                    instrument.AssetType == ASSET_TYPE.OPTION &&
                    int.TryParse(instrument.Symbol.AsSpan(13, 8), out int price) ?
                    (double)price / 1000 : 0,

                NetChange = instrument.NetChange,
            };
        }

        [GeneratedRegex(@"^([A-Z]+)$")]
        private static partial Regex RegexEquity();

        public static AssetType LookLike(string symbol, out OptionSign optionSign)
        {
            optionSign = null;

            if (string.IsNullOrWhiteSpace(symbol)) return AssetType.Unknown;
            symbol = symbol.Trim().ToUpper();
            if (symbol.Length <= 6 && RegexEquity().Match(symbol).Success) return AssetType.Equity;
            if (OptionSign.TryParse(symbol, out optionSign)) return AssetType.Option;

            return AssetType.Unknown;
        }

        public static AssetType LookLike(string symbol)
        {
            return LookLike(symbol, out _);
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("Instrument");
            sb.AppendLine();
            sb.AppendLine($"Asset Type:                {CONVERT.ToString(TRANSLATE.Translate(AssetType))}");
            sb.AppendLine($"Cusip:                     {Cusip}");
            sb.AppendLine($"Symbol:                    {Symbol}");
            sb.AppendLine();

            switch (AssetType)
            {
                case AssetType.Option:
                    sb.AppendLine($"Contract Type:             {CONVERT.ToString(TRANSLATE.Translate(OptionContractType))}");
                    sb.AppendLine($"Option Type:               {CONVERT.ToString(TRANSLATE.Translate(OptionType))}");
                    sb.AppendLine($"Underlying Symbol:         {OptionUnderlyingSymbol}");
                    sb.AppendLine();
                    sb.AppendLine($"Expiration Date:           {OptionExpirationDate:yyyy-MM-dd}");
                    sb.AppendLine($"Strike Price:              {OptionStrikePrice:F2}");
                    sb.AppendLine();
                    break;
            }

            sb.AppendLine($"Net Change:                {NetChange:F2}");
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
