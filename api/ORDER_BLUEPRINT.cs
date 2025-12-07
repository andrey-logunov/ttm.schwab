using Newtonsoft.Json.Linq;
using System;
using ttm.schwab.api.enumerations;

namespace ttm.schwab.api
{
    internal sealed class ORDER_BLUEPRINT(string blueprint)
    {
        public string Blueprint { get; private set; } = blueprint;

        private static ORDER_BLUEPRINT CreateMarket(string symbol, int quantity, ORDER_INSTRUCTION_TYPE instructionType, ASSET_TYPE assetType)
        {
            return new ORDER_BLUEPRINT(
                new JObject
                {
                    ["orderType"] = CONVERT.ToString(ORDER_TYPE.MARKET),
                    ["session"] = CONVERT.ToString(SESSION_TYPE.NORMAL),
                    ["duration"] = CONVERT.ToString(DURATION_TYPE.DAY),
                    ["orderStrategyType"] = CONVERT.ToString(ORDER_STRATEGY_TYPE.SINGLE),
                    ["orderLegCollection"] = new JArray
                    {
                        new JObject
                        {
                            ["instruction"] = CONVERT.ToString(instructionType),
                            ["quantity"] = quantity,
                            ["instrument"] = new JObject
                            {
                                ["symbol"] = symbol,
                                ["assetType"] = CONVERT.ToString(assetType)
                            }
                        }
                    }
                }.ToString()
            );
        }

        public static ORDER_BLUEPRINT CreateBuyMarketEquity(string symbol, int quantity)
        {
            return CreateMarket(symbol, quantity, ORDER_INSTRUCTION_TYPE.BUY, ASSET_TYPE.EQUITY);
        }

        public static ORDER_BLUEPRINT CreateSellMarketEquity(string symbol, int quantity)
        {
            return CreateMarket(symbol, quantity, ORDER_INSTRUCTION_TYPE.SELL, ASSET_TYPE.EQUITY);
        }

        private static ORDER_BLUEPRINT CreateLimit(string symbol, int quantity, double price, ORDER_INSTRUCTION_TYPE instructionType, ASSET_TYPE assetType)
        {
            return new ORDER_BLUEPRINT(
                new JObject
                {
                    ["complexOrderStrategyType"] = "NONE",
                    ["orderType"] = CONVERT.ToString(ORDER_TYPE.LIMIT),
                    ["session"] = assetType == ASSET_TYPE.EQUITY ? CONVERT.ToString(SESSION_TYPE.SEAMLESS) : CONVERT.ToString(SESSION_TYPE.NORMAL),
                    ["price"] = $"{price:F2}",
                    ["duration"] = CONVERT.ToString(DURATION_TYPE.GOOD_TILL_CANCEL),
                    ["orderStrategyType"] = CONVERT.ToString(ORDER_STRATEGY_TYPE.SINGLE),
                    ["orderLegCollection"] = new JArray
                    {
                        new JObject
                        {
                            ["instruction"] = CONVERT.ToString(instructionType),
                            ["quantity"] = quantity,
                            ["instrument"] = new JObject
                            {
                                ["symbol"] = symbol,
                                ["assetType"] = CONVERT.ToString(assetType)
                            }
                        }
                    }
                }.ToString()
            );
        }

        public static ORDER_BLUEPRINT CreateBuyLimitEquity(string symbol, int quantity, double price)
        {
            return CreateLimit(symbol, quantity, price, ORDER_INSTRUCTION_TYPE.BUY, ASSET_TYPE.EQUITY);
        }

        public static ORDER_BLUEPRINT CreateSellLimitEquity(string symbol, int quantity, double price)
        {
            return CreateLimit(symbol, quantity, price, ORDER_INSTRUCTION_TYPE.SELL, ASSET_TYPE.EQUITY);
        }

        public static ORDER_BLUEPRINT CreateBuyToOpenLimitOption(string symbol, int quantity, double price)
        {
            return CreateLimit(symbol, quantity, price, ORDER_INSTRUCTION_TYPE.BUY_TO_OPEN, ASSET_TYPE.OPTION);
        }

        public static ORDER_BLUEPRINT CreateBuyToCloseLimitOption(string symbol, int quantity, double price)
        {
            return CreateLimit(symbol, quantity, price, ORDER_INSTRUCTION_TYPE.BUY_TO_CLOSE, ASSET_TYPE.OPTION);
        }

        public static ORDER_BLUEPRINT CreateSellToOpenLimitOption(string symbol, int quantity, double price)
        {
            return CreateLimit(symbol, quantity, price, ORDER_INSTRUCTION_TYPE.SELL_TO_OPEN, ASSET_TYPE.OPTION);
        }

        public static ORDER_BLUEPRINT CreateSellToCloseLimitOption(string symbol, int quantity, double price)
        {
            return CreateLimit(symbol, quantity, price, ORDER_INSTRUCTION_TYPE.SELL_TO_CLOSE, ASSET_TYPE.OPTION);
        }

        public static ORDER_BLUEPRINT FromOrder(ORDER order)
        {
            if (order.OrderType == ORDER_TYPE.MARKET && order.AssetType == ASSET_TYPE.EQUITY)
            {
                if (order.Instruction == ORDER_INSTRUCTION_TYPE.BUY) return CreateBuyMarketEquity(order.Symbol, Convert.ToInt32(order.Quantity));
                if (order.Instruction == ORDER_INSTRUCTION_TYPE.SELL) return CreateSellMarketEquity(order.Symbol, Convert.ToInt32(order.Quantity));
            }

            if (order.OrderType == ORDER_TYPE.LIMIT)
            {
                if (order.AssetType == ASSET_TYPE.EQUITY)
                {
                    if (order.Instruction == ORDER_INSTRUCTION_TYPE.BUY) return CreateBuyLimitEquity(order.Symbol, Convert.ToInt32(order.Quantity), order.Price);
                    if (order.Instruction == ORDER_INSTRUCTION_TYPE.SELL) return CreateSellLimitEquity(order.Symbol, Convert.ToInt32(order.Quantity), order.Price);
                }

                if (order.AssetType == ASSET_TYPE.OPTION)
                {
                    if (order.Instruction == ORDER_INSTRUCTION_TYPE.BUY_TO_OPEN) return CreateBuyToOpenLimitOption(order.Symbol, Convert.ToInt32(order.Quantity), order.Price);
                    if (order.Instruction == ORDER_INSTRUCTION_TYPE.BUY_TO_CLOSE) return CreateBuyToCloseLimitOption(order.Symbol, Convert.ToInt32(order.Quantity), order.Price);
                    if (order.Instruction == ORDER_INSTRUCTION_TYPE.SELL_TO_OPEN) return CreateSellToOpenLimitOption(order.Symbol, Convert.ToInt32(order.Quantity), order.Price);
                    if (order.Instruction == ORDER_INSTRUCTION_TYPE.SELL_TO_CLOSE) return CreateSellToCloseLimitOption(order.Symbol, Convert.ToInt32(order.Quantity), order.Price);
                }
            }

            return null;
        }
    }
}
