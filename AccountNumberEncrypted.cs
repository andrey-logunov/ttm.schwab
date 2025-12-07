using System.Text;
using ttm.schwab.api;

namespace ttm.schwab
{
    public sealed class AccountNumberEncrypted
    {
        public string AccountNumber { get; private set; }
        public string HashValue { get; private set; }

        private AccountNumberEncrypted() { }

        internal static AccountNumberEncrypted Copy(ACCOUNT_NUMBER_ENCRYPTED number)
        {
            return new AccountNumberEncrypted
            {
                AccountNumber = number.AccountNumber,
                HashValue = number.HashValue,
            };
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("Account Number Encrypted");
            sb.AppendLine();
            sb.AppendLine($"Account Number:                     {AccountNumber}");
            sb.AppendLine($"Hash Value:                         {HashValue}");
            return sb.ToString();
        }
    }
}
