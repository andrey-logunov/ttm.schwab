namespace ttm.schwab.api.enumerations
{
    internal enum ACCOUNT_TYPE
    {
        CASH,
        MARGIN,
        UNKNOWN
    }

    internal static partial class CONVERT
    {
        public static string ToString(ACCOUNT_TYPE accountType)
        {
            return accountType switch
            {
                ACCOUNT_TYPE.CASH => "CASH",
                ACCOUNT_TYPE.MARGIN => "MARGIN",
                ACCOUNT_TYPE.UNKNOWN => "UNKNOWN",
                _ => null,
            };
        }

        public static bool FromString(string text, out ACCOUNT_TYPE accountType)
        {
            switch (text)
            {
                case "CASH":
                    accountType = ACCOUNT_TYPE.CASH;
                    return true;
                case "MARGIN":
                    accountType = ACCOUNT_TYPE.MARGIN;
                    return true;
                case "UNKNOWN":
                    accountType = ACCOUNT_TYPE.UNKNOWN;
                    return true;
                default:
                    accountType = ACCOUNT_TYPE.UNKNOWN;
                    return false;
            }
        }
    }
}
