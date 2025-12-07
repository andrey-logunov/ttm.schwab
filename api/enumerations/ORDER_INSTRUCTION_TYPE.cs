namespace ttm.schwab.api.enumerations
{
    internal enum ORDER_INSTRUCTION_TYPE
    {
        BUY,
        SELL,
        BUY_TO_COVER,
        SELL_SHORT,
        BUY_TO_OPEN,
        BUY_TO_CLOSE,
        SELL_TO_OPEN,
        SELL_TO_CLOSE,
        EXCHANGE,
        SELL_SHORT_EXEMPT,
        UNKNOWN
    }

    internal static partial class CONVERT
    {
        public static string ToString(ORDER_INSTRUCTION_TYPE orderInstructionType)
        {
            return orderInstructionType switch
            {
                ORDER_INSTRUCTION_TYPE.BUY => "BUY",
                ORDER_INSTRUCTION_TYPE.SELL => "SELL",
                ORDER_INSTRUCTION_TYPE.BUY_TO_COVER => "BUY_TO_COVER",
                ORDER_INSTRUCTION_TYPE.SELL_SHORT => "SELL_SHORT",
                ORDER_INSTRUCTION_TYPE.BUY_TO_OPEN => "BUY_TO_OPEN",
                ORDER_INSTRUCTION_TYPE.BUY_TO_CLOSE => "BUY_TO_CLOSE",
                ORDER_INSTRUCTION_TYPE.SELL_TO_OPEN => "SELL_TO_OPEN",
                ORDER_INSTRUCTION_TYPE.SELL_TO_CLOSE => "SELL_TO_CLOSE",
                ORDER_INSTRUCTION_TYPE.EXCHANGE => "EXCHANGE",
                ORDER_INSTRUCTION_TYPE.SELL_SHORT_EXEMPT => "SELL_SHORT_EXEMPT",
                ORDER_INSTRUCTION_TYPE.UNKNOWN => "UNKNOWN",
                _ => null,
            };
        }

        public static bool FromString(string text, out ORDER_INSTRUCTION_TYPE orderInstructionType)
        {
            switch (text)
            {
                case "BUY":
                    orderInstructionType = ORDER_INSTRUCTION_TYPE.BUY;
                    return true;
                case "SELL":
                    orderInstructionType = ORDER_INSTRUCTION_TYPE.SELL;
                    return true;
                case "BUY_TO_COVER":
                    orderInstructionType = ORDER_INSTRUCTION_TYPE.BUY_TO_COVER;
                    return true;
                case "SELL_SHORT":
                    orderInstructionType = ORDER_INSTRUCTION_TYPE.SELL_SHORT;
                    return true;
                case "BUY_TO_OPEN":
                    orderInstructionType = ORDER_INSTRUCTION_TYPE.BUY_TO_OPEN;
                    return true;
                case "BUY_TO_CLOSE":
                    orderInstructionType = ORDER_INSTRUCTION_TYPE.BUY_TO_CLOSE;
                    return true;
                case "SELL_TO_OPEN":
                    orderInstructionType = ORDER_INSTRUCTION_TYPE.SELL_TO_OPEN;
                    return true;
                case "SELL_TO_CLOSE":
                    orderInstructionType = ORDER_INSTRUCTION_TYPE.SELL_TO_CLOSE;
                    return true;
                case "EXCHANGE":
                    orderInstructionType = ORDER_INSTRUCTION_TYPE.EXCHANGE;
                    return true;
                case "SELL_SHORT_EXEMPT":
                    orderInstructionType = ORDER_INSTRUCTION_TYPE.SELL_SHORT_EXEMPT;
                    return true;
                case "UNKNOWN":
                    orderInstructionType = ORDER_INSTRUCTION_TYPE.UNKNOWN;
                    return true;
                default:
                    orderInstructionType = ORDER_INSTRUCTION_TYPE.UNKNOWN;
                    return false;
            }
        }
    }
}
