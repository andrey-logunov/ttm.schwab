using Newtonsoft.Json.Linq;
using System.Text;

namespace ttm.schwab.api
{
    internal sealed class ACCOUNT_NUMBER_ENCRYPTED
    {
        public string AccountNumber { get; set; }
        public string HashValue { get; set; }

        private ACCOUNT_NUMBER_ENCRYPTED() { }

        public static ACCOUNT_NUMBER_ENCRYPTED Parse(JToken jtoken)
        {
            if (jtoken == null) return null;

            return new ACCOUNT_NUMBER_ENCRYPTED
            {
                AccountNumber = Convert.ToString(jtoken["accountNumber"]),
                HashValue = Convert.ToString(jtoken["hashValue"]),
            };
        }

        public static ACCOUNT_NUMBER_ENCRYPTED[] ParseArray(JToken jtoken)
        {
            return jtoken.Children().Select(Parse).Where(p => p != null).ToArray();
        }

        public static ACCOUNT_NUMBER_ENCRYPTED[] ParseArray(string json)
        {
            return ParseArray(JToken.Parse(json));
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("ACCOUNT NUMBER ENCRYPTED");
            sb.AppendLine();
            sb.AppendLine($"Account Number:                     {AccountNumber}");
            sb.AppendLine($"Hash Value:                         {HashValue}");
            return sb.ToString();
        }
    }
}
