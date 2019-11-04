using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class TransferMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;

        public TransferMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var from = this.accountRepository.GetAccountById(fromAccountId);
            var to = this.accountRepository.GetAccountById(toAccountId);


            // Cover the checks for the "from" account
            if (!from.CanWithdrawAmount(amount))
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }

            if (from.WillLeaveLowFunds(amount))
            {
                this.notificationService.NotifyFundsLow(from.User.Email);
            }

            // Cover the checks for the "to" account
            if (to.HasReachedPayInLimit(amount))
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }

            if (to.IsApproachingPayInLimit(amount))
            {
                this.notificationService.NotifyApproachingPayInLimit(to.User.Email);
            }

            // If we are here, we can update the balance, withdrawn and paid in amounts of the accounts
            from.WithdrawFunds(amount);
            to.AcceptFunds(amount);
            
            this.accountRepository.Update(from);
            this.accountRepository.Update(to);            
        }
    }
}
