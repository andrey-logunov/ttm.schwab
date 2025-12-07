using ttm.schwab.api.enumerations;

namespace ttm.schwab
{
    public enum SessionType
    {
        Normal,
        Am,
        Pm,
        Seamless,
        Unknown
    }

    internal static partial class TRANSLATE
    {
        public static SessionType Translate(SESSION_TYPE sessionType)
        {
            return sessionType switch
            {
                SESSION_TYPE.NORMAL => SessionType.Normal,
                SESSION_TYPE.AM => SessionType.Am,
                SESSION_TYPE.PM => SessionType.Pm,
                SESSION_TYPE.SEAMLESS => SessionType.Seamless,
                SESSION_TYPE.UNKNOWN => SessionType.Unknown,
                _ => SessionType.Unknown,
            };
        }

        public static SESSION_TYPE Translate(SessionType sessionType)
        {
            return sessionType switch
            {
                SessionType.Normal => SESSION_TYPE.NORMAL,
                SessionType.Am => SESSION_TYPE.AM,
                SessionType.Pm => SESSION_TYPE.PM,
                SessionType.Seamless => SESSION_TYPE.SEAMLESS,
                SessionType.Unknown => SESSION_TYPE.UNKNOWN,
                _ => SESSION_TYPE.UNKNOWN,
            };
        }
    }
}
