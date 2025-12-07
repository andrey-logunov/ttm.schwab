using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ttm.schwab.api
{
    internal static class Schwab
    {
        const int ORDER_DAYS_LIMIT = 30;
        const int ORDER_MAX_RESULTS = 3000;

        const string TOKEN_URL = @"https://api.schwabapi.com/v1/oauth/token";

        const string TRADER_URL = @"https://api.schwabapi.com/trader/v1/";

        const string ACCOUNT_NUMBERS_URL = TRADER_URL + @"accounts/accountNumbers";
        const string ACCOUNT_URL = TRADER_URL + @"accounts/{0}?fields=positions";

        const string ORDERS_URL = TRADER_URL + @"accounts/{0}/orders";
        const string ORDER_URL = ORDERS_URL + @"/{1}";
        const string ORDERS_DATES_URL = ORDERS_URL + @"?maxResults={3}&fromEnteredTime={1:yyyy-MM-dd}T00:00:00.000Z&toEnteredTime={2:yyyy-MM-dd}T00:00:00.000Z";

        const string MARKETDATA_URL = @"https://api.schwabapi.com/marketdata/v1/";

        const string QUOTES_URL = MARKETDATA_URL + @"quotes?symbols={0}&fields=quote";
        const string QUOTE_URL = MARKETDATA_URL + @"{0}/quotes?fields=quote";

        const string OPTION_CHAIN_URL = MARKETDATA_URL + @"chains?symbol={0}&contractType=ALL&strikeCount=100&fromDate={1:yyyy-MM-dd}&toDate={2:yyyy-MM-dd}";
        const string OPTION_EXPIRATION_CHAIN_URL = MARKETDATA_URL + @"expirationchain?symbol={0}";

        const string EQUITY_REGEX_URL = MARKETDATA_URL + @"instruments?symbol={0}&projection=symbol-regex";
        const string EQUITY_SEARCH_URL = MARKETDATA_URL + @"instruments?symbol={0}&projection=symbol-search";
        const string EQUITY_FUNDAMENTAL_URL = MARKETDATA_URL + @"instruments?symbol={0}&projection=fundamental";

        const string PRICE_HISTORY_URL = MARKETDATA_URL + @"pricehistory?symbol={0}&periodType=year&period=10&frequencyType=daily&needExtendedHoursData=true";

        static readonly KeyValuePair<string, string> userAgentHeader = new("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");

        #region |||| ACCESS TOKEN ||||

        public static async Task<string> GetRefreshTokenAsStringAsync(string apiKey, string apiSecret, string redirectUri, string authorizationCode)
        {
            int start = authorizationCode.IndexOf("code=");
            start = start > -1 ? start + 5 : 0;
            int end = authorizationCode.IndexOf("%40");
            if (end < start) return null;
            authorizationCode = string.Concat(authorizationCode.AsSpan(start, end - start), "@");

            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add(userAgentHeader.Key, userAgentHeader.Value);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(new UTF8Encoding().GetBytes($"{apiKey}:{apiSecret}")));
            FormUrlEncodedContent content = new([
                    new("grant_type", "authorization_code"),
                    new("code", authorizationCode),
                    new("redirect_uri", redirectUri),
                ]);
            return await httpClient.PostAsync(new Uri(TOKEN_URL), content).Result.Content.ReadAsStringAsync();
        }

        public static async Task<(ACCESS_TOKEN accessToken, string json)> GetRefreshTokenAsync(string apiKey, string apiSecret, string redirectUri, string authorizationCode)
        {
            string json = await GetRefreshTokenAsStringAsync(apiKey, apiSecret, redirectUri, authorizationCode);
            return (ACCESS_TOKEN.Parse(json), json);
        }

        public static async Task<string> GetAccessTokenAsStringAsync(string apiKey, string apiSecret, string refreshToken)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add(userAgentHeader.Key, userAgentHeader.Value);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(new UTF8Encoding().GetBytes($"{apiKey}:{apiSecret}")));
            FormUrlEncodedContent content = new([
                    new("grant_type", "refresh_token"),
                    new("refresh_token", refreshToken),
                ]);
            return await httpClient.PostAsync(new Uri(TOKEN_URL), content).Result.Content.ReadAsStringAsync();
        }

        public static async Task<(ACCESS_TOKEN accessToken, string json)> GetAccessTokenAsync(string apiKey, string apiSecret, string refreshToken)
        {
            string json = await GetAccessTokenAsStringAsync(apiKey, apiSecret, refreshToken);
            return (ACCESS_TOKEN.Parse(json), json);
        }

        #endregion

        #region |||| ACCOUNT ||||

        private static async Task<string> GetAccountNumberEncryptedAsStringAsync(string accessToken)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add(userAgentHeader.Key, userAgentHeader.Value);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            return await httpClient.GetStringAsync(new Uri(ACCOUNT_NUMBERS_URL));
        }

        public static async Task<(ACCOUNT_NUMBER_ENCRYPTED[] numbers, string json)> GetAccountNumberEncryptedAsync(string accessToken)
        {
            string json = await GetAccountNumberEncryptedAsStringAsync(accessToken);
            return (ACCOUNT_NUMBER_ENCRYPTED.ParseArray(json), json);
        }

        private static async Task<string> GetAccountAsStringAsync(string accountNumber, string accessToken)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add(userAgentHeader.Key, userAgentHeader.Value);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            return await httpClient.GetStringAsync(new Uri(string.Format(ACCOUNT_URL, accountNumber)));
        }

        public static async Task<(ACCOUNT account, string json)> GetAccountAsync(string accountNumber, string accessToken)
        {
            string json = await GetAccountAsStringAsync(accountNumber, accessToken);
            return (ACCOUNT.Parse(json), json);
        }

        #endregion

        #region |||| ORDERS ||||

        public static async Task<string> GetOrdersAsStringAsync(string accountNumber, string accessToken)
        {
            DateTime fromEnteredTime = DateTime.Today.AddDays(-ORDER_DAYS_LIMIT);
            DateTime toEnteredTime = DateTime.Today.AddDays(10);

            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add(userAgentHeader.Key, userAgentHeader.Value);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            return await httpClient.GetStringAsync(new Uri(string.Format(ORDERS_DATES_URL, accountNumber, fromEnteredTime, toEnteredTime, ORDER_MAX_RESULTS)));
        }

        public static async Task<(ORDER[] orders, string json)> GetOrdersAsync(string accountNumber, string accessToken)
        {
            string json = await GetOrdersAsStringAsync(accountNumber, accessToken);
            return (ORDER.ParseArray(json), json);
        }

        public static async Task<string> GetOrderAsStringAsync(string accountNumber, long orderId, string accessToken)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add(userAgentHeader.Key, userAgentHeader.Value);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            return await httpClient.GetStringAsync(new Uri(string.Format(ORDER_URL, accountNumber, orderId)));
        }

        public static async Task<(ORDER order, string json)> GetOrderAsync(string accountNumber, long orderId, string accessToken)
        {
            string json = await GetOrderAsStringAsync(accountNumber, orderId, accessToken);
            return (ORDER.Parse(json), json);
        }
 
        // Place Order

        private static async Task<(bool success, string message)> PostOrderAsync(string url, string jsonOrder, string accessToken)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add(userAgentHeader.Key, userAgentHeader.Value);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            HttpResponseMessage response = await httpClient.PostAsync(new Uri(url), new StringContent(jsonOrder, Encoding.UTF8, "application/json"));
            return (response.IsSuccessStatusCode, response.ReasonPhrase);
        }

        public static async Task<(bool success, string message)> PlaceOrderAsync(string accountNumber, ORDER_BLUEPRINT orderBlueprint, string accessToken)
        {
            return await PostOrderAsync(string.Format(ORDERS_URL, accountNumber), orderBlueprint.Blueprint, accessToken);
        }

        // Cancel Order

        public static async Task<(bool success, string message)> CancelOrderAsync(string accountNumber, long orderId, string accessToken)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add(userAgentHeader.Key, userAgentHeader.Value);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            HttpResponseMessage response = await httpClient.DeleteAsync(new Uri(string.Format(ORDER_URL, accountNumber, orderId)));
            return (response.IsSuccessStatusCode, response.ReasonPhrase);
        }

        // Replace Order

        private static async Task<(bool success, string message)> PutOrderAsync(string url, string jsonOrder, string accessToken)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add(userAgentHeader.Key, userAgentHeader.Value);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            HttpResponseMessage response = await httpClient.PutAsync(new Uri(url), new StringContent(jsonOrder, Encoding.UTF8, "application/json"));
            return (response.IsSuccessStatusCode, response.ReasonPhrase);
        }

        public static async Task<(bool success, string message)> ReplaceOrderAsync(string accountNumber, long orderId, ORDER_BLUEPRINT orderBlueprint, string accessToken)
        {
            return await PutOrderAsync(string.Format(ORDER_URL, accountNumber, orderId), orderBlueprint.Blueprint, accessToken);
        }

        #endregion

        #region |||| QUOTES ||||

        public static async Task<string> GetQuotesAsStringAsync(string[] symbols, string accessToken)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add(userAgentHeader.Key, userAgentHeader.Value);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            return await httpClient.GetStringAsync(new Uri(string.Format(QUOTES_URL, string.Join("%2C", symbols))));
        }

        public static async Task<(QUOTE[] quotes, string json)> GetQuotesAsync(string[] symbols, string accessToken)
        {
            string json = await GetQuotesAsStringAsync(symbols, accessToken);
            return (QUOTE.Parse(json), json);
        }

        public static async Task<string> GetQuoteAsStringAsync(string symbol, string accessToken)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add(userAgentHeader.Key, userAgentHeader.Value);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            return await httpClient.GetStringAsync(new Uri(string.Format(QUOTE_URL, symbol)));
        }

        public static async Task<(QUOTE quote, string json)> GetQuoteAsync(string symbol, string accessToken)
        {
            string json = await GetQuoteAsStringAsync(symbol, accessToken);
            QUOTE[] quotes = QUOTE.Parse(json);
            if (quotes != null && quotes.Length > 0) return (quotes[0], json);
            return (null, json);
        }

        #endregion

        #region |||| OPTION CHAIN ||||

        public static async Task<string> GetOptionChainAsStringAsync(string symbol, DateTime fromDate, DateTime toDate, string accessToken)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add(userAgentHeader.Key, userAgentHeader.Value);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            return await httpClient.GetStringAsync(new Uri(string.Format(OPTION_CHAIN_URL, symbol, fromDate, toDate)));
        }

        public static async Task<(OPTION_CHAIN optionChain, string json)> GetOptionChainAsync(string symbol, DateTime fromDate, DateTime toDate, string accessToken)
        {
            string json = await GetOptionChainAsStringAsync(symbol, fromDate, toDate, accessToken);
            return (OPTION_CHAIN.Parse(JToken.Parse(json)), json);
        }

        public static async Task<string> GetOptionExpirationChainAsStringAsync(string symbol, string accessToken)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add(userAgentHeader.Key, userAgentHeader.Value);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            return await httpClient.GetStringAsync(new Uri(string.Format(OPTION_EXPIRATION_CHAIN_URL, symbol)));
        }

        public static async Task<(OPTION_EXPIRATION[] expirations, string json)> GetOptionExpirationChainAsync(string symbol, string accessToken)
        {
            string json = await GetOptionExpirationChainAsStringAsync(symbol, accessToken);
            return (OPTION_EXPIRATION.ParseArray(JToken.Parse(json)["expirationList"]), json);
        }

        #endregion

        #region |||| EQUITIES ||||

        public static async Task<string> GetEquitiesAsStringAsync(string symbolPattern, string accessToken)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add(userAgentHeader.Key, userAgentHeader.Value);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            return await httpClient.GetStringAsync(new Uri(string.Format(EQUITY_REGEX_URL, symbolPattern)));
        }

        public static async Task<(EQUITY[] equities, string json)> GetEquitiesAsync(string symbolPattern, string accessToken)
        {
            string json = await GetEquitiesAsStringAsync(symbolPattern, accessToken);
            return (EQUITY.ParseArray(JToken.Parse(json)["instruments"]), json);
        }

        public static async Task<string> GetEquityAsStringAsync(string symbol, string accessToken)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add(userAgentHeader.Key, userAgentHeader.Value);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            return await httpClient.GetStringAsync(new Uri(string.Format(EQUITY_SEARCH_URL, symbol)));
        }

        public static async Task<(EQUITY equity, string json)> GetEquityAsync(string symbol, string accessToken)
        {
            string json = await GetEquityAsStringAsync(symbol, accessToken);
            return (EQUITY.ParseArray(JToken.Parse(json)["instruments"]).FirstOrDefault(), json);
        }

        public static async Task<string> GetFundamentalAsStringAsync(string symbol, string accessToken)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add(userAgentHeader.Key, userAgentHeader.Value);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            return await httpClient.GetStringAsync(new Uri(string.Format(EQUITY_FUNDAMENTAL_URL, symbol)));
        }

        public static async Task<(FUNDAMENTAL fundamental, string json)> GetFundamentalAsync(string symbol, string accessToken)
        {
            string json = await GetFundamentalAsStringAsync(symbol, accessToken);
            return (FUNDAMENTAL.ParseArray(JToken.Parse(json)["instruments"]).FirstOrDefault(), json);
        }

        #endregion

        #region |||| PRICE HISTORY ||||

        public static async Task<string> GetPriceHistoryAsStringAsync(string symbol, string accessToken)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add(userAgentHeader.Key, userAgentHeader.Value);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            return await httpClient.GetStringAsync(new Uri(string.Format(PRICE_HISTORY_URL, symbol)));
        }

        public static async Task<(PRICE[] prices, string json)> GetPriceHistoryAsync(string symbol, string accessToken)
        {
            string json = await GetPriceHistoryAsStringAsync(symbol, accessToken);
            return (PRICE.ParseArray(JToken.Parse(json)["candles"]), json);
        }

        #endregion

    }
}
