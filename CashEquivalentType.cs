using ttm.schwab.api.enumerations;

namespace ttm.schwab
{
    internal enum CashEquivalentType
    {
        SweepVehicle,
        Savings,
        MoneyMarketFund,
        Unknown
    }

    internal static partial class TRANSLATE
    {
        public static CashEquivalentType Translate(CASH_EQUIVALENT_TYPE cashEquivalentType)
        {
            return cashEquivalentType switch
            {
                CASH_EQUIVALENT_TYPE.SWEEP_VEHICLE => CashEquivalentType.SweepVehicle,
                CASH_EQUIVALENT_TYPE.SAVINGS => CashEquivalentType.Savings,
                CASH_EQUIVALENT_TYPE.MONEY_MARKET_FUND => CashEquivalentType.MoneyMarketFund,
                CASH_EQUIVALENT_TYPE.UNKNOWN => CashEquivalentType.Unknown,
                _ => CashEquivalentType.Unknown,
            };
        }

        public static CASH_EQUIVALENT_TYPE Translate(CashEquivalentType cashEquivalentType)
        {
            return cashEquivalentType switch
            {
                CashEquivalentType.SweepVehicle => CASH_EQUIVALENT_TYPE.SWEEP_VEHICLE,
                CashEquivalentType.Savings => CASH_EQUIVALENT_TYPE.SAVINGS,
                CashEquivalentType.MoneyMarketFund => CASH_EQUIVALENT_TYPE.MONEY_MARKET_FUND,
                CashEquivalentType.Unknown => CASH_EQUIVALENT_TYPE.UNKNOWN,
                _ => CASH_EQUIVALENT_TYPE.UNKNOWN,
            };
        }
    }
}
