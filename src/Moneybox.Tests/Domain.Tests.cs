
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moneybox.App;
using System;

namespace Moneybox.Tests
{
    [TestClass]
    public class DomainTests
    {
        [TestMethod]
        public void CheckInitialBalanceIsZero()
        {
            Account from = new Account();            
            Assert.IsTrue(from.Balance == 0m);
        }

        [TestMethod]
        public void CanAcceptMoney()
        {
            Account account = new Account();
            account.AcceptFunds(400m);
            Assert.IsTrue(account.Balance == 400m);
        }

        [TestMethod]
        public void CanWithdrawFunds()
        {
            Account account = new Account();
            account.AcceptFunds(400m);
            account.WithdrawFunds(300m);
            Assert.IsTrue(account.Balance == 100m);
        }

        [TestMethod]
        public void CanTriggerLowFundsAlert()
        {
            Account account = new Account();
            account.AcceptFunds(account.LowFundsAmount);            
            Assert.IsTrue(account.WillLeaveLowFunds(10m));
        }

        public void CanNotGoOverdrawn()
        {
            Account account = new Account();
            account.AcceptFunds(100m);
            Assert.IsFalse(account.CanWithdrawAmount(200m));
        }

        [TestMethod]
        public void CanReachPayInLimit()
        {
            Account account = new Account();
            account.AcceptFunds(account.PayInLimit);
            Assert.IsTrue(account.HasReachedPayInLimit(10m));
        }

        [TestMethod]
        public void CanWarnAboutPayInLimit()
        {
            Account account = new Account();
            account.AcceptFunds(account.PayInLimit - (account.LowFundsAmount + 10m));
            Assert.IsTrue(account.IsApproachingPayInLimit(20m));
        }        
    }
}
