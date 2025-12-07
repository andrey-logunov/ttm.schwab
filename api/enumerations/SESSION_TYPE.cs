namespace ttm.schwab.api.enumerations
{
    internal enum SESSION_TYPE
    {
        NORMAL,
        AM,
        PM,
        SEAMLESS,
        UNKNOWN
    }

    internal static partial class CONVERT
    {
        public static string ToString(SESSION_TYPE session)
        {
            return session switch
            {
                SESSION_TYPE.NORMAL => "NORMAL",
                SESSION_TYPE.AM => "AM",
                SESSION_TYPE.PM => "PM",
                SESSION_TYPE.SEAMLESS => "SEAMLESS",
                SESSION_TYPE.UNKNOWN => "UNKNOWN",
                _ => null,
            };
        }

        public static bool FromString(string text, out SESSION_TYPE session)
        {
            switch (text)
            {
                case "NORMAL":
                    session = SESSION_TYPE.NORMAL;
                    return true;
                case "AM":
                    session = SESSION_TYPE.AM;
                    return true;
                case "PM":
                    session = SESSION_TYPE.PM;
                    return true;
                case "SEAMLESS":
                    session = SESSION_TYPE.SEAMLESS;
                    return true;
                case "UNKNOWN":
                    session = SESSION_TYPE.UNKNOWN;
                    return true;
                default:
                    session = SESSION_TYPE.UNKNOWN;
                    return false;
            }
        }
    }
}
