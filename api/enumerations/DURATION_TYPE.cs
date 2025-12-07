namespace ttm.schwab.api.enumerations
{
    internal enum DURATION_TYPE
    {
        DAY,
        GOOD_TILL_CANCEL,
        FILL_OR_KILL,
        IMMEDIATE_OR_CANCEL,
        END_OF_WEEK,
        END_OF_MONTH,
        NEXT_END_OF_MONTH,
        UNKNOWN
    }

    internal static partial class CONVERT
    {
        public static string ToString(DURATION_TYPE duration)
        {
            return duration switch
            {
                DURATION_TYPE.DAY => "DAY",
                DURATION_TYPE.GOOD_TILL_CANCEL => "GOOD_TILL_CANCEL",
                DURATION_TYPE.FILL_OR_KILL => "FILL_OR_KILL",
                DURATION_TYPE.IMMEDIATE_OR_CANCEL => "IMMEDIATE_OR_CANCEL",
                DURATION_TYPE.END_OF_WEEK => "END_OF_WEEK",
                DURATION_TYPE.END_OF_MONTH => "END_OF_MONTH",
                DURATION_TYPE.NEXT_END_OF_MONTH => "NEXT_END_OF_MONTH",
                DURATION_TYPE.UNKNOWN => "UNKNOWN",
                _ => null,
            };
        }

        public static bool FromString(string text, out DURATION_TYPE duration)
        {
            switch (text)
            {
                case "DAY":
                    duration = DURATION_TYPE.DAY;
                    return true;
                case "GOOD_TILL_CANCEL":
                    duration = DURATION_TYPE.GOOD_TILL_CANCEL;
                    return true;
                case "FILL_OR_KILL":
                    duration = DURATION_TYPE.FILL_OR_KILL;
                    return true;
                case "IMMEDIATE_OR_CANCEL":
                    duration = DURATION_TYPE.IMMEDIATE_OR_CANCEL;
                    return true;
                case "END_OF_WEEK":
                    duration = DURATION_TYPE.END_OF_WEEK;
                    return true;
                case "END_OF_MONTH":
                    duration = DURATION_TYPE.END_OF_MONTH;
                    return true;
                case "NEXT_END_OF_MONTH":
                    duration = DURATION_TYPE.NEXT_END_OF_MONTH;
                    return true;
                case "UNKNOWN":
                    duration = DURATION_TYPE.UNKNOWN;
                    return true;
                default:
                    duration = DURATION_TYPE.UNKNOWN;
                    return false;
            }
        }
    }
}
