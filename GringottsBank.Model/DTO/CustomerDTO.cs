using System.ComponentModel.DataAnnotations;

namespace GringottsBank.Model.DTO
{
    public class CustomerDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Tckn { get; set; }
    }
}
