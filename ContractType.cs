using ttm.schwab.api.enumerations;

namespace ttm.schwab
{
    public enum ContractType
    {
        Put,
        Call,
        Unknown
    }

    internal static partial class TRANSLATE
    {
        public static ContractType Translate(CONTRACT_TYPE contractType)
        {
            return contractType switch
            {
                CONTRACT_TYPE.PUT => ContractType.Put,
                CONTRACT_TYPE.CALL => ContractType.Call,
                CONTRACT_TYPE.UNKNOWN => ContractType.Unknown,
                _ => ContractType.Unknown,
            };
        }

        public static CONTRACT_TYPE Translate(ContractType contractType)
        {
            return contractType switch
            {
                ContractType.Put => CONTRACT_TYPE.PUT,
                ContractType.Call => CONTRACT_TYPE.CALL,
                ContractType.Unknown => CONTRACT_TYPE.UNKNOWN,
                _ => CONTRACT_TYPE.UNKNOWN,
            };
        }
    }
}
