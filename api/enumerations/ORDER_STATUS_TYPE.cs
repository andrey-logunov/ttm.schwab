namespace ttm.schwab.api.enumerations
{
    internal enum ORDER_STATUS_TYPE
    {
        AWAITING_PARENT_ORDER,
        AWAITING_CONDITION,
        AWAITING_STOP_CONDITION,
        AWAITING_MANUAL_REVIEW,
        ACCEPTED,
        AWAITING_UR_OUT,
        PENDING_ACTIVATION,
        QUEUED,
        WORKING,
        REJECTED,
        PENDING_CANCEL,
        CANCELED,
        PENDING_REPLACE,
        REPLACED,
        FILLED,
        EXPIRED,
        NEW,
        AWAITING_RELEASE_TIME,
        PENDING_ACKNOWLEDGEMENT,
        PENDING_RECALL,
        UNKNOWN
    }

    internal static partial class CONVERT
    {
        public static string ToString(ORDER_STATUS_TYPE status)
        {
            return status switch
            {
                ORDER_STATUS_TYPE.AWAITING_PARENT_ORDER => "AWAITING_PARENT_ORDER",
                ORDER_STATUS_TYPE.AWAITING_CONDITION => "AWAITING_CONDITION",
                ORDER_STATUS_TYPE.AWAITING_STOP_CONDITION => "AWAITING_STOP_CONDITION",
                ORDER_STATUS_TYPE.AWAITING_MANUAL_REVIEW => "AWAITING_MANUAL_REVIEW",
                ORDER_STATUS_TYPE.ACCEPTED => "ACCEPTED",
                ORDER_STATUS_TYPE.AWAITING_UR_OUT => "AWAITING_UR_OUT",
                ORDER_STATUS_TYPE.PENDING_ACTIVATION => "PENDING_ACTIVATION",
                ORDER_STATUS_TYPE.QUEUED => "QUEUED",
                ORDER_STATUS_TYPE.WORKING => "WORKING",
                ORDER_STATUS_TYPE.REJECTED => "REJECTED",
                ORDER_STATUS_TYPE.PENDING_CANCEL => "PENDING_CANCEL",
                ORDER_STATUS_TYPE.CANCELED => "CANCELED",
                ORDER_STATUS_TYPE.PENDING_REPLACE => "PENDING_REPLACE",
                ORDER_STATUS_TYPE.REPLACED => "REPLACED",
                ORDER_STATUS_TYPE.FILLED => "FILLED",
                ORDER_STATUS_TYPE.EXPIRED => "EXPIRED",
                ORDER_STATUS_TYPE.NEW => "NEW",
                ORDER_STATUS_TYPE.AWAITING_RELEASE_TIME => "AWAITING_RELEASE_TIME",
                ORDER_STATUS_TYPE.PENDING_ACKNOWLEDGEMENT => "PENDING_ACKNOWLEDGEMENT",
                ORDER_STATUS_TYPE.PENDING_RECALL => "PENDING_RECALL",
                ORDER_STATUS_TYPE.UNKNOWN => "UNKNOWN",
                _ => null,
            };
        }

        public static bool FromString(string text, out ORDER_STATUS_TYPE status)
        {
            switch (text)
            {
                case "AWAITING_PARENT_ORDER":
                    status = ORDER_STATUS_TYPE.AWAITING_PARENT_ORDER;
                    return true;
                case "AWAITING_CONDITION":
                    status = ORDER_STATUS_TYPE.AWAITING_CONDITION;
                    return true;
                case "AWAITING_STOP_CONDITION":
                    status = ORDER_STATUS_TYPE.AWAITING_STOP_CONDITION;
                    return true;
                case "AWAITING_MANUAL_REVIEW":
                    status = ORDER_STATUS_TYPE.AWAITING_MANUAL_REVIEW;
                    return true;
                case "ACCEPTED":
                    status = ORDER_STATUS_TYPE.ACCEPTED;
                    return true;
                case "AWAITING_UR_OUT":
                    status = ORDER_STATUS_TYPE.AWAITING_UR_OUT;
                    return true;
                case "PENDING_ACTIVATION":
                    status = ORDER_STATUS_TYPE.PENDING_ACTIVATION;
                    return true;
                case "QUEUED":
                    status = ORDER_STATUS_TYPE.QUEUED;
                    return true;
                case "WORKING":
                    status = ORDER_STATUS_TYPE.WORKING;
                    return true;
                case "REJECTED":
                    status = ORDER_STATUS_TYPE.REJECTED;
                    return true;
                case "PENDING_CANCEL":
                    status = ORDER_STATUS_TYPE.PENDING_CANCEL;
                    return true;
                case "CANCELED":
                    status = ORDER_STATUS_TYPE.CANCELED;
                    return true;
                case "PENDING_REPLACE":
                    status = ORDER_STATUS_TYPE.PENDING_REPLACE;
                    return true;
                case "REPLACED":
                    status = ORDER_STATUS_TYPE.REPLACED;
                    return true;
                case "FILLED":
                    status = ORDER_STATUS_TYPE.FILLED;
                    return true;
                case "EXPIRED":
                    status = ORDER_STATUS_TYPE.EXPIRED;
                    return true;
                case "NEW":
                    status = ORDER_STATUS_TYPE.NEW;
                    return true;
                case "AWAITING_RELEASE_TIME":
                    status = ORDER_STATUS_TYPE.AWAITING_RELEASE_TIME;
                    return true;
                case "PENDING_ACKNOWLEDGEMENT":
                    status = ORDER_STATUS_TYPE.PENDING_ACKNOWLEDGEMENT;
                    return true;
                case "PENDING_RECALL":
                    status = ORDER_STATUS_TYPE.PENDING_RECALL;
                    return true;
                case "UNKNOWN":
                    status = ORDER_STATUS_TYPE.UNKNOWN;
                    return true;
                default:
                    status = ORDER_STATUS_TYPE.UNKNOWN;
                    return false;
            }
        }
    }
}
