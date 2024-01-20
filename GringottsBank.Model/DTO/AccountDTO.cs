using System;

namespace GringottsBank.Model.DTO
{
    public class AccountDTO
    {
        public string AccountNumber { get; set; }
        public string CustomerId { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
