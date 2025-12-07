namespace ttm.schwab.api.enumerations
{
    internal enum OPTION_TYPE
    {
        VANILLA,
        BINARY,
        BARRIER,
        UNKNOWN
    }

    internal static partial class CONVERT
    {
        public static string ToString(OPTION_TYPE optionType)
        {
            return optionType switch
            {
                OPTION_TYPE.VANILLA => "VANILLA",
                OPTION_TYPE.BINARY => "BINARY",
                OPTION_TYPE.BARRIER => "BARRIER",
                OPTION_TYPE.UNKNOWN => "UNKNOWN",
                _ => null,
            };
        }

        public static bool FromString(string text, out OPTION_TYPE optionType)
        {
            switch (text)
            {
                case "VANILLA":
                    optionType = OPTION_TYPE.VANILLA;
                    return true;
                case "BINARY":
                    optionType = OPTION_TYPE.BINARY;
                    return true;
                case "BARRIER":
                    optionType = OPTION_TYPE.BARRIER;
                    return true;
                case "UNKNOWN":
                    optionType = OPTION_TYPE.UNKNOWN;
                    return true;
                default:
                    optionType = OPTION_TYPE.UNKNOWN;
                    return false;
            }
        }
    }
}
