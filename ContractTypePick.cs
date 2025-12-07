using ttm.schwab.api.enumerations;

namespace ttm.schwab
{
    public enum ContractTypePick
    {
        Put,
        Call,
        All,
        Unknown
    }

    internal static partial class TRANSLATE
    {
        public static ContractTypePick Translate(CONTRACT_TYPE_PICK contractTypePick)
        {
            return contractTypePick switch
            {
                CONTRACT_TYPE_PICK.PUT => ContractTypePick.Put,
                CONTRACT_TYPE_PICK.CALL => ContractTypePick.Call,
                CONTRACT_TYPE_PICK.ALL => ContractTypePick.All,
                CONTRACT_TYPE_PICK.UNKNOWN => ContractTypePick.Unknown,
                _ => ContractTypePick.Unknown,
            };
        }

        public static CONTRACT_TYPE_PICK Translate(ContractTypePick contractTypePick)
        {
            return contractTypePick switch
            {
                ContractTypePick.Put => CONTRACT_TYPE_PICK.PUT,
                ContractTypePick.Call => CONTRACT_TYPE_PICK.CALL,
                ContractTypePick.All => CONTRACT_TYPE_PICK.ALL,
                ContractTypePick.Unknown => CONTRACT_TYPE_PICK.UNKNOWN,
                _ => CONTRACT_TYPE_PICK.UNKNOWN,
            };
        }
    }
}
