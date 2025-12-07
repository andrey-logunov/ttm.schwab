using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Text;
using ttm.schwab.api.enumerations;

namespace ttm.schwab.api
{
    internal sealed class OPTION_EXPIRATION
    {
        public DateTime ExpirationDate { get; private set; }
        public int DaysToExpiration { get; private set; }
        public OPTION_EXPIRATION_TYPE ExpirationType { get; private set; }
        public bool Standard { get; private set; }

        private OPTION_EXPIRATION() { }

        public static OPTION_EXPIRATION Parse(JToken jtoken)
        {
            if (jtoken == null) return null;

            return new OPTION_EXPIRATION
            {
                ExpirationDate = Convert.ToDateTime(jtoken["expirationDate"]),
                DaysToExpiration = Convert.ToInt32(jtoken["daysToExpiration"]),
                ExpirationType = CONVERT.FromString(Convert.ToString(jtoken["expirationType"]), out OPTION_EXPIRATION_TYPE expirationType) ? expirationType : OPTION_EXPIRATION_TYPE.UNKNOWN,
                Standard = Convert.ToBoolean(jtoken["standard"]),
            };
        }

        public static OPTION_EXPIRATION Parse(string json)
        {
            return Parse(JToken.Parse(json));
        }

        public static OPTION_EXPIRATION[] ParseArray(JToken jtoken)
        {
            return jtoken.Children().Select(Parse).Where(p => p != null).ToArray();
        }

        public static OPTION_EXPIRATION[] ParseArray(string json)
        {
            return ParseArray(JToken.Parse(json));
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("OPTION EXPIRATION");
            sb.AppendLine();
            sb.AppendLine($"Expiration Date:                    {ExpirationDate:yyyy-MM-dd}");
            sb.AppendLine($"Days To Expiration:                 {DaysToExpiration}");
            sb.AppendLine($"Expiration Type:                    {CONVERT.ToString(ExpirationType)}");
            sb.AppendLine($"Standard:                           {Standard}");
            return sb.ToString();
        }
    }
}
