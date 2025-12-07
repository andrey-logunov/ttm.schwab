using ttm.schwab.api.enumerations;

namespace ttm.schwab
{
    public enum AccountType
    {
        Cash,
        Margin,
        Unknown
    }

    internal static partial class TRANSLATE
    {
        public static AccountType Translate(ACCOUNT_TYPE accountType)
        {
            return accountType switch
            {
                ACCOUNT_TYPE.CASH => AccountType.Cash,
                ACCOUNT_TYPE.MARGIN => AccountType.Margin,
                ACCOUNT_TYPE.UNKNOWN => AccountType.Unknown,
                _ => AccountType.Cash,
            };
        }

        public static ACCOUNT_TYPE Translate(AccountType accountType)
        {
            return accountType switch
            {
                AccountType.Cash => ACCOUNT_TYPE.CASH,
                AccountType.Margin => ACCOUNT_TYPE.MARGIN,
                AccountType.Unknown => ACCOUNT_TYPE.UNKNOWN,
                _ => ACCOUNT_TYPE.UNKNOWN,
            };
        }
    }
}
