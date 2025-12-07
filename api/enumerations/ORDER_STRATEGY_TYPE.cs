namespace ttm.schwab.api.enumerations
{
    internal enum ORDER_STRATEGY_TYPE
    {
        SINGLE,
        CANCEL,
        RECALL,
        PAIR,
        FLATTEN,
        TWO_DAY_SWAP,
        BLAST_ALL,
        OCO,
        TRIGGER,
        UNKNOWN
    }

    internal static partial class CONVERT
    {
        public static string ToString(ORDER_STRATEGY_TYPE orderStrategyType)
        {
            return orderStrategyType switch
            {
                ORDER_STRATEGY_TYPE.SINGLE => "SINGLE",
                ORDER_STRATEGY_TYPE.CANCEL => "CANCEL",
                ORDER_STRATEGY_TYPE.RECALL => "RECALL",
                ORDER_STRATEGY_TYPE.PAIR => "PAIR",
                ORDER_STRATEGY_TYPE.FLATTEN => "FLATTEN",
                ORDER_STRATEGY_TYPE.TWO_DAY_SWAP => "TWO_DAY_SWAP",
                ORDER_STRATEGY_TYPE.BLAST_ALL => "BLAST_ALL",
                ORDER_STRATEGY_TYPE.OCO => "OCO",
                ORDER_STRATEGY_TYPE.TRIGGER => "TRIGGER",
                ORDER_STRATEGY_TYPE.UNKNOWN => "UNKNOWN",
                _ => null,
            };
        }

        public static bool FromString(string text, out ORDER_STRATEGY_TYPE orderStrategyType)
        {
            switch (text)
            {
                case "SINGLE":
                    orderStrategyType = ORDER_STRATEGY_TYPE.SINGLE;
                    return true;
                case "CANCEL":
                    orderStrategyType = ORDER_STRATEGY_TYPE.CANCEL;
                    return true;
                case "RECALL":
                    orderStrategyType = ORDER_STRATEGY_TYPE.RECALL;
                    return true;
                case "PAIR":
                    orderStrategyType = ORDER_STRATEGY_TYPE.PAIR;
                    return true;
                case "FLATTEN":
                    orderStrategyType = ORDER_STRATEGY_TYPE.FLATTEN;
                    return true;
                case "TWO_DAY_SWAP":
                    orderStrategyType = ORDER_STRATEGY_TYPE.TWO_DAY_SWAP;
                    return true;
                case "BLAST_ALL":
                    orderStrategyType = ORDER_STRATEGY_TYPE.BLAST_ALL;
                    return true;
                case "OCO":
                    orderStrategyType = ORDER_STRATEGY_TYPE.OCO;
                    return true;
                case "TRIGGER":
                    orderStrategyType = ORDER_STRATEGY_TYPE.TRIGGER;
                    return true;
                case "UNKNOWN":
                    orderStrategyType = ORDER_STRATEGY_TYPE.UNKNOWN;
                    return true;
                default:
                    orderStrategyType = ORDER_STRATEGY_TYPE.UNKNOWN;
                    return false;
            }
        }
    }
}
