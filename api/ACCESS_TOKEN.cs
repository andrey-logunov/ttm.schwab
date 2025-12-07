using Newtonsoft.Json.Linq;
using System.Text;

namespace ttm.schwab.api
{
    internal sealed class ACCESS_TOKEN
    {
        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }

        private ACCESS_TOKEN() { }

        public static ACCESS_TOKEN Parse(JToken jtoken)
        {
            if (jtoken == null) return null;

            return new ACCESS_TOKEN
            {
                AccessToken = Convert.ToString(jtoken["access_token"]),
                RefreshToken = Convert.ToString(jtoken["refresh_token"]),
            };
        }

        public static ACCESS_TOKEN Parse(string json)
        {
            return Parse(JToken.Parse(json));
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"AccessToken:           {AccessToken}");
            sb.AppendLine($"RefreshToken:          {RefreshToken}");
            return sb.ToString();
        }
    }
}
