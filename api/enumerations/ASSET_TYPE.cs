namespace ttm.schwab.api.enumerations
{
    internal enum ASSET_TYPE
    {
        EQUITY,
        MUTUAL_FUND,
        OPTION,
        FUTURE,
        FOREX,
        INDEX,
        CASH_EQUIVALENT,
        FIXED_INCOME,
        PRODUCT,
        CURRENCY,
        COLLECTIVE_INVESTMENT,
        UNKNOWN
    }

    internal static partial class CONVERT
    {
        public static string ToString(ASSET_TYPE assetType)
        {
            return assetType switch
            {
                ASSET_TYPE.EQUITY => "EQUITY",
                ASSET_TYPE.MUTUAL_FUND => "MUTUAL_FUND",
                ASSET_TYPE.OPTION => "OPTION",
                ASSET_TYPE.FUTURE => "FUTURE",
                ASSET_TYPE.FOREX => "FOREX",
                ASSET_TYPE.INDEX => "INDEX",
                ASSET_TYPE.CASH_EQUIVALENT => "CASH_EQUIVALENT",
                ASSET_TYPE.FIXED_INCOME => "FIXED_INCOME",
                ASSET_TYPE.PRODUCT => "PRODUCT",
                ASSET_TYPE.CURRENCY => "CURRENCY",
                ASSET_TYPE.COLLECTIVE_INVESTMENT => "COLLECTIVE_INVESTMENT",
                ASSET_TYPE.UNKNOWN => "UNKNOWN",
                _ => null,
            };
        }

        public static bool FromString(string text, out ASSET_TYPE assetType)
        {
            switch (text)
            {
                case "EQUITY":
                    assetType = ASSET_TYPE.EQUITY;
                    return true;
                case "MUTUAL_FUND":
                    assetType = ASSET_TYPE.MUTUAL_FUND;
                    return true;
                case "OPTION":
                    assetType = ASSET_TYPE.OPTION;
                    return true;
                case "FUTURE":
                    assetType = ASSET_TYPE.FUTURE;
                    return true;
                case "FOREX":
                    assetType = ASSET_TYPE.FOREX;
                    return true;
                case "INDEX":
                    assetType = ASSET_TYPE.INDEX;
                    return true;
                case "CASH_EQUIVALENT":
                    assetType = ASSET_TYPE.CASH_EQUIVALENT;
                    return true;
                case "FIXED_INCOME":
                    assetType = ASSET_TYPE.FIXED_INCOME;
                    return true;
                case "PRODUCT":
                    assetType = ASSET_TYPE.PRODUCT;
                    return true;
                case "CURRENCY":
                    assetType = ASSET_TYPE.CURRENCY;
                    return true;
                case "COLLECTIVE_INVESTMENT":
                    assetType = ASSET_TYPE.COLLECTIVE_INVESTMENT;
                    return true;
                case "UNKNOWN":
                    assetType = ASSET_TYPE.UNKNOWN;
                    return true;
                default:
                    assetType = ASSET_TYPE.UNKNOWN;
                    return false;
            }
        }
    }
}
