using GringottsBank.Model.DTO;
using GringottsBank.Service.Abstract;
using GringottsBank.Service.Helper;
using Microsoft.Extensions.Caching.Memory;

namespace GringottsBank.Service.Concrete.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly ICacheHelper _cacheHelper;
        public CustomerService(ICacheHelper cacheHelper)
        {
            _cacheHelper = cacheHelper;
        }
        public async Task<ServiceResponse<CustomerDTO>> CreateCustomer(CustomerDTO customerDTO)
        {
            // Validate customerDTO
            var validationError = ValidateCustomerDTO(customerDTO);
            if (validationError != null)
            {
                return validationError;
            }

            var customer = await _cacheHelper.GetCustomerWithTcknFromCache(customerDTO.Tckn);
            if (customer != null)
            {
                return ServiceResponse<CustomerDTO>.CreateError("Customer is already exist.");
            }
            // Assume customer ID is generated here (replace with actual logic)
            customerDTO.Id = Guid.NewGuid().ToString();

            // Add the customer to the cache (database would be better but this one is easier)
            await _cacheHelper.AddCustomerToCache(customerDTO);

            return ServiceResponse<CustomerDTO>.CreateSuccess(customerDTO);
        }
        private ServiceResponse<CustomerDTO> ValidateCustomerDTO(CustomerDTO customerDTO)
        {
            if (customerDTO == null)
            {
                return ServiceResponse<CustomerDTO>.CreateError("Customer data must be filled.");
            }

            if (string.IsNullOrEmpty(customerDTO.Tckn) || customerDTO.Tckn.Length != 11)
            {
                return ServiceResponse<CustomerDTO>.CreateError("Tckn must be exactly 11 characters.");
            }

            if (string.IsNullOrEmpty(customerDTO.Name) || string.IsNullOrEmpty(customerDTO.Surname))
            {
                return ServiceResponse<CustomerDTO>.CreateError("Name and surname must be filled.");
            }

            return null; // No validation error
        }
    }
}
