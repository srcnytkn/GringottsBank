using System;

namespace GringottsBank.Model.DTO
{
    public class CustomerTransactionsRequestDTO
    {
        public string CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
