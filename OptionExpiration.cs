using System;
using System.Text;
using ttm.schwab.api;
using ttm.schwab.api.enumerations;

namespace ttm.schwab
{
    public sealed class OptionExpiration
    {
        public DateTime ExpirationDate { get; private set; }
        public int DaysToExpiration { get; private set; }
        public OptionExpirationType ExpirationType { get; private set; }
        public bool Standard { get; private set; }

        private OptionExpiration() { }

        internal static OptionExpiration Copy(OPTION_EXPIRATION optionExpiration)
        {
            return new OptionExpiration
            {
                ExpirationDate = optionExpiration.ExpirationDate,
                DaysToExpiration = optionExpiration.DaysToExpiration,
                ExpirationType = TRANSLATE.Translate(optionExpiration.ExpirationType),
                Standard = optionExpiration.Standard,
            };
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("Option Expiration");
            sb.AppendLine();
            sb.AppendLine($"Expiration Date:                    {ExpirationDate:yyyy-MM-dd}");
            sb.AppendLine($"Days To Expiration:                 {DaysToExpiration}");
            sb.AppendLine($"Expiration Type:                    {CONVERT.ToString(TRANSLATE.Translate(ExpirationType))}");
            sb.AppendLine($"Standard:                           {Standard}");
            return sb.ToString();
        }
    }
}
