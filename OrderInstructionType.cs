using ttm.schwab.api.enumerations;

namespace ttm.schwab
{
    public enum OrderInstructionType
    {
        Buy,
        Sell,
        BuyToCover,
        SellShort,
        BuyToOpen,
        BuyToClose,
        SellToOpen,
        SellToClose,
        Exchange,
        SellShortExempt,
        Unknown
    }

    internal static partial class TRANSLATE
    {
        public static OrderInstructionType Translate(ORDER_INSTRUCTION_TYPE orderInstructionType)
        {
            return orderInstructionType switch
            {
                ORDER_INSTRUCTION_TYPE.BUY => OrderInstructionType.Buy,
                ORDER_INSTRUCTION_TYPE.SELL => OrderInstructionType.Sell,
                ORDER_INSTRUCTION_TYPE.BUY_TO_COVER => OrderInstructionType.BuyToCover,
                ORDER_INSTRUCTION_TYPE.SELL_SHORT => OrderInstructionType.SellShort,
                ORDER_INSTRUCTION_TYPE.BUY_TO_OPEN => OrderInstructionType.BuyToOpen,
                ORDER_INSTRUCTION_TYPE.BUY_TO_CLOSE => OrderInstructionType.BuyToClose,
                ORDER_INSTRUCTION_TYPE.SELL_TO_OPEN => OrderInstructionType.SellToOpen,
                ORDER_INSTRUCTION_TYPE.SELL_TO_CLOSE => OrderInstructionType.SellToClose,
                ORDER_INSTRUCTION_TYPE.EXCHANGE => OrderInstructionType.Exchange,
                ORDER_INSTRUCTION_TYPE.SELL_SHORT_EXEMPT => OrderInstructionType.SellShortExempt,
                ORDER_INSTRUCTION_TYPE.UNKNOWN => OrderInstructionType.Unknown,
                _ => OrderInstructionType.Unknown,
            };
        }

        public static ORDER_INSTRUCTION_TYPE Translate(OrderInstructionType orderInstructionType)
        {
            return orderInstructionType switch
            {
                OrderInstructionType.Buy => ORDER_INSTRUCTION_TYPE.BUY,
                OrderInstructionType.Sell => ORDER_INSTRUCTION_TYPE.SELL,
                OrderInstructionType.BuyToCover => ORDER_INSTRUCTION_TYPE.BUY_TO_COVER,
                OrderInstructionType.SellShort => ORDER_INSTRUCTION_TYPE.SELL_SHORT,
                OrderInstructionType.BuyToOpen => ORDER_INSTRUCTION_TYPE.BUY_TO_OPEN,
                OrderInstructionType.BuyToClose => ORDER_INSTRUCTION_TYPE.BUY_TO_CLOSE,
                OrderInstructionType.SellToOpen => ORDER_INSTRUCTION_TYPE.SELL_TO_OPEN,
                OrderInstructionType.SellToClose => ORDER_INSTRUCTION_TYPE.SELL_TO_CLOSE,
                OrderInstructionType.Exchange => ORDER_INSTRUCTION_TYPE.EXCHANGE,
                OrderInstructionType.SellShortExempt => ORDER_INSTRUCTION_TYPE.SELL_SHORT_EXEMPT,
                OrderInstructionType.Unknown => ORDER_INSTRUCTION_TYPE.UNKNOWN,
                _ => ORDER_INSTRUCTION_TYPE.UNKNOWN,
            };
        }
    }
}
