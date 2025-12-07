namespace ttm.schwab.api.enumerations
{
    internal enum CONTRACT_TYPE
    {
        PUT,
        CALL,
        UNKNOWN
    }

    internal static partial class CONVERT
    {
        public static string ToString(CONTRACT_TYPE contractType)
        {
            return contractType switch
            {
                CONTRACT_TYPE.PUT => "PUT",
                CONTRACT_TYPE.CALL => "CALL",
                CONTRACT_TYPE.UNKNOWN => "UNKNOWN",
                _ => null,
            };
        }

        public static bool FromString(string text, out CONTRACT_TYPE contractType)
        {
            switch (text)
            {
                case "PUT":
                    contractType = CONTRACT_TYPE.PUT;
                    return true;
                case "CALL":
                    contractType = CONTRACT_TYPE.CALL;
                    return true;
                case "UNKNOWN":
                    contractType = CONTRACT_TYPE.UNKNOWN;
                    return true;
                default:
                    contractType = CONTRACT_TYPE.UNKNOWN;
                    return false;
            }
        }
    }
}
