using GringottsBank.Model.DTO;
using GringottsBank.Service.Abstract;
using Microsoft.Extensions.Caching.Memory;
using System.Text;
using System;

namespace GringottsBank.Service.Concrete.Account
{
    public partial class AccountService : IAccountService
    {
        private static readonly Random random = new Random();
        public async Task<ServiceResponse<AccountDTO>> CreateAccount(AccountDTO accountDTO)
        {
            var validationError = ValidateAccountDTO(accountDTO);
            if (validationError != null)
            {
                return validationError;
            }

            var customer = await _cacheHelper.GetCustomerWithIdFromCache(accountDTO.CustomerId);
            if (customer == null)
            {
                return ServiceResponse<AccountDTO>.CreateError("Customer is not exist.");
            }

            accountDTO.AccountNumber = GenerateAccountNumber();
            accountDTO.Balance = 0;
            accountDTO.CreatedDate = DateTime.Now;

            await _cacheHelper.AddAccountToCache(accountDTO);

            return ServiceResponse<AccountDTO>.CreateSuccess(accountDTO);
        }
        private ServiceResponse<AccountDTO> ValidateAccountDTO(AccountDTO accountDTO)
        {
            if (accountDTO == null)
            {
                return ServiceResponse<AccountDTO>.CreateError("Account data must be filled.");
            }

            if (string.IsNullOrEmpty(accountDTO.CustomerId))
            {
                return ServiceResponse<AccountDTO>.CreateError("Customer id must be filled.");
            }

            return null; // No validation error
        }
        public static string GenerateAccountNumber()
        {
            const string digits = "0123456789";
            int length = 17;

            // Use StringBuilder for better performance in concatenation
            StringBuilder accountNumberBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(digits.Length);
                accountNumberBuilder.Append(digits[index]);
            }

            return accountNumberBuilder.ToString();
        }
    }
}
