using ttm.schwab.api.enumerations;

namespace ttm.schwab
{
    public enum SpecialInstructionType
    {
        AllOrNone,
        DoNotReduce,
        AllOrNoneDoNotReduce,
        Unknown
    }

    internal static partial class TRANSLATE
    {
        public static SpecialInstructionType Translate(SPECIAL_INSTRUCTION_TYPE specialInstructionType)
        {
            return specialInstructionType switch
            {
                SPECIAL_INSTRUCTION_TYPE.ALL_OR_NONE => SpecialInstructionType.AllOrNone,
                SPECIAL_INSTRUCTION_TYPE.DO_NOT_REDUCE => SpecialInstructionType.DoNotReduce,
                SPECIAL_INSTRUCTION_TYPE.ALL_OR_NONE_DO_NOT_REDUCE => SpecialInstructionType.AllOrNoneDoNotReduce,
                SPECIAL_INSTRUCTION_TYPE.UNKNOWN => SpecialInstructionType.Unknown,
                _ => SpecialInstructionType.Unknown,
            };
        }

        public static SPECIAL_INSTRUCTION_TYPE Translate(SpecialInstructionType specialInstructionType)
        {
            return specialInstructionType switch
            {
                SpecialInstructionType.AllOrNone => SPECIAL_INSTRUCTION_TYPE.ALL_OR_NONE,
                SpecialInstructionType.DoNotReduce => SPECIAL_INSTRUCTION_TYPE.DO_NOT_REDUCE,
                SpecialInstructionType.AllOrNoneDoNotReduce => SPECIAL_INSTRUCTION_TYPE.ALL_OR_NONE_DO_NOT_REDUCE,
                SpecialInstructionType.Unknown => SPECIAL_INSTRUCTION_TYPE.UNKNOWN,
                _ => SPECIAL_INSTRUCTION_TYPE.UNKNOWN,
            };
        }
    }
}
