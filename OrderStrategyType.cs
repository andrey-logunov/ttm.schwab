using ttm.schwab.api.enumerations;

namespace ttm.schwab
{
    public enum OrderStrategyType
    {
        Single,
        Cancel,
        Recall,
        Pair,
        Flatten,
        TwoDaySwap,
        BlastAll,
        Oco,
        Trigger,
        Unknown
    }

    internal static partial class TRANSLATE
    {
        public static OrderStrategyType Translate(ORDER_STRATEGY_TYPE orderStrategyType)
        {
            return orderStrategyType switch
            {
                ORDER_STRATEGY_TYPE.SINGLE => OrderStrategyType.Single,
                ORDER_STRATEGY_TYPE.CANCEL => OrderStrategyType.Cancel,
                ORDER_STRATEGY_TYPE.RECALL => OrderStrategyType.Recall,
                ORDER_STRATEGY_TYPE.PAIR => OrderStrategyType.Pair,
                ORDER_STRATEGY_TYPE.FLATTEN => OrderStrategyType.Flatten,
                ORDER_STRATEGY_TYPE.TWO_DAY_SWAP => OrderStrategyType.TwoDaySwap,
                ORDER_STRATEGY_TYPE.BLAST_ALL => OrderStrategyType.BlastAll,
                ORDER_STRATEGY_TYPE.OCO => OrderStrategyType.Oco,
                ORDER_STRATEGY_TYPE.TRIGGER => OrderStrategyType.Trigger,
                ORDER_STRATEGY_TYPE.UNKNOWN => OrderStrategyType.Unknown,
                _ => OrderStrategyType.Single,
            };
        }

        public static ORDER_STRATEGY_TYPE Translate(OrderStrategyType orderStrategyType)
        {
            return orderStrategyType switch
            {
                OrderStrategyType.Single => ORDER_STRATEGY_TYPE.SINGLE,
                OrderStrategyType.Cancel => ORDER_STRATEGY_TYPE.CANCEL,
                OrderStrategyType.Recall => ORDER_STRATEGY_TYPE.RECALL,
                OrderStrategyType.Pair => ORDER_STRATEGY_TYPE.PAIR,
                OrderStrategyType.Flatten => ORDER_STRATEGY_TYPE.FLATTEN,
                OrderStrategyType.TwoDaySwap => ORDER_STRATEGY_TYPE.TWO_DAY_SWAP,
                OrderStrategyType.BlastAll => ORDER_STRATEGY_TYPE.BLAST_ALL,
                OrderStrategyType.Oco => ORDER_STRATEGY_TYPE.OCO,
                OrderStrategyType.Trigger => ORDER_STRATEGY_TYPE.TRIGGER,
                OrderStrategyType.Unknown => ORDER_STRATEGY_TYPE.UNKNOWN,
                _ => ORDER_STRATEGY_TYPE.UNKNOWN,
            };
        }
    }
}
