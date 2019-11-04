using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;

        public WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            var from = this.accountRepository.GetAccountById(fromAccountId);

            // Check the account has sufficient funds
            if (!from.CanWithdrawAmount(amount))
            {
                throw new InvalidOperationException("Insufficient funds to make withdrawal");
            }

            if (from.WillLeaveLowFunds(amount))
            {
                this.notificationService.NotifyFundsLow(from.User.Email);
            }

            // If we got here without throwing an exception we can make the withdrawal
            from.WithdrawFunds(amount);

            this.accountRepository.Update(from);

        }
    }
}
