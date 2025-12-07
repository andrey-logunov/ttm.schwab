using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Text;
using ttm.schwab.api.enumerations;

namespace ttm.schwab.api
{
    internal sealed class ORDER
    {
        public SESSION_TYPE Session { get; private set; }
        public DURATION_TYPE Duration { get; private set; }
        public ORDER_TYPE OrderType { get; private set; }
        public DateTime CancelTime { get; private set; }
        public string ComplexOrderStrategy { get; private set; }
        public double Quantity { get; private set; }
        public double FilledQuantity { get; private set; }
        public double RemainingQuantity { get; private set; }
        public string RequestedDestination { get; private set; }
        public string DestinationLinkName { get; private set; }
        public DateTime ReleaseTime { get; private set; }
        public double StopPrice { get; private set; }
        public string StopPriceLinkBasis { get; private set; }
        public string StopPriceLinkType { get; private set; }
        public double StopPriceOffset { get; private set; }
        public string StopType { get; private set; }
        public string PriceLinkBasis { get; private set; }
        public string PriceLinkType { get; private set; }
        public double Price { get; private set; }
        public string TaxLotMethod { get; private set; }

        public string Symbol { get; private set; }
        public ASSET_TYPE AssetType { get; private set; }
        public ORDER_INSTRUCTION_TYPE Instruction { get; private set; }

        public int NumberOfOrderLegs { get; private set; }
        public string OrderLegCollectionJson { get; private set; }

        public SPECIAL_INSTRUCTION_TYPE SpecialInstruction { get; private set; }
        public ORDER_STRATEGY_TYPE OrderStrategyType { get; private set; }
        public long OrderID { get; private set; }
        public bool Cancelable { get; private set; }
        public bool Editable { get; private set; }
        public ORDER_STATUS_TYPE OrderStatus { get; private set; }
        public DateTime EnteredTime { get; private set; }
        public DateTime CloseTime { get; private set; }
        public string Tag { get; private set; }
        public long AccountNumber { get; private set; }

        public double ExecutionPrice { get; private set; }

        public string OrderActivityCollectionJson { get; private set; }
        public string ReplacingOrderCollectionJson { get; private set; }
        public string ChildOrderStrategiesJson { get; private set; }

        public string OrderStatusDescription { get; private set; }

        private ORDER() { }

        public static ORDER Parse(JToken jtoken)
        {
            if (jtoken == null) return null;

            return new ORDER
            {
                Session = CONVERT.FromString(Convert.ToString(jtoken["session"]), out SESSION_TYPE session) ? session : SESSION_TYPE.UNKNOWN,
                Duration = CONVERT.FromString(Convert.ToString(jtoken["duration"]), out DURATION_TYPE duration) ? duration : DURATION_TYPE.UNKNOWN,
                OrderType = CONVERT.FromString(Convert.ToString(jtoken["orderType"]), out ORDER_TYPE orderType) ? orderType : ORDER_TYPE.UNKNOWN,
                CancelTime = Convert.ToDateTime(jtoken["cancelTime"]),
                ComplexOrderStrategy = Convert.ToString(jtoken["complexOrderStrategyType"]),
                Quantity = Convert.ToDouble(jtoken["quantity"]),
                FilledQuantity = Convert.ToDouble(jtoken["filledQuantity"]),
                RemainingQuantity = Convert.ToDouble(jtoken["remainingQuantity"]),
                RequestedDestination = Convert.ToString(jtoken["requestedDestination"]),
                DestinationLinkName = Convert.ToString(jtoken["destinationLinkName"]),
                ReleaseTime = Convert.ToDateTime(jtoken["releaseTime"]),
                StopPrice = Convert.ToDouble(jtoken["stopPrice"]),
                StopPriceLinkBasis = Convert.ToString(jtoken["stopPriceLinkBasis"]),
                StopPriceLinkType = Convert.ToString(jtoken["stopPriceLinkType"]),
                StopPriceOffset = Convert.ToDouble(jtoken["stopPriceOffset"]),
                StopType = Convert.ToString(jtoken["stopType"]),
                PriceLinkBasis = Convert.ToString(jtoken["priceLinkBasis"]),
                PriceLinkType = Convert.ToString(jtoken["priceLinkType"]),
                Price = Convert.ToDouble(jtoken["price"]),
                TaxLotMethod = Convert.ToString(jtoken["taxLotMethod"]),

                Symbol = Convert.ToString(jtoken["orderLegCollection"][0]["instrument"]["symbol"]).ToUpper(),
                AssetType = CONVERT.FromString(Convert.ToString(jtoken["orderLegCollection"][0]["instrument"]["assetType"]), out ASSET_TYPE assetType) ? assetType : ASSET_TYPE.UNKNOWN,
                Instruction = CONVERT.FromString(Convert.ToString(jtoken["orderLegCollection"][0]["instruction"]), out ORDER_INSTRUCTION_TYPE instructionType) ? instructionType : ORDER_INSTRUCTION_TYPE.UNKNOWN,

                NumberOfOrderLegs = jtoken["orderLegCollection"]?.Children().Count() ?? 0,
                OrderLegCollectionJson = Convert.ToString(jtoken["orderLegCollection"]),

                SpecialInstruction = CONVERT.FromString(Convert.ToString(jtoken["specialInstruction"]), out SPECIAL_INSTRUCTION_TYPE specialInstruction) ? specialInstruction : SPECIAL_INSTRUCTION_TYPE.UNKNOWN,
                OrderStrategyType = CONVERT.FromString(Convert.ToString(jtoken["orderStrategyType"]), out ORDER_STRATEGY_TYPE orderStrategyType) ? orderStrategyType : ORDER_STRATEGY_TYPE.UNKNOWN,
                OrderID = Convert.ToInt64(jtoken["orderId"]),
                Cancelable = Convert.ToBoolean(jtoken["cancelable"]),
                Editable = Convert.ToBoolean(jtoken["editable"]),
                OrderStatus = CONVERT.FromString(Convert.ToString(jtoken["status"]), out ORDER_STATUS_TYPE status) ? status : ORDER_STATUS_TYPE.UNKNOWN,
                EnteredTime = Convert.ToDateTime(jtoken["enteredTime"]),
                CloseTime = Convert.ToDateTime(jtoken["closeTime"]),
                Tag = Convert.ToString(jtoken["tag"]),
                AccountNumber = Convert.ToInt64(jtoken["accountNumber"]),

                ExecutionPrice = Convert.ToDouble(jtoken["orderActivityCollection"]?[0]["executionLegs"]?[0]["price"]),

                OrderActivityCollectionJson = Convert.ToString(jtoken["orderActivityCollection"]),
                ReplacingOrderCollectionJson = Convert.ToString(jtoken["replacingOrderCollection"]),
                ChildOrderStrategiesJson = Convert.ToString(jtoken["childOrderStrategies"]),

                OrderStatusDescription = Convert.ToString(jtoken["statusDescription"]),
            };
        }

        public static ORDER Parse(string json)
        {
            return Parse(JToken.Parse(json));
        }

        public static ORDER[] ParseArray(JToken jtoken)
        {
            return jtoken.Children().Select(Parse).Where(p => p != null).ToArray();
        }

        public static ORDER[] ParseArray(string json)
        {
            return ParseArray(JToken.Parse(json));
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("ORDER");
            sb.AppendLine();
            sb.AppendLine($"Session:                            {CONVERT.ToString(Session)}");
            sb.AppendLine($"Duration:                           {CONVERT.ToString(Duration)}");
            sb.AppendLine($"Order Type:                         {CONVERT.ToString(OrderType)}");
            sb.AppendLine($"Cancel Time:                        {CancelTime:yyyy-MM-dd HH:mm}");
            sb.AppendLine($"Complex Order Strategy:             {ComplexOrderStrategy}");
            sb.AppendLine($"Quantity:                           {Quantity:F2}");
            sb.AppendLine($"FilledQuantity:                     {FilledQuantity:F2}");
            sb.AppendLine($"RemainingQuantity:                  {RemainingQuantity:F2}");
            sb.AppendLine($"Requested Destination:              {RequestedDestination}");
            sb.AppendLine($"Destination Link Name:              {DestinationLinkName}");
            sb.AppendLine($"Release Time:                       {ReleaseTime:yyyy-MM-dd HH:mm}");
            sb.AppendLine($"Stop Price:                         {StopPrice:F2}");
            sb.AppendLine($"Stop Price Link Basis:              {StopPriceLinkBasis}");
            sb.AppendLine($"Stop Price Link Type:               {StopPriceLinkType}");
            sb.AppendLine($"Stop Price Offset:                  {StopPriceOffset:F2}");
            sb.AppendLine($"Stop Type:                          {StopType}");
            sb.AppendLine($"Price Link Basis:                   {PriceLinkBasis}");
            sb.AppendLine($"Price Link Type:                    {PriceLinkType}");
            sb.AppendLine($"Price:                              {Price:F2}");
            sb.AppendLine($"Tax Lot Method:                     {TaxLotMethod}");
            sb.AppendLine();
            sb.AppendLine($"Symbol:                             {Symbol}");
            sb.AppendLine($"Asset Type:                         {CONVERT.ToString(AssetType)}");
            sb.AppendLine($"Instruction:                        {CONVERT.ToString(Instruction)}");
            sb.AppendLine();
            sb.AppendLine($"Number of Order Legs:               {NumberOfOrderLegs}");
            sb.AppendLine();
            sb.AppendLine($"Special Instruction:                {CONVERT.ToString(SpecialInstruction)}");
            sb.AppendLine($"Order Strategy Type:                {CONVERT.ToString(OrderStrategyType)}");
            sb.AppendLine($"Order ID:                           {OrderID}");
            sb.AppendLine($"Cancelable:                         {Cancelable}");
            sb.AppendLine($"Editable:                           {Editable}");
            sb.AppendLine($"Order Status:                       {CONVERT.ToString(OrderStatus)}");
            sb.AppendLine($"Entered Time:                       {EnteredTime:yyyy-MM-dd HH:mm}");
            sb.AppendLine($"Close Time:                         {CloseTime:yyyy-MM-dd HH:mm}");
            sb.AppendLine($"Tag:                                {Tag}");
            sb.AppendLine($"AccountNumber:                      {AccountNumber}");
            sb.AppendLine($"Order Status Description:           {OrderStatusDescription}");
            sb.AppendLine();
            sb.AppendLine($"Execution Price:                    {ExecutionPrice:F2}");
            return sb.ToString();
        }
    }
}
