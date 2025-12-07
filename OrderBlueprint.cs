using System;
using ttm.schwab.api;

namespace ttm.schwab
{
    public sealed class OrderBlueprint
    {
        public string Blueprint { get; private set; }

        private OrderBlueprint() { }

        public static OrderBlueprint CreateBuyMarketEquity(string symbol, int quantity)
        {
            return new OrderBlueprint { Blueprint = ORDER_BLUEPRINT.CreateBuyMarketEquity(symbol, quantity).Blueprint };
        }

        public static OrderBlueprint CreateSellMarketEquity(string symbol, int quantity)
        {
            return new OrderBlueprint { Blueprint = ORDER_BLUEPRINT.CreateSellMarketEquity(symbol, quantity).Blueprint };
        }

        public static OrderBlueprint CreateBuyLimitEquity(string symbol, int quantity, double price)
        {
            return new OrderBlueprint { Blueprint = ORDER_BLUEPRINT.CreateBuyLimitEquity(symbol, quantity, price).Blueprint };
        }

        public static OrderBlueprint CreateSellLimitEquity(string symbol, int quantity, double price)
        {
            return new OrderBlueprint { Blueprint = ORDER_BLUEPRINT.CreateSellLimitEquity(symbol, quantity, price).Blueprint };
        }

        public static OrderBlueprint CreateBuyToOpenLimitOption(string symbol, int quantity, double price)
        {
            return new OrderBlueprint { Blueprint = ORDER_BLUEPRINT.CreateBuyToOpenLimitOption(symbol, quantity, price).Blueprint };
        }

        public static OrderBlueprint CreateBuyToCloseLimitOption(string symbol, int quantity, double price)
        {
            return new OrderBlueprint { Blueprint = ORDER_BLUEPRINT.CreateBuyToCloseLimitOption(symbol, quantity, price).Blueprint };
        }

        public static OrderBlueprint CreateSellToOpenLimitOption(string symbol, int quantity, double price)
        {
            return new OrderBlueprint { Blueprint = ORDER_BLUEPRINT.CreateSellToOpenLimitOption(symbol, quantity, price).Blueprint };
        }

        public static OrderBlueprint CreateSellToCloseLimitOption(string symbol, int quantity, double price)
        {
            return new OrderBlueprint { Blueprint = ORDER_BLUEPRINT.CreateSellToCloseLimitOption(symbol, quantity, price).Blueprint };
        }

        public static OrderBlueprint FromOrder(Order order)
        {
            if (order.OrderType == OrderType.Market && order.AssetType == AssetType.Equity)
            {
                if (order.Instruction == OrderInstructionType.Buy) return CreateBuyMarketEquity(order.Symbol, Convert.ToInt32(order.Quantity));
                if (order.Instruction == OrderInstructionType.Sell) return CreateSellMarketEquity(order.Symbol, Convert.ToInt32(order.Quantity));
            }

            if (order.OrderType == OrderType.Limit)
            {
                if (order.AssetType == AssetType.Equity)
                {
                    if (order.Instruction == OrderInstructionType.Buy) return CreateBuyLimitEquity(order.Symbol, Convert.ToInt32(order.Quantity), order.Price);
                    if (order.Instruction == OrderInstructionType.Sell) return CreateSellLimitEquity(order.Symbol, Convert.ToInt32(order.Quantity), order.Price);
                }

                if (order.AssetType == AssetType.Option)
                {
                    if (order.Instruction == OrderInstructionType.BuyToOpen) return CreateBuyToOpenLimitOption(order.Symbol, Convert.ToInt32(order.Quantity), order.Price);
                    if (order.Instruction == OrderInstructionType.BuyToClose) return CreateBuyToCloseLimitOption(order.Symbol, Convert.ToInt32(order.Quantity), order.Price);
                    if (order.Instruction == OrderInstructionType.SellToOpen) return CreateSellToOpenLimitOption(order.Symbol, Convert.ToInt32(order.Quantity), order.Price);
                    if (order.Instruction == OrderInstructionType.SellToClose) return CreateSellToCloseLimitOption(order.Symbol, Convert.ToInt32(order.Quantity), order.Price);
                }
            }

            return null;
        }
    }
}
