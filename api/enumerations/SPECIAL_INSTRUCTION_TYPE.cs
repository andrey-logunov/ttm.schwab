namespace ttm.schwab.api.enumerations
{
    internal enum SPECIAL_INSTRUCTION_TYPE
    {
        ALL_OR_NONE,
        DO_NOT_REDUCE,
        ALL_OR_NONE_DO_NOT_REDUCE,
        UNKNOWN
    }

    internal static partial class CONVERT
    {
        public static string ToString(SPECIAL_INSTRUCTION_TYPE specialInstructionType)
        {
            return specialInstructionType switch
            {
                SPECIAL_INSTRUCTION_TYPE.ALL_OR_NONE => "ALL_OR_NONE",
                SPECIAL_INSTRUCTION_TYPE.DO_NOT_REDUCE => "DO_NOT_REDUCE",
                SPECIAL_INSTRUCTION_TYPE.ALL_OR_NONE_DO_NOT_REDUCE => "ALL_OR_NONE_DO_NOT_REDUCE",
                _ => null,
            };
        }

        public static bool FromString(string text, out SPECIAL_INSTRUCTION_TYPE specialInstructionType)
        {
            switch (text)
            {
                case "ALL_OR_NONE":
                    specialInstructionType = SPECIAL_INSTRUCTION_TYPE.ALL_OR_NONE;
                    return true;
                case "DO_NOT_REDUCE":
                    specialInstructionType = SPECIAL_INSTRUCTION_TYPE.DO_NOT_REDUCE;
                    return true;
                case "ALL_OR_NONE_DO_NOT_REDUCE":
                    specialInstructionType = SPECIAL_INSTRUCTION_TYPE.ALL_OR_NONE_DO_NOT_REDUCE;
                    return true;
                case "UNKNOWN":
                    specialInstructionType = SPECIAL_INSTRUCTION_TYPE.UNKNOWN;
                    return true;
                default:
                    specialInstructionType = SPECIAL_INSTRUCTION_TYPE.UNKNOWN;
                    return false;
            }
        }
    }
}
