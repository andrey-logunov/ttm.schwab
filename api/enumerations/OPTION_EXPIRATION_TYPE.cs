namespace ttm.schwab.api.enumerations
{
    internal enum OPTION_EXPIRATION_TYPE
    {
        REGULAR,
        WEEKLY,
        END_OF_MONTH,
        QUARTERLY,
        UNKNOWN
    }

    internal static partial class CONVERT
    {
        public static string ToString(OPTION_EXPIRATION_TYPE optionExpirationType)
        {
            return optionExpirationType switch
            {
                OPTION_EXPIRATION_TYPE.REGULAR => "S",
                OPTION_EXPIRATION_TYPE.WEEKLY => "W",
                OPTION_EXPIRATION_TYPE.END_OF_MONTH => "M",
                OPTION_EXPIRATION_TYPE.QUARTERLY => "Q",
                OPTION_EXPIRATION_TYPE.UNKNOWN => "UNKNOWN",
                _ => null,
            };
        }

        public static bool FromString(string text, out OPTION_EXPIRATION_TYPE optionExpirationType)
        {
            switch (text)
            {
                case "S":
                    optionExpirationType = OPTION_EXPIRATION_TYPE.REGULAR;
                    return true;
                case "W":
                    optionExpirationType = OPTION_EXPIRATION_TYPE.WEEKLY;
                    return true;
                case "M":
                    optionExpirationType = OPTION_EXPIRATION_TYPE.END_OF_MONTH;
                    return true;
                case "Q":
                    optionExpirationType = OPTION_EXPIRATION_TYPE.QUARTERLY;
                    return true;
                case "UNKNOWN":
                    optionExpirationType = OPTION_EXPIRATION_TYPE.UNKNOWN;
                    return true;
                default:
                    optionExpirationType = OPTION_EXPIRATION_TYPE.UNKNOWN;
                    return false;
            }
        }
    }
}
