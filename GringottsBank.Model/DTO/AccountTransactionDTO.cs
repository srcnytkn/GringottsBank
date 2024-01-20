using System;

namespace GringottsBank.Model.DTO
{
    public class AccountTransactionDTO
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
