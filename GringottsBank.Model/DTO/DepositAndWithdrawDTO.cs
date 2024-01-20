using System;

namespace GringottsBank.Model.DTO
{
    public class DepositAndWithdrawDTO
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public TransactionWay TransactionWay { get; set; }
    }
    public enum TransactionWay
    {
        Deposit,
        Withdraw
    }
}
