namespace ttm.schwab.api.enumerations
{
    internal enum ORDER_TYPE
    {
        MARKET,
        LIMIT,
        STOP,
        STOP_LIMIT,
        TRAILING_STOP,
        CABINET,
        NON_MARKETABLE,
        MARKET_ON_CLOSE,
        EXERCISE,
        TRAILING_STOP_LIMIT,
        NET_DEBIT,
        NET_CREDIT,
        NET_ZERO,
        LIMIT_ON_CLOSE,
        UNKNOWN
    }

    internal static partial class CONVERT
    {
        public static string ToString(ORDER_TYPE orderType)
        {
            return orderType switch
            {
                ORDER_TYPE.MARKET => "MARKET",
                ORDER_TYPE.LIMIT => "LIMIT",
                ORDER_TYPE.STOP => "STOP",
                ORDER_TYPE.STOP_LIMIT => "STOP_LIMIT",
                ORDER_TYPE.TRAILING_STOP => "TRAILING_STOP",
                ORDER_TYPE.CABINET => "CABINET",
                ORDER_TYPE.NON_MARKETABLE => "NON_MARKETABLE",
                ORDER_TYPE.MARKET_ON_CLOSE => "MARKET_ON_CLOSE",
                ORDER_TYPE.EXERCISE => "EXERCISE",
                ORDER_TYPE.TRAILING_STOP_LIMIT => "TRAILING_STOP_LIMIT",
                ORDER_TYPE.NET_DEBIT => "NET_DEBIT",
                ORDER_TYPE.NET_CREDIT => "NET_CREDIT",
                ORDER_TYPE.NET_ZERO => "NET_ZERO",
                ORDER_TYPE.LIMIT_ON_CLOSE => "LIMIT_ON_CLOSE",
                ORDER_TYPE.UNKNOWN => "UNKNOWN",
                _ => null,
            };
        }

        public static bool FromString(string text, out ORDER_TYPE orderType)
        {
            switch (text)
            {
                case "MARKET":
                    orderType = ORDER_TYPE.MARKET;
                    return true;
                case "LIMIT":
                    orderType = ORDER_TYPE.LIMIT;
                    return true;
                case "STOP":
                    orderType = ORDER_TYPE.STOP;
                    return true;
                case "STOP_LIMIT":
                    orderType = ORDER_TYPE.STOP_LIMIT;
                    return true;
                case "TRAILING_STOP":
                    orderType = ORDER_TYPE.TRAILING_STOP;
                    return true;
                case "CABINET":
                    orderType = ORDER_TYPE.CABINET;
                    return true;
                case "NON_MARKETABLE":
                    orderType = ORDER_TYPE.NON_MARKETABLE;
                    return true;
                case "MARKET_ON_CLOSE":
                    orderType = ORDER_TYPE.MARKET_ON_CLOSE;
                    return true;
                case "EXERCISE":
                    orderType = ORDER_TYPE.EXERCISE;
                    return true;
                case "TRAILING_STOP_LIMIT":
                    orderType = ORDER_TYPE.TRAILING_STOP_LIMIT;
                    return true;
                case "NET_DEBIT":
                    orderType = ORDER_TYPE.NET_DEBIT;
                    return true;
                case "NET_CREDIT":
                    orderType = ORDER_TYPE.NET_CREDIT;
                    return true;
                case "NET_ZERO":
                    orderType = ORDER_TYPE.NET_ZERO;
                    return true;
                case "LIMIT_ON_CLOSE":
                    orderType = ORDER_TYPE.LIMIT_ON_CLOSE;
                    return true;
                case "UNKNOWN":
                    orderType = ORDER_TYPE.UNKNOWN;
                    return true;
                default:
                    orderType = ORDER_TYPE.UNKNOWN;
                    return false;
            }
        }
    }
}
