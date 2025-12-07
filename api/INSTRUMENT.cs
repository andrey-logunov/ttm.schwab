using Newtonsoft.Json.Linq;
using System;
using System.Text;
using ttm.schwab.api.enumerations;

namespace ttm.schwab.api
{
    internal sealed class INSTRUMENT
    {
        public ASSET_TYPE AssetType { get; private set; }
        public string Cusip { get; private set; }
        public string Symbol { get; private set; }

        public CONTRACT_TYPE OptionContractType { get; private set; }
        public OPTION_TYPE OptionType { get; private set; }
        public string OptionUnderlyingSymbol { get; private set; }

        public double NetChange { get; private set; }

        private INSTRUMENT() { }

        public static INSTRUMENT Parse(JToken jtoken)
        {
            if (jtoken == null) return null;

            INSTRUMENT instrument = new()
            {
                AssetType = CONVERT.FromString(Convert.ToString(jtoken["assetType"]).ToUpper(), out ASSET_TYPE assetType) ? assetType : ASSET_TYPE.UNKNOWN,
                Cusip = Convert.ToString(jtoken["cusip"]),
                Symbol = Convert.ToString(jtoken["symbol"]).ToUpper(),
                NetChange = Convert.ToDouble(jtoken["netChange"])
            };

            switch (instrument.AssetType)
            {
                case ASSET_TYPE.OPTION:
                    instrument.OptionContractType = CONVERT.FromString(Convert.ToString(jtoken["putCall"]), out CONTRACT_TYPE contractType) ? contractType : CONTRACT_TYPE.UNKNOWN;
                    instrument.OptionType = CONVERT.FromString(Convert.ToString(jtoken["type"]), out OPTION_TYPE optionType) ? optionType : OPTION_TYPE.UNKNOWN;
                    instrument.OptionUnderlyingSymbol = Convert.ToString(jtoken["underlyingSymbol"]);
                    break;
            }

            return instrument;
        }

        public static INSTRUMENT Parse(string json)
        {
            return Parse(JToken.Parse(json));
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("INSTRUMENT");
            sb.AppendLine();
            sb.AppendLine($"Asset Type:                {CONVERT.ToString(AssetType)}");
            sb.AppendLine($"Cusip:                     {Cusip}");
            sb.AppendLine($"Symbol:                    {Symbol}");
            sb.AppendLine();

            switch (AssetType)
            {
                case ASSET_TYPE.OPTION:
                    sb.AppendLine($"Contract Type:             {CONVERT.ToString(OptionContractType)}");
                    sb.AppendLine($"Option Type:               {CONVERT.ToString(OptionType)}");
                    sb.AppendLine($"Underlying Symbol:         {OptionUnderlyingSymbol}");
                    sb.AppendLine();
                    break;
            }

            sb.AppendLine($"Net Change:                {NetChange:F2}");
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
