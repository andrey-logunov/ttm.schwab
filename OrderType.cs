using ttm.schwab.api.enumerations;

namespace ttm.schwab
{
    public enum OrderType
    {
        Market,
        Limit,
        Stop,
        StopLimit,
        TrailingStop,
        Cabinet,
        NonMarketable,
        MarketOnClose,
        Exercise,
        TrailingStopLimit,
        NetDebit,
        NetCredit,
        NetZero,
        LimitOnClose,
        Unknown
    }

    public static partial class TEXT
    {
        public static string ToString(OrderType orderType)
        {
            return orderType switch
            {
                OrderType.Market => "Market",
                OrderType.Limit => "Limit",
                OrderType.Stop => "Stop",
                OrderType.StopLimit => "Stop Limit",
                OrderType.TrailingStop => "Trailing Stop",
                OrderType.Cabinet => "Cabinet",
                OrderType.NonMarketable => "Non Marketable",
                OrderType.MarketOnClose => "Market On Close",
                OrderType.Exercise => "Exercise",
                OrderType.TrailingStopLimit => "Trailing Stop Limit",
                OrderType.NetDebit => "Net Debit",
                OrderType.NetCredit => "Net Credit",
                OrderType.NetZero => "Net Zero",
                OrderType.LimitOnClose => "Limit On Close",
                OrderType.Unknown => "Unknown",
                _ => "Unknown",
            };
        }
    }

    internal static partial class TRANSLATE
    {
        public static OrderType Translate(ORDER_TYPE orderType)
        {
            return orderType switch
            {
                ORDER_TYPE.MARKET => OrderType.Market,
                ORDER_TYPE.LIMIT => OrderType.Limit,
                ORDER_TYPE.STOP => OrderType.Stop,
                ORDER_TYPE.STOP_LIMIT => OrderType.StopLimit,
                ORDER_TYPE.TRAILING_STOP => OrderType.TrailingStop,
                ORDER_TYPE.CABINET => OrderType.Cabinet,
                ORDER_TYPE.NON_MARKETABLE => OrderType.NonMarketable,
                ORDER_TYPE.MARKET_ON_CLOSE => OrderType.MarketOnClose,
                ORDER_TYPE.EXERCISE => OrderType.Exercise,
                ORDER_TYPE.TRAILING_STOP_LIMIT => OrderType.TrailingStopLimit,
                ORDER_TYPE.NET_DEBIT => OrderType.NetDebit,
                ORDER_TYPE.NET_CREDIT => OrderType.NetCredit,
                ORDER_TYPE.NET_ZERO => OrderType.NetZero,
                ORDER_TYPE.LIMIT_ON_CLOSE => OrderType.LimitOnClose,
                ORDER_TYPE.UNKNOWN => OrderType.Unknown,
                _ => OrderType.Unknown,
            };
        }

        public static ORDER_TYPE Translate(OrderType orderType)
        {
            return orderType switch
            {
                OrderType.Market => ORDER_TYPE.MARKET,
                OrderType.Limit => ORDER_TYPE.LIMIT,
                OrderType.Stop => ORDER_TYPE.STOP,
                OrderType.StopLimit => ORDER_TYPE.STOP_LIMIT,
                OrderType.TrailingStop => ORDER_TYPE.TRAILING_STOP,
                OrderType.Cabinet => ORDER_TYPE.CABINET,
                OrderType.NonMarketable => ORDER_TYPE.NON_MARKETABLE,
                OrderType.MarketOnClose => ORDER_TYPE.MARKET_ON_CLOSE,
                OrderType.Exercise => ORDER_TYPE.EXERCISE,
                OrderType.TrailingStopLimit => ORDER_TYPE.TRAILING_STOP_LIMIT,
                OrderType.NetDebit => ORDER_TYPE.NET_DEBIT,
                OrderType.NetCredit => ORDER_TYPE.NET_CREDIT,
                OrderType.NetZero => ORDER_TYPE.NET_ZERO,
                OrderType.LimitOnClose => ORDER_TYPE.LIMIT_ON_CLOSE,
                OrderType.Unknown => ORDER_TYPE.UNKNOWN,
                _ => ORDER_TYPE.UNKNOWN,
            };
        }
    }
}
