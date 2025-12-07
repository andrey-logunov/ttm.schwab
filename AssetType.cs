using ttm.schwab.api.enumerations;

namespace ttm.schwab
{
    public enum AssetType
    {
        Equity,
        MutualFund,
        Option,
        Future,
        Index,
        CashEquivalent,
        FixedIncome,
        Product,
        Currency,
        CollectiveInvestment,
        Unknown
    }

    internal static partial class TRANSLATE
    {
        public static AssetType Translate(ASSET_TYPE assetType)
        {
            return assetType switch
            {
                ASSET_TYPE.EQUITY => AssetType.Equity,
                ASSET_TYPE.MUTUAL_FUND => AssetType.MutualFund,
                ASSET_TYPE.OPTION => AssetType.Option,
                ASSET_TYPE.FUTURE => AssetType.Future,
                ASSET_TYPE.INDEX => AssetType.Index,
                ASSET_TYPE.CASH_EQUIVALENT => AssetType.CashEquivalent,
                ASSET_TYPE.FIXED_INCOME => AssetType.FixedIncome,
                ASSET_TYPE.PRODUCT => AssetType.Product,
                ASSET_TYPE.CURRENCY => AssetType.Currency,
                ASSET_TYPE.COLLECTIVE_INVESTMENT => AssetType.CollectiveInvestment,
                ASSET_TYPE.UNKNOWN => AssetType.Unknown,
                _ => AssetType.Unknown,
            };
        }

        public static ASSET_TYPE Translate(AssetType assetType)
        {
            return assetType switch
            {
                AssetType.Equity => ASSET_TYPE.EQUITY,
                AssetType.MutualFund => ASSET_TYPE.MUTUAL_FUND,
                AssetType.Option => ASSET_TYPE.OPTION,
                AssetType.Future => ASSET_TYPE.FUTURE,
                AssetType.Index => ASSET_TYPE.INDEX,
                AssetType.CashEquivalent => ASSET_TYPE.CASH_EQUIVALENT,
                AssetType.FixedIncome => ASSET_TYPE.FIXED_INCOME,
                AssetType.Product => ASSET_TYPE.PRODUCT,
                AssetType.Currency => ASSET_TYPE.CURRENCY,
                AssetType.CollectiveInvestment => ASSET_TYPE.COLLECTIVE_INVESTMENT,
                AssetType.Unknown => ASSET_TYPE.UNKNOWN,
                _ => ASSET_TYPE.UNKNOWN,
            };
        }
    }
}
