using GringottsBank.Model.DTO;

namespace GringottsBank.Service.Abstract
{
    public interface ICustomerService
    {
        Task<ServiceResponse<CustomerDTO>> CreateCustomer(CustomerDTO customerDTO);
    }
}
