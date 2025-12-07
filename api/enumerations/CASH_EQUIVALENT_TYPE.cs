namespace ttm.schwab.api.enumerations
{
    internal enum CASH_EQUIVALENT_TYPE
    {
        SWEEP_VEHICLE,
        SAVINGS,
        MONEY_MARKET_FUND,
        UNKNOWN
    }

    internal static partial class CONVERT
    {
        public static string ToString(CASH_EQUIVALENT_TYPE cashEquivalentType)
        {
            return cashEquivalentType switch
            {
                CASH_EQUIVALENT_TYPE.SWEEP_VEHICLE => "SWEEP_VEHICLE",
                CASH_EQUIVALENT_TYPE.SAVINGS => "SAVINGS",
                CASH_EQUIVALENT_TYPE.MONEY_MARKET_FUND => "MONEY_MARKET_FUND",
                CASH_EQUIVALENT_TYPE.UNKNOWN => "UNKNOWN",
                _ => null,
            };
        }

        public static bool FromString(string text, out CASH_EQUIVALENT_TYPE cashEquivalentType)
        {
            switch (text)
            {
                case "SWEEP_VEHICLE":
                    cashEquivalentType = CASH_EQUIVALENT_TYPE.SWEEP_VEHICLE;
                    return true;
                case "SAVINGS":
                    cashEquivalentType = CASH_EQUIVALENT_TYPE.SAVINGS;
                    return true;
                case "MONEY_MARKET_FUND":
                    cashEquivalentType = CASH_EQUIVALENT_TYPE.MONEY_MARKET_FUND;
                    return true;
                case "UNKNOWN":
                    cashEquivalentType = CASH_EQUIVALENT_TYPE.UNKNOWN;
                    return true;
                default:
                    cashEquivalentType = CASH_EQUIVALENT_TYPE.UNKNOWN;
                    return false;
            }
        }
    }
}
