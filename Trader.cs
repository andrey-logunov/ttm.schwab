using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ttm.schwab.api;
using ttm.schwab.fake;

namespace ttm.schwab
{
    public sealed class Trader(string accountNumber, string apiKey, string apiSecret, string refreshToken, int millisecondsCallDelay = 0, Action<int> accessTokenGenerated = null, bool dummy = false)
    {
        #region || Call Queue ||

        class CallQueue(int limit)
        {
            private int Limit { get; set; } = limit;
            private Queue<(DateTime Time, string Description, string Response)> Queue { get; set; } = new();

            public void Add(string description, string response)
            {
                Queue.Enqueue((DateTime.Now, description, response));
                while (Queue.Count > Limit) Queue.Dequeue();
            }

            public IEnumerable<(DateTime Time, string Description, string Response)> GetItems()
            {
                foreach ((DateTime Time, string Description, string Response) in Queue)
                {
                    yield return (Time, Description, Response);
                }
            }

            public IEnumerable<(DateTime Time, string Description, string Response)> GetLastItems(int count)
            {
                foreach ((DateTime Time, string Description, string Response) in Queue.TakeLast(count))
                {
                    yield return (Time, Description, Response);
                }
            }
        }

        #endregion

        const int ACCESS_TOKEN_TIMEOUT_MINUTES = 29;
        const int MINIMUM_CALL_DELAY_MILLISECONDS = 500;
        const int LOG_LIMIT = 1000;

        string AccountNumber { get; set; } = accountNumber;
        string ApiKey { get; set; } = apiKey;
        string ApiSecret { get; set; } = apiSecret;
        string RefreshToken { get; set; } = refreshToken;

        bool Dummy { get; set; } = dummy;

        int MillisecondsCallDelay { get; set; } = millisecondsCallDelay > MINIMUM_CALL_DELAY_MILLISECONDS ? millisecondsCallDelay : MINIMUM_CALL_DELAY_MILLISECONDS;
        Action<int> AccessTokenGenerated { get; set; } = accessTokenGenerated ?? (_ => { });
        CallQueue Log { get; set; } = new CallQueue(LOG_LIMIT);

        #region || Log ||

        public IEnumerable<(DateTime Time, string Description, string Response)> GetLog()
        {
            return Log.GetItems();
        }

        public IEnumerable<(DateTime Time, string Description, string Response)> GetLastLog(int count)
        {
            return Log.GetLastItems(count);
        }

        #endregion

        #region || Traffic Length ||

        static int trafficLength = 0;
        public static int TrafficLength { get { return trafficLength; } }

        #endregion

        #region || Access Token ||

        private string accessToken = null;
        private DateTime updateTime = DateTime.Now;

        static int numberOfAccessTokensGenerated = 0;
        public static int NumberOfAccessTokensGenerated { get { return numberOfAccessTokensGenerated; } }

        readonly static SemaphoreSlim semaphoreSlim = new(1, 1);

        public async Task<string> GetRefreshTokenAsync(string redirectUri, string authorizationCode)
        {
            Interlocked.Increment(ref trafficLength);
            await semaphoreSlim.WaitAsync();
            try
            {
                await Task.Delay(MillisecondsCallDelay);

                Interlocked.Increment(ref numberOfAccessTokensGenerated);
                AccessTokenGenerated(NumberOfAccessTokensGenerated);
                updateTime = DateTime.Now;

                if (Dummy) return "[ DUMMY TOKEN ]";

                (ACCESS_TOKEN token, string json) = await Schwab.GetRefreshTokenAsync(ApiKey, ApiSecret, redirectUri, authorizationCode);
                Log.Add("GetRefreshTokenAsync", json);

                accessToken = token.AccessToken;
                RefreshToken = token.RefreshToken;
            }
            finally
            {
                semaphoreSlim.Release();
                Interlocked.Decrement(ref trafficLength);
            }
            return RefreshToken;
        }

        private async Task<string> GetAccessTokenAsync()
        {
            if (string.IsNullOrEmpty(accessToken) || DateTime.Now.Subtract(updateTime).TotalMinutes > ACCESS_TOKEN_TIMEOUT_MINUTES)
            {
                Interlocked.Increment(ref trafficLength);
                await semaphoreSlim.WaitAsync();
                try
                {
                    await Task.Delay(MillisecondsCallDelay);

                    Interlocked.Increment(ref numberOfAccessTokensGenerated);
                    AccessTokenGenerated(NumberOfAccessTokensGenerated);
                    updateTime = DateTime.Now;

                    if (Dummy) return "[ DUMMY TOKEN ]";

                    (ACCESS_TOKEN token, string json) = await Schwab.GetAccessTokenAsync(ApiKey, ApiSecret, RefreshToken);
                    Log.Add("GetAccessTokenAsync", json);

                    accessToken = token.AccessToken;
                }
                finally
                {
                    semaphoreSlim.Release();
                    Interlocked.Decrement(ref trafficLength);
                }
            }
            return accessToken;
        }

        public async Task<bool> IsConnectedAsync() {
            await semaphoreSlim.WaitAsync();
            try
            {
                return !string.IsNullOrEmpty(accessToken);
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        public async Task ResetTokenAsync()
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                accessToken = null;
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        #endregion

        #region || Account ||

        public async Task<AccountNumberEncrypted[]> GetAccountNumberEncryptedAsync()
        {
            string accessToken = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(accessToken)) return null;

            Interlocked.Increment(ref trafficLength);
            await semaphoreSlim.WaitAsync();
            try
            {
                await Task.Delay(MillisecondsCallDelay);

                if (Dummy) return [];

                (ACCOUNT_NUMBER_ENCRYPTED[] numbers, string json) = await Schwab.GetAccountNumberEncryptedAsync(accessToken);
                Log.Add($"GetAccountNumberEncryptedAsync", json);
                return numbers.Select(AccountNumberEncrypted.Copy).ToArray();
            }
            finally
            {
                semaphoreSlim.Release();
                Interlocked.Decrement(ref trafficLength);
            }
        }

        public async Task<Account> GetAccountAsync()
        {
            string accessToken = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(accessToken)) return null;

            Interlocked.Increment(ref trafficLength);
            await semaphoreSlim.WaitAsync();
            try
            {
                await Task.Delay(MillisecondsCallDelay);

                if (Dummy) return Fake.FakeAccount();

                (ACCOUNT account, string json) = await Schwab.GetAccountAsync(AccountNumber, accessToken);
                Log.Add($"GetAccountAsync", json);
                return Account.Copy(account);
            }
            finally
            {
                semaphoreSlim.Release();
                Interlocked.Decrement(ref trafficLength);
            }
        }

        #endregion

        #region || Orders ||

        public async Task<Order[]> GetOrdersAsync()
        {
            string accessToken = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(accessToken)) return null;

            Interlocked.Increment(ref trafficLength);
            await semaphoreSlim.WaitAsync();
            try
            {
                await Task.Delay(MillisecondsCallDelay);

                if (Dummy) return Fake.FakeOrders();

                (ORDER[] orders, string json) = await Schwab.GetOrdersAsync(AccountNumber, accessToken);
                Log.Add("GetOrdersAsync", json);
                return orders.Select(Order.Copy).ToArray();
            }
            finally
            {
                semaphoreSlim.Release();
                Interlocked.Decrement(ref trafficLength);
            }
        }

        public async Task<Order> GetOrderAsync(long orderId)
        {
            string accessToken = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(accessToken)) return null;

            Interlocked.Increment(ref trafficLength);
            await semaphoreSlim.WaitAsync();
            try
            {
                await Task.Delay(MillisecondsCallDelay);

                if (Dummy) return Fake.FakeOrder();

                (ORDER order, string json) = await Schwab.GetOrderAsync(AccountNumber, orderId, accessToken);
                Log.Add($"GetOrderAsync {orderId}", json);
                return Order.Copy(order);
            }
            finally
            {
                semaphoreSlim.Release();
                Interlocked.Decrement(ref trafficLength);
            }
        }

        public async Task<bool> PlaceOrderAsync(OrderBlueprint orderBlueprint)
        {
            string accessToken = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(accessToken)) return false;

            Interlocked.Increment(ref trafficLength);
            await semaphoreSlim.WaitAsync();
            try
            {
                await Task.Delay(MillisecondsCallDelay);

                if (Dummy) return true;

                (bool success, string message) = await Schwab.PlaceOrderAsync(AccountNumber, new ORDER_BLUEPRINT(orderBlueprint.Blueprint), accessToken);
                Log.Add($"PlaceOrderAsync {orderBlueprint.Blueprint}", message);
                return success;
            }
            finally
            {
                semaphoreSlim.Release();
                Interlocked.Decrement(ref trafficLength);
            }
        }

        public async Task<bool> CancelOrderAsync(long orderId)
        {
            string accessToken = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(accessToken)) return false;

            Interlocked.Increment(ref trafficLength);
            await semaphoreSlim.WaitAsync();
            try
            {
                await Task.Delay(MillisecondsCallDelay);

                if (Dummy) return true;

                (bool success, string message) = await Schwab.CancelOrderAsync(AccountNumber, orderId, accessToken);
                Log.Add($"CancelOrderAsync {orderId}", message);
                return success;
            }
            finally
            {
                semaphoreSlim.Release();
                Interlocked.Decrement(ref trafficLength);
            }
        }

        public async Task<bool> ReplaceOrderAsync(long orderId, OrderBlueprint orderBlueprint)
        {
            string accessToken = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(accessToken)) return false;

            Interlocked.Increment(ref trafficLength);
            await semaphoreSlim.WaitAsync();
            try
            {
                await Task.Delay(MillisecondsCallDelay);

                if (Dummy) return true;

                (bool success, string message) = await Schwab.ReplaceOrderAsync(AccountNumber, orderId, new ORDER_BLUEPRINT(orderBlueprint.Blueprint), accessToken);
                Log.Add($"ReplaceOrderAsync {orderId} {orderBlueprint.Blueprint}", message);
                return success;
            }
            finally
            {
                semaphoreSlim.Release();
                Interlocked.Decrement(ref trafficLength);
            }
        }

        #endregion

        #region || Quotes ||

        public async Task<Quote[]> GetQuotesAsync(string[] symbols)
        {
            string accessToken = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(accessToken)) return null;

            Interlocked.Increment(ref trafficLength);
            await semaphoreSlim.WaitAsync();
            try
            {
                await Task.Delay(MillisecondsCallDelay);

                if (Dummy) return Fake.FakeQuotes();

                (QUOTE[] quotes, string json) = await Schwab.GetQuotesAsync(symbols, accessToken);
                Log.Add($"GetQuotesAsync {string.Join(",", symbols)}", json);
                return quotes.Select(Quote.Copy).ToArray();
            }
            finally
            {
                semaphoreSlim.Release();
                Interlocked.Decrement(ref trafficLength);
            }
        }

        public async Task<Quote> GetQuoteAsync(string symbol)
        {
            string accessToken = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(accessToken)) return null;

            Interlocked.Increment(ref trafficLength);
            await semaphoreSlim.WaitAsync();
            try
            {
                await Task.Delay(MillisecondsCallDelay);

                if (Dummy) return Fake.FakeQuote();

                (QUOTE quote, string json) = await Schwab.GetQuoteAsync(symbol, accessToken);
                Log.Add($"GetQuoteAsync {symbol}", json);
                return Quote.Copy(quote);
            }
            finally
            {
                semaphoreSlim.Release();
                Interlocked.Decrement(ref trafficLength);
            }
        }

        #endregion

        #region || Option Chains ||

        public async Task<OptionChain> GetOptionChainAsync(string symbol, DateTime fromDate, DateTime toDate)
        {
            string accessToken = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(accessToken)) return null;

            Interlocked.Increment(ref trafficLength);
            await semaphoreSlim.WaitAsync();
            try
            {
                await Task.Delay(MillisecondsCallDelay);

                if (Dummy) return Fake.FakeOptionChain();

                (OPTION_CHAIN optionChain, string json) = await Schwab.GetOptionChainAsync(symbol, fromDate, toDate, accessToken);
                Log.Add($"GetOptionChainAsync {symbol} {fromDate:yyyy-MM-dd} {toDate:yyyy-MM-dd}", json);
                return OptionChain.Copy(optionChain);
            }
            finally
            {
                semaphoreSlim.Release();
                Interlocked.Decrement(ref trafficLength);
            }
        }

        public async Task<OptionExpiration[]> GetOptionExpirationChainAsync(string symbol)
        {
            string accessToken = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(accessToken)) return null;

            Interlocked.Increment(ref trafficLength);
            await semaphoreSlim.WaitAsync();
            try
            {
                await Task.Delay(MillisecondsCallDelay);

                if (Dummy) return [];

                (OPTION_EXPIRATION[] optionExpirations, string json) = await Schwab.GetOptionExpirationChainAsync(symbol, accessToken);
                Log.Add($"GetOptionExpirationChainAsync {symbol}", json);
                return optionExpirations.Select(OptionExpiration.Copy).ToArray();
            }
            finally
            {
                semaphoreSlim.Release();
                Interlocked.Decrement(ref trafficLength);
            }
        }

        #endregion

        #region || Equities ||

        const int NUMBER_OF_ATTEMPTS = 3;

        static ConcurrentDictionary<string, Equity> DictionaryOfEquities { get; set; } = [];
        static ConcurrentDictionary<string, int> DictionaryOfAttempts { get; set; } = [];

        public async Task<Equity> GetEquityAsync(string symbol)
        {
            if (Instrument.LookLike(symbol) != AssetType.Equity) return null;
            symbol = symbol.Trim().ToUpper();
            if (DictionaryOfAttempts.TryGetValue(symbol, out int attempts) && attempts > NUMBER_OF_ATTEMPTS) return null;

            if (DictionaryOfEquities.TryGetValue(symbol, out Equity equityCached)) return equityCached;

            string accessToken = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(accessToken)) return null;

            Interlocked.Increment(ref trafficLength);
            await semaphoreSlim.WaitAsync();
            try
            {
                await Task.Delay(MillisecondsCallDelay);

                if (Dummy) return Fake.FakeEquities()[0];

                (EQUITY EQUITY, string json) = await Schwab.GetEquityAsync(symbol, accessToken);
                Log.Add($"GetEquityAsync {symbol}", json);

                if (EQUITY == null)
                {
                    if (!DictionaryOfAttempts.ContainsKey(symbol)) DictionaryOfAttempts[symbol] = 0;
                    DictionaryOfAttempts[symbol]++;
                    return null;
                }

                Equity equity = Equity.Copy(EQUITY);
                DictionaryOfEquities[equity.Symbol] = equity;
                return equity;
            }
            finally
            {
                semaphoreSlim.Release();
                Interlocked.Decrement(ref trafficLength);
            }
        }

        public async Task<Equity[]> GetEquitiesAsync(string symbolPattern)
        {
            string accessToken = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(accessToken)) return null;

            Interlocked.Increment(ref trafficLength);
            await semaphoreSlim.WaitAsync();
            try
            {
                await Task.Delay(MillisecondsCallDelay);

                if (Dummy) return Fake.FakeEquities();

                (EQUITY[] equities, string json) = await Schwab.GetEquitiesAsync(symbolPattern, accessToken);
                Log.Add($"GetEquitiesAsync {symbolPattern}", json);
                return equities.Select(Equity.Copy).Select(p => { DictionaryOfEquities[p.Symbol] = p; return p; }).ToArray();
            }
            catch (Exception ex)
            {
                Log.Add($"GetEquitiesAsync {symbolPattern}", ex.Message);
                return null;
            }
            finally
            {
                semaphoreSlim.Release();
                Interlocked.Decrement(ref trafficLength);
            }
        }

        static bool GetEquitiesDone { get; set; } = false;

        public async Task<Equity[]> GetEquitiesAsync(bool startOver, CancellationToken cancelToken)
        {
            if (!GetEquitiesDone || startOver)
            {
                foreach (char letter1 in "abcdefghijklmnopqrstuvwxyz")
                {
                    foreach (char letter2 in "abcdefghijklmnopqrstuvwxyz")
                    {
                        await GetEquitiesAsync($"{letter1}{letter2}.*");
                        if (cancelToken.IsCancellationRequested)
                        {
                            return [.. DictionaryOfEquities.Values.OrderBy(p => p.Symbol)];
                        }
                    }
                }
                GetEquitiesDone = true;
            }
            return [.. DictionaryOfEquities.Values.OrderBy(p => p.Symbol)];
        }

        public async Task<Equity[]> GetEquitiesAsync(CancellationToken cancelToken)
        {
            return await GetEquitiesAsync(false, cancelToken);
        }

        #endregion

        #region || Fundamental ||

        static ConcurrentDictionary<string, Fundamental> DictionaryOfFundamentals { get; set; } = [];

        public async Task<Fundamental> GetFundamentalAsync(string symbol)
        {
            if (Instrument.LookLike(symbol) != AssetType.Equity) return null;
            symbol = symbol.Trim().ToUpper();
            if (DictionaryOfAttempts.TryGetValue(symbol, out int attempts) && attempts > NUMBER_OF_ATTEMPTS) return null;

            if (DictionaryOfFundamentals.TryGetValue(symbol, out Fundamental fundamentalCached)) return fundamentalCached;

            string accessToken = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(accessToken)) return null;

            Interlocked.Increment(ref trafficLength);
            await semaphoreSlim.WaitAsync();
            try
            {
                await Task.Delay(MillisecondsCallDelay);

                if (Dummy) return Fundamental.Copy(Fake.FakeEquities()[0]);

                (FUNDAMENTAL FUNDAMENTAL, string json) = await Schwab.GetFundamentalAsync(symbol, accessToken);
                Log.Add($"GetFundamentalAsync {symbol}", json);

                if (FUNDAMENTAL == null)
                {
                    if (!DictionaryOfAttempts.ContainsKey(symbol)) DictionaryOfAttempts[symbol] = 0;
                    DictionaryOfAttempts[symbol]++;
                    return null;
                }

                Fundamental fundamental = Fundamental.Copy(FUNDAMENTAL);
                DictionaryOfFundamentals[fundamental.Symbol] = fundamental;
                return fundamental;
            }
            finally
            {
                semaphoreSlim.Release();
                Interlocked.Decrement(ref trafficLength);
            }
        }

        #endregion

        #region || Price History ||

        public async Task<Price[]> GetPriceHistoryAsync(string symbol)
        {
            string accessToken = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(accessToken)) return null;

            Interlocked.Increment(ref trafficLength);
            await semaphoreSlim.WaitAsync();
            try
            {
                await Task.Delay(MillisecondsCallDelay);

                if (Dummy) return [];

                (PRICE[] prices, string json) = await Schwab.GetPriceHistoryAsync(symbol, accessToken);
                Log.Add($"GetPriceHistoryAsync {symbol}", json);
                return prices.Select(Price.Copy).ToArray();
            }
            finally
            {
                semaphoreSlim.Release();
                Interlocked.Decrement(ref trafficLength);
            }
        }

        #endregion
    }
}
