namespace ttm.schwab.api.enumerations
{
    internal enum CONTRACT_TYPE_PICK
    {
        PUT,
        CALL,
        ALL,
        UNKNOWN
    }

    internal static partial class CONVERT
    {
        public static string ToString(CONTRACT_TYPE_PICK contractTypePick)
        {
            return contractTypePick switch
            {
                CONTRACT_TYPE_PICK.PUT => "PUT",
                CONTRACT_TYPE_PICK.CALL => "CALL",
                CONTRACT_TYPE_PICK.ALL => "ALL",
                CONTRACT_TYPE_PICK.UNKNOWN => "UNKNOWN",
                _ => null,
            };
        }

        public static bool FromString(string text, out CONTRACT_TYPE_PICK contractTypePick)
        {
            switch (text)
            {
                case "PUT":
                    contractTypePick = CONTRACT_TYPE_PICK.PUT;
                    return true;
                case "CALL":
                    contractTypePick = CONTRACT_TYPE_PICK.CALL;
                    return true;
                case "ALL":
                    contractTypePick = CONTRACT_TYPE_PICK.ALL;
                    return true;
                case "UNKNOWN":
                    contractTypePick = CONTRACT_TYPE_PICK.UNKNOWN;
                    return true;
                default:
                    contractTypePick = CONTRACT_TYPE_PICK.UNKNOWN;
                    return false;
            }
        }
    }
}
