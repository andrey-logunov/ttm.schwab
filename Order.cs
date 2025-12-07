using System;
using System.Text;
using ttm.schwab.api;
using ttm.schwab.api.enumerations;

namespace ttm.schwab
{
    public sealed class Order
    {
        public SessionType Session { get; private set; }
        public DurationType Duration { get; private set; }
        public OrderType OrderType { get; private set; }
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
        public AssetType AssetType { get; private set; }
        public OrderInstructionType Instruction { get; private set; }

        public int NumberOfOrderLegs { get; private set; }
        public string OrderLegCollectionJson { get; private set; }

        public SpecialInstructionType SpecialInstruction { get; private set; }
        public OrderStrategyType OrderStrategyType { get; private set; }
        public long OrderID { get; private set; }
        public bool Cancelable { get; private set; }
        public bool Editable { get; private set; }
        public OrderStatusType OrderStatus { get; private set; }
        public DateTime EnteredTime { get; private set; }
        public DateTime CloseTime { get; private set; }
        public string Tag { get; private set; }
        public long AccountNumber { get; private set; }

        public string OrderActivityCollectionJson { get; private set; }
        public string ReplacingOrderCollectionJson { get; private set; }
        public string ChildOrderStrategiesJson { get; private set; }

        public string OrderStatusDescription { get; private set; }

        public double ExecutionPrice { get; private set; }
        public DateTime ExecutionTime { get { return CloseTime; } }

        public bool IsExpiringSoon(out string expiringInfo) {
            expiringInfo = null;

            if (OrderStatus == OrderStatusType.Working)
            {
                if (CancelTime >= DateTime.Today)
                {
                    expiringInfo = $"{CancelTime:yyyy-MM-dd}";
                    return CancelTime.Subtract(DateTime.Today).TotalDays < 30;
                }
                else
                {
                    int days = (int)DateTime.Today.Subtract(EnteredTime).TotalDays;
                    expiringInfo = $"{days} days old ...";
                    return days > 100;
                }
            }
            return false;
        }

        private Order() { }

        internal static Order Copy(ORDER order)
        {
            return new Order
            {
                Session = TRANSLATE.Translate(order.Session),
                Duration = TRANSLATE.Translate(order.Duration),
                OrderType = TRANSLATE.Translate(order.OrderType),
                CancelTime = order.CancelTime,
                ComplexOrderStrategy = order.ComplexOrderStrategy,
                Quantity = order.Quantity,
                FilledQuantity = order.FilledQuantity,
                RemainingQuantity = order.RemainingQuantity,
                RequestedDestination = order.RequestedDestination,
                DestinationLinkName = order.DestinationLinkName,
                ReleaseTime = order.ReleaseTime,
                StopPrice = order.StopPrice,
                StopPriceLinkBasis = order.StopPriceLinkBasis,
                StopPriceLinkType = order.StopPriceLinkType,
                StopPriceOffset = order.StopPriceOffset,
                StopType = order.StopType,
                PriceLinkBasis = order.PriceLinkBasis,
                PriceLinkType = order.PriceLinkType,
                Price = order.Price,
                TaxLotMethod = order.TaxLotMethod,

                Symbol = order.Symbol,
                AssetType = TRANSLATE.Translate(order.AssetType),
                Instruction = TRANSLATE.Translate(order.Instruction),

                NumberOfOrderLegs = order.NumberOfOrderLegs,
                OrderLegCollectionJson = order.OrderLegCollectionJson,

                SpecialInstruction = TRANSLATE.Translate(order.SpecialInstruction),
                OrderStrategyType = TRANSLATE.Translate(order.OrderStrategyType),
                OrderID = order.OrderID,
                Cancelable = order.Cancelable,
                Editable = order.Editable,
                OrderStatus = TRANSLATE.Translate(order.OrderStatus),
                EnteredTime = order.EnteredTime,
                CloseTime = order.CloseTime,
                Tag = order.Tag,
                AccountNumber = order.AccountNumber,

                ExecutionPrice = order.ExecutionPrice,

                OrderActivityCollectionJson = order.OrderActivityCollectionJson,
                ReplacingOrderCollectionJson = order.ReplacingOrderCollectionJson,
                ChildOrderStrategiesJson = order.ChildOrderStrategiesJson,

                OrderStatusDescription = order.OrderStatusDescription,
            };
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("Order");
            sb.AppendLine();
            sb.AppendLine($"Session:                            {CONVERT.ToString(TRANSLATE.Translate(Session))}");
            sb.AppendLine($"Duration:                           {CONVERT.ToString(TRANSLATE.Translate(Duration))}");
            sb.AppendLine($"Order Type:                         {CONVERT.ToString(TRANSLATE.Translate(OrderType))}");
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
            sb.AppendLine($"Asset Type:                         {CONVERT.ToString(TRANSLATE.Translate(AssetType))}");
            sb.AppendLine($"Instruction:                        {CONVERT.ToString(TRANSLATE.Translate(Instruction))}");
            sb.AppendLine();
            sb.AppendLine($"Number of Order Legs:               {NumberOfOrderLegs}");
            sb.AppendLine();
            sb.AppendLine($"Special Instruction:                {CONVERT.ToString(TRANSLATE.Translate(SpecialInstruction))}");
            sb.AppendLine($"Order Strategy Type:                {CONVERT.ToString(TRANSLATE.Translate(OrderStrategyType))}");
            sb.AppendLine($"Order ID:                           {OrderID}");
            sb.AppendLine($"Cancelable:                         {Cancelable}");
            sb.AppendLine($"Editable:                           {Editable}");
            sb.AppendLine($"Order Status:                       {CONVERT.ToString(TRANSLATE.Translate(OrderStatus))}");
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
