using ttm.schwab.api.enumerations;

namespace ttm.schwab
{
    public enum OptionType
    {
        Vanilla,
        Binary,
        Barrier,
        Unknown
    }

    internal static partial class TRANSLATE
    {
        public static OptionType Translate(OPTION_TYPE optionType)
        {
            return optionType switch
            {
                OPTION_TYPE.VANILLA => OptionType.Vanilla,
                OPTION_TYPE.BINARY => OptionType.Binary,
                OPTION_TYPE.BARRIER => OptionType.Barrier,
                OPTION_TYPE.UNKNOWN => OptionType.Unknown,
                _ => OptionType.Unknown,
            };
        }

        public static OPTION_TYPE Translate(OptionType optionType)
        {
            return optionType switch
            {
                OptionType.Vanilla => OPTION_TYPE.VANILLA,
                OptionType.Binary => OPTION_TYPE.BINARY,
                OptionType.Barrier => OPTION_TYPE.BARRIER,
                OptionType.Unknown => OPTION_TYPE.UNKNOWN,
                _ => OPTION_TYPE.UNKNOWN,
            };
        }
    }
}
