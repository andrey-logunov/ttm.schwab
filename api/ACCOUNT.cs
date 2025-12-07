using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using ttm.schwab.api.enumerations;

namespace ttm.schwab.api
{
    internal sealed class ACCOUNT
    {
        public ACCOUNT_TYPE AccountType { get; private set; }
        public string AccountNumber { get; private set; }

        public int RoundTrips { get; private set; }
        public bool IsDayTrader { get; private set; }
        public bool IsClosingOnlyRestricted { get; private set; }
        public bool PfcbFlag { get; private set; }

        public double InitialBalance { get; private set; }
        public double CurrentBalance { get; private set; }

        public double LongStockValue { get; private set; }
        public double ShortStockValue { get; private set; }
        public double LongOptionsValue { get; private set; }
        public double ShortOptionsValue { get; private set; }

        public double CashBalance { get; private set; }
        public double CashSweep { get; private set; }
        public double CashUnsettled { get; private set; }

        public double PendingDeposits { get; private set; }
        public double NonMarginableFunds { get; private set; }

        public double AvailableForTrading { get; private set; }
        public double AvailableForWithdrawal { get; private set; }

        public double AvailableForTradingProjected { get; private set; }
        public double AvailableForWithdrawalProjected { get; private set; }

        private POSITION[] _positions;
        public IEnumerable<POSITION> Positions { get { foreach (POSITION position in _positions) yield return position; } }

        private ACCOUNT() { }

        public static ACCOUNT Parse(JToken jtoken)
        {
            if (jtoken == null || jtoken["securitiesAccount"] == null) return null;

            return new ACCOUNT
            {
                AccountType = CONVERT.FromString(Convert.ToString(jtoken["securitiesAccount"]["type"]), out ACCOUNT_TYPE accountType) ? accountType : ACCOUNT_TYPE.UNKNOWN,
                AccountNumber = Convert.ToString(jtoken["securitiesAccount"]["accountNumber"]),

                RoundTrips = Convert.ToInt32(jtoken["securitiesAccount"]["roundTrips"]),
                IsDayTrader = Convert.ToBoolean(jtoken["securitiesAccount"]["isDayTrader"]),
                IsClosingOnlyRestricted = Convert.ToBoolean(jtoken["securitiesAccount"]["isClosingOnlyRestricted"]),
                PfcbFlag = Convert.ToBoolean(jtoken["securitiesAccount"]["pfcbFlag"]),

                InitialBalance = Convert.ToDouble(jtoken["securitiesAccount"]["initialBalances"]["liquidationValue"]),
                CurrentBalance = Convert.ToDouble(jtoken["securitiesAccount"]["currentBalances"]["liquidationValue"]),

                LongStockValue = Convert.ToDouble(jtoken["securitiesAccount"]["currentBalances"]["longMarketValue"]),
                ShortStockValue = Convert.ToDouble(jtoken["securitiesAccount"]["currentBalances"]["shortMarketValue"]),
                LongOptionsValue = Convert.ToDouble(jtoken["securitiesAccount"]["currentBalances"]["longOptionMarketValue"]),
                ShortOptionsValue = Convert.ToDouble(jtoken["securitiesAccount"]["currentBalances"]["shortOptionMarketValue"]),

                CashBalance = Convert.ToDouble(jtoken["securitiesAccount"]["currentBalances"]["cashBalance"]),
                CashSweep = Convert.ToDouble(jtoken["securitiesAccount"]["currentBalances"]["moneyMarketFund"]),
                CashUnsettled = Convert.ToDouble(jtoken["securitiesAccount"]["currentBalances"]["unsettledCash"]),

                PendingDeposits = Convert.ToDouble(jtoken["securitiesAccount"]["currentBalances"]["pendingDeposits"]),
                NonMarginableFunds = Convert.ToDouble(jtoken["securitiesAccount"]["currentBalances"]["longNonMarginableMarketValue"]),

                AvailableForTrading = Convert.ToDouble(jtoken["securitiesAccount"]["currentBalances"]["cashAvailableForTrading"]),
                AvailableForWithdrawal = Convert.ToDouble(jtoken["securitiesAccount"]["currentBalances"]["cashAvailableForWithdrawal"]),

                AvailableForTradingProjected = Convert.ToDouble(jtoken["securitiesAccount"]["projectedBalances"]["cashAvailableForTrading"]),
                AvailableForWithdrawalProjected = Convert.ToDouble(jtoken["securitiesAccount"]["projectedBalances"]["cashAvailableForWithdrawal"]),

                _positions = POSITION.ParseArray(jtoken["securitiesAccount"]["positions"]),
            };
        }

        public static ACCOUNT Parse(string json)
        {
            return Parse(JToken.Parse(json));
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("ACCOUNT");
            sb.AppendLine();
            sb.AppendLine($"Account type:                        {CONVERT.ToString(AccountType),10}");
            sb.AppendLine($"Account number:                      {AccountNumber,10}");
            sb.AppendLine();
            sb.AppendLine($"Round trips:                         {RoundTrips,10}");
            sb.AppendLine($"Day trader:                          {IsDayTrader,10}");
            sb.AppendLine($"Closing only restricted:             {IsClosingOnlyRestricted,10}");
            sb.AppendLine($"Pfcb flag:                           {PfcbFlag,10}");
            sb.AppendLine();
            sb.AppendLine($"Initial Balance:                     {InitialBalance,10:F2}");
            sb.AppendLine($"Current Balance:                     {CurrentBalance,10:F2}");
            sb.AppendLine();
            sb.AppendLine($"Long Stock Value:                    {LongStockValue,10:F2}");
            sb.AppendLine($"Short Stock Value:                   {ShortStockValue,10:F2}");
            sb.AppendLine($"Long Options Value:                  {LongOptionsValue,10:F2}");
            sb.AppendLine($"Short Options Value:                 {ShortOptionsValue,10:F2}");
            sb.AppendLine();
            sb.AppendLine($"Cash Balance:                        {CashBalance,10:F2}");
            sb.AppendLine($"Cash Sweep:                          {CashSweep,10:F2}");
            sb.AppendLine($"Cash Unsettled:                      {CashUnsettled,10:F2}");
            sb.AppendLine();
            sb.AppendLine($"Pending Deposits:                    {PendingDeposits,10:F2}");
            sb.AppendLine($"Non Marginable Funds:                {NonMarginableFunds,10:F2}");
            sb.AppendLine();
            sb.AppendLine($"Available For Trading:               {AvailableForTrading,10:F2}");
            sb.AppendLine($"Available For Withdrawal:            {AvailableForWithdrawal,10:F2}");
            sb.AppendLine();
            sb.AppendLine($"Available For Trading Projected:     {AvailableForTradingProjected,10:F2}");
            sb.AppendLine($"Available For Withdrawal Projected:  {AvailableForWithdrawalProjected,10:F2}");
            sb.AppendLine();

            foreach (POSITION position in Positions)
            {
                sb.AppendLine($"{position}");
            }
            return sb.ToString();
        }
    }
}
