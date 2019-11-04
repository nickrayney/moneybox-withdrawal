using System;

namespace Moneybox.App
{
    public class Account
    {
        private readonly decimal _payInLimit = 4000m;
        private readonly decimal _lowFundsAmount = 500m;
        private decimal _paidIn;
        private decimal _balance;
        private decimal _withdrawn;

        public Guid Id { get; set; }
        public User User { get; set; }

        public decimal Balance { get => _balance; }
        public decimal Withdrawn { get => _withdrawn; }
        public decimal PaidIn { get => _paidIn; }

        public decimal PayInLimit => _payInLimit;
        public decimal LowFundsAmount => _lowFundsAmount;

        public bool CanWithdrawAmount(decimal amount)
        {
            return _balance > amount;
        }

        public bool WillLeaveLowFunds(decimal amount)
        {
            return _balance - amount < LowFundsAmount;
        }

        public bool HasReachedPayInLimit(decimal amount)
        {
            return _paidIn + amount > PayInLimit;
        }

        public bool IsApproachingPayInLimit(decimal amount)
        {
            return _paidIn + amount > _payInLimit - LowFundsAmount;
        }

        public void WithdrawFunds(decimal amount)
        {
            _balance -= amount;
            _withdrawn += amount;
        }

        public void AcceptFunds(decimal amount)
        {
            _balance += amount;
            _paidIn += amount;
        }
    }
}
