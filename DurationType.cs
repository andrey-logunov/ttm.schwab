using ttm.schwab.api.enumerations;

namespace ttm.schwab
{
    public enum DurationType
    {
        Day,
        GoodTillCancel,
        FillOrKill,
        ImmediateOrCancel,
        EndOfWeek,
        EndOfMonth,
        NextEndOfMonth,
        Unknown
    }

    public static partial class TEXT
    {
        public static string ToString(DurationType durationType)
        {
            return durationType switch
            {
                DurationType.Day => "Day",
                DurationType.GoodTillCancel => "GTC",
                DurationType.FillOrKill => "FOK",
                DurationType.ImmediateOrCancel => "IOC",
                DurationType.EndOfWeek => "EOW",
                DurationType.EndOfMonth => "EOM",
                DurationType.NextEndOfMonth => "NEM",
                DurationType.Unknown => "Unknown",
                _ => "Unknown",
            };
        }
    }

    internal static partial class TRANSLATE
    {
        public static DurationType Translate(DURATION_TYPE durationType)
        {
            return durationType switch
            {
                DURATION_TYPE.DAY => DurationType.Day,
                DURATION_TYPE.GOOD_TILL_CANCEL => DurationType.GoodTillCancel,
                DURATION_TYPE.FILL_OR_KILL => DurationType.FillOrKill,
                DURATION_TYPE.IMMEDIATE_OR_CANCEL => DurationType.ImmediateOrCancel,
                DURATION_TYPE.END_OF_WEEK => DurationType.EndOfWeek,
                DURATION_TYPE.END_OF_MONTH => DurationType.EndOfMonth,
                DURATION_TYPE.NEXT_END_OF_MONTH => DurationType.NextEndOfMonth,
                DURATION_TYPE.UNKNOWN => DurationType.Unknown,
                _ => DurationType.Unknown,
            };
        }

        public static DURATION_TYPE Translate(DurationType durationType)
        {
            return durationType switch
            {
                DurationType.Day => DURATION_TYPE.DAY,
                DurationType.GoodTillCancel => DURATION_TYPE.GOOD_TILL_CANCEL,
                DurationType.FillOrKill => DURATION_TYPE.FILL_OR_KILL,
                DurationType.ImmediateOrCancel => DURATION_TYPE.IMMEDIATE_OR_CANCEL,
                DurationType.EndOfWeek => DURATION_TYPE.END_OF_WEEK,
                DurationType.EndOfMonth => DURATION_TYPE.END_OF_MONTH,
                DurationType.NextEndOfMonth => DURATION_TYPE.NEXT_END_OF_MONTH,
                DurationType.Unknown => DURATION_TYPE.UNKNOWN,
                _ => DURATION_TYPE.UNKNOWN,
            };
        }
    }
}
