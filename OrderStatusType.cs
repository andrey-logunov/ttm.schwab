using ttm.schwab.api.enumerations;

namespace ttm.schwab
{
    public enum OrderStatusType
    {
        AwaitingParentOrder,
        AwaitingCondition,
        AwaitingStopCondition,
        AwaitingManualReview,
        Accepted,
        AwaitingUrOut,
        PendingActivation,
        Queued,
        Working,
        Rejected,
        PendingCancel,
        Canceled,
        PendingReplace,
        Replaced,
        Filled,
        Expired,
        New,
        AwaitingReleaseTime,
        PendingAcknowledgement,
        PendingRecall,
        Unknown
    }

    public static partial class TEXT
    {
        public static string ToString(OrderStatusType orderStatusType)
        {
            return orderStatusType switch
            {
                OrderStatusType.AwaitingParentOrder => "Awaiting Parent Order",
                OrderStatusType.AwaitingCondition => "Awaiting Condition",
                OrderStatusType.AwaitingStopCondition => "Awaiting Stop Condition",
                OrderStatusType.AwaitingManualReview => "Awaiting Manual Review",
                OrderStatusType.Accepted => "Accepted",
                OrderStatusType.AwaitingUrOut => "Awaiting Ur Out",
                OrderStatusType.PendingActivation => "Pending Activation",
                OrderStatusType.Queued => "Queued",
                OrderStatusType.Working => "Working",
                OrderStatusType.Rejected => "Rejected",
                OrderStatusType.PendingCancel => "Pending Cancel",
                OrderStatusType.Canceled => "Canceled",
                OrderStatusType.PendingReplace => "Pending Replace",
                OrderStatusType.Replaced => "Replaced",
                OrderStatusType.Filled => "Filled",
                OrderStatusType.Expired => "Expired",
                OrderStatusType.New => "New",
                OrderStatusType.AwaitingReleaseTime => "Awaiting Release Time",
                OrderStatusType.PendingAcknowledgement => "Pending Acknowledgement",
                OrderStatusType.PendingRecall => "Pending Recall",
                OrderStatusType.Unknown => "Unknown",
                _ => "Unknown",
            };
        }
    }

    internal static partial class TRANSLATE
    {
        public static OrderStatusType Translate(ORDER_STATUS_TYPE orderStatusType)
        {
            return orderStatusType switch
            {
                ORDER_STATUS_TYPE.AWAITING_PARENT_ORDER => OrderStatusType.AwaitingParentOrder,
                ORDER_STATUS_TYPE.AWAITING_CONDITION => OrderStatusType.AwaitingCondition,
                ORDER_STATUS_TYPE.AWAITING_STOP_CONDITION => OrderStatusType.AwaitingStopCondition,
                ORDER_STATUS_TYPE.AWAITING_MANUAL_REVIEW => OrderStatusType.AwaitingManualReview,
                ORDER_STATUS_TYPE.ACCEPTED => OrderStatusType.Accepted,
                ORDER_STATUS_TYPE.AWAITING_UR_OUT => OrderStatusType.AwaitingUrOut,
                ORDER_STATUS_TYPE.PENDING_ACTIVATION => OrderStatusType.PendingActivation,
                ORDER_STATUS_TYPE.QUEUED => OrderStatusType.Queued,
                ORDER_STATUS_TYPE.WORKING => OrderStatusType.Working,
                ORDER_STATUS_TYPE.REJECTED => OrderStatusType.Rejected,
                ORDER_STATUS_TYPE.PENDING_CANCEL => OrderStatusType.PendingCancel,
                ORDER_STATUS_TYPE.CANCELED => OrderStatusType.Canceled,
                ORDER_STATUS_TYPE.PENDING_REPLACE => OrderStatusType.PendingReplace,
                ORDER_STATUS_TYPE.REPLACED => OrderStatusType.Replaced,
                ORDER_STATUS_TYPE.FILLED => OrderStatusType.Filled,
                ORDER_STATUS_TYPE.EXPIRED => OrderStatusType.Expired,
                ORDER_STATUS_TYPE.NEW => OrderStatusType.New,
                ORDER_STATUS_TYPE.AWAITING_RELEASE_TIME => OrderStatusType.AwaitingReleaseTime,
                ORDER_STATUS_TYPE.PENDING_ACKNOWLEDGEMENT => OrderStatusType.PendingAcknowledgement,
                ORDER_STATUS_TYPE.PENDING_RECALL => OrderStatusType.PendingRecall,
                ORDER_STATUS_TYPE.UNKNOWN => OrderStatusType.Unknown,
                _ => OrderStatusType.Unknown,
            };
        }

        public static ORDER_STATUS_TYPE Translate(OrderStatusType orderStatusType)
        {
            return orderStatusType switch
            {
                OrderStatusType.AwaitingParentOrder => ORDER_STATUS_TYPE.AWAITING_PARENT_ORDER,
                OrderStatusType.AwaitingCondition => ORDER_STATUS_TYPE.AWAITING_CONDITION,
                OrderStatusType.AwaitingStopCondition => ORDER_STATUS_TYPE.AWAITING_STOP_CONDITION,
                OrderStatusType.AwaitingManualReview => ORDER_STATUS_TYPE.AWAITING_MANUAL_REVIEW,
                OrderStatusType.Accepted => ORDER_STATUS_TYPE.ACCEPTED,
                OrderStatusType.AwaitingUrOut => ORDER_STATUS_TYPE.AWAITING_UR_OUT,
                OrderStatusType.PendingActivation => ORDER_STATUS_TYPE.PENDING_ACTIVATION,
                OrderStatusType.Queued => ORDER_STATUS_TYPE.QUEUED,
                OrderStatusType.Working => ORDER_STATUS_TYPE.WORKING,
                OrderStatusType.Rejected => ORDER_STATUS_TYPE.REJECTED,
                OrderStatusType.PendingCancel => ORDER_STATUS_TYPE.PENDING_CANCEL,
                OrderStatusType.Canceled => ORDER_STATUS_TYPE.CANCELED,
                OrderStatusType.PendingReplace => ORDER_STATUS_TYPE.PENDING_REPLACE,
                OrderStatusType.Replaced => ORDER_STATUS_TYPE.REPLACED,
                OrderStatusType.Filled => ORDER_STATUS_TYPE.FILLED,
                OrderStatusType.Expired => ORDER_STATUS_TYPE.EXPIRED,
                OrderStatusType.New => ORDER_STATUS_TYPE.NEW,
                OrderStatusType.AwaitingReleaseTime => ORDER_STATUS_TYPE.AWAITING_RELEASE_TIME,
                OrderStatusType.PendingAcknowledgement => ORDER_STATUS_TYPE.PENDING_ACKNOWLEDGEMENT,
                OrderStatusType.PendingRecall => ORDER_STATUS_TYPE.PENDING_RECALL,
                OrderStatusType.Unknown => ORDER_STATUS_TYPE.UNKNOWN,
                _ => ORDER_STATUS_TYPE.UNKNOWN,
            };
        }
    }
}
