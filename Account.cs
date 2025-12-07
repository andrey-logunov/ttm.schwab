using System.Collections.Generic;
using System.Linq;
using System.Text;
using ttm.schwab.api;
using ttm.schwab.api.enumerations;

namespace ttm.schwab
{
    public sealed class Account
    {
        public AccountType AccountType { get; private set; }
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

        private Position[] _positions;
        public int PositionCount { get { return _positions != null ? _positions.Length : 0; } }
        
        public Position GetPosition(int index)
        {
            if (index >= 0 && index < _positions.Length)
            {
                return _positions[index];
            }
            return null;
        }

        public IEnumerable<Position> Positions
        {
            get {
                if (_positions != null)
                {
                    foreach (Position position in _positions)
                    {
                        yield return position;
                    }
                }
            }
        }

        private Account() { }

        internal static Account Copy(ACCOUNT account)
        {
            return new Account
            {
                AccountType = TRANSLATE.Translate(account.AccountType),
                AccountNumber = account.AccountNumber,

                RoundTrips = account.RoundTrips,
                IsDayTrader = account.IsDayTrader,
                IsClosingOnlyRestricted = account.IsClosingOnlyRestricted,
                PfcbFlag = account.PfcbFlag,

                InitialBalance = account.InitialBalance,
                CurrentBalance = account.CurrentBalance,

                LongStockValue = account.LongStockValue,
                ShortStockValue = account.ShortStockValue,
                LongOptionsValue = account.LongOptionsValue,
                ShortOptionsValue = account.ShortOptionsValue,

                CashBalance = account.CashBalance,
                CashSweep = account.CashSweep,
                CashUnsettled = account.CashUnsettled,

                PendingDeposits = account.PendingDeposits,
                NonMarginableFunds = account.NonMarginableFunds,

                AvailableForTrading = account.AvailableForTrading,
                AvailableForWithdrawal = account.AvailableForWithdrawal,

                AvailableForTradingProjected = account.AvailableForTradingProjected,
                AvailableForWithdrawalProjected = account.AvailableForWithdrawalProjected,

                _positions = account.Positions.Select(p => Position.Copy(p)).ToArray(),
            };
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("Account");
            sb.AppendLine();
            sb.AppendLine($"Account type:                        {CONVERT.ToString(TRANSLATE.Translate(AccountType)),10}");
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

            foreach (Position position in Positions)
            {
                sb.AppendLine($"{position}");
            }
            return sb.ToString();
        }
    }
}
