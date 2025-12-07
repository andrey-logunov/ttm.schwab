using System.Reflection;
using ttm.schwab.api;

namespace ttm.schwab.fake
{
    internal static class Fake
    {
        const string FAKE_ACCOUNT = @"ttm.schwab.fake.json.account.json";
        const string FAKE_ORDERS = @"ttm.schwab.fake.json.orders.json";
        const string FAKE_QUOTES = @"ttm.schwab.fake.json.quotes.json";
        const string FAKE_OPTION_CHAIN = @"ttm.schwab.fake.json.optionchain.json";
        const string FAKE_EQUITIES = @"ttm.schwab.fake.json.equities.json";

        public static Account FakeAccount()
        {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(FAKE_ACCOUNT);
            using StreamReader reader = new(stream);
            return Account.Copy(ACCOUNT.Parse(reader.ReadToEnd()));
        }

        public static Order[] FakeOrders()
        {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(FAKE_ORDERS);
            using StreamReader reader = new(stream);
            return ORDER.ParseArray(reader.ReadToEnd()).Select(Order.Copy).ToArray();
        }

        public static Order FakeOrder()
        {
            Order[] orders = FakeOrders();
            return orders != null && orders.Length > 0 ? orders[0] : null;
        }

        public static Quote[] FakeQuotes()
        {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(FAKE_QUOTES);
            using StreamReader reader = new(stream);
            return  QUOTE.Parse(reader.ReadToEnd()).Select(Quote.Copy).ToArray();
        }

        public static Quote FakeQuote()
        {
            Quote[] quotes = FakeQuotes();
            return quotes != null && quotes.Length > 0 ? quotes[0] : null;
        }

        public static OptionChain FakeOptionChain()
        {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(FAKE_OPTION_CHAIN);
            using StreamReader reader = new(stream);
            return OptionChain.Copy(OPTION_CHAIN.Parse(reader.ReadToEnd()));
        }

        public static Equity[] FakeEquities()
        {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(FAKE_EQUITIES);
            using StreamReader reader = new(stream);
            return EQUITY.ParseArray(reader.ReadToEnd()).Select(Equity.Copy).ToArray();
        }
    }
}
