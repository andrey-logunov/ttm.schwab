using ttm.schwab.api.enumerations;

namespace ttm.schwab
{
    public enum OptionExpirationType
    {
        Regular,
        Weekly,
        EndOfMonth,
        Quarterly,
        Unknown
    }

    internal static partial class TRANSLATE
    {
        public static OptionExpirationType Translate(OPTION_EXPIRATION_TYPE optionExpirationType)
        {
            return optionExpirationType switch
            {
                OPTION_EXPIRATION_TYPE.REGULAR => OptionExpirationType.Regular,
                OPTION_EXPIRATION_TYPE.WEEKLY => OptionExpirationType.Weekly,
                OPTION_EXPIRATION_TYPE.END_OF_MONTH => OptionExpirationType.EndOfMonth,
                OPTION_EXPIRATION_TYPE.QUARTERLY => OptionExpirationType.Quarterly,
                OPTION_EXPIRATION_TYPE.UNKNOWN => OptionExpirationType.Unknown,
                _ => OptionExpirationType.Unknown,
            };
        }

        public static OPTION_EXPIRATION_TYPE Translate(OptionExpirationType optionExpirationType)
        {
            return optionExpirationType switch
            {
                OptionExpirationType.Regular => OPTION_EXPIRATION_TYPE.REGULAR,
                OptionExpirationType.Weekly => OPTION_EXPIRATION_TYPE.WEEKLY,
                OptionExpirationType.EndOfMonth => OPTION_EXPIRATION_TYPE.END_OF_MONTH,
                OptionExpirationType.Quarterly => OPTION_EXPIRATION_TYPE.QUARTERLY,
                OptionExpirationType.Unknown => OPTION_EXPIRATION_TYPE.UNKNOWN,
                _ => OPTION_EXPIRATION_TYPE.UNKNOWN,
            };
        }
    }
}
