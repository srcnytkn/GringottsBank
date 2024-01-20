using GringottsBank.Model.DTO;
using GringottsBank.Service.Abstract;
using Microsoft.Extensions.Logging.Abstractions;

namespace GringottsBank.Service.Concrete.Account
{
    public partial class AccountService : IAccountService
    {
        public async Task<ServiceResponse<List<AccountTransactionDTO>>> ListCustomerTransactions(CustomerTransactionsRequestDTO customerTransactionsRequestDTO)
        {
            var transactionList = new List<AccountTransactionDTO>();

            var validationError = ValidateListCustomerTransactionsRequest(customerTransactionsRequestDTO);
            if (validationError != null)
            {
                return validationError;
            }

            var customer = await _cacheHelper.GetCustomerWithIdFromCache(customerTransactionsRequestDTO.CustomerId);
            if (customer == null)
            {
                return ServiceResponse<List<AccountTransactionDTO>>.CreateError("Customer is not exist.");
            }

            var accounts = await _cacheHelper.GetCustomerAccountsFromCache(customerTransactionsRequestDTO.CustomerId);
            if (accounts == null || !accounts.Any())
            {
                return ServiceResponse<List<AccountTransactionDTO>>.CreateError("Account not found.");
            }

            foreach (var account in accounts)
            {
                var accountTransactions = await _cacheHelper.GetAccountTransactionsFromCache(account.AccountNumber);
                if(accountTransactions != null && accountTransactions.Any())
                {
                    accountTransactions.RemoveAll(x => x.TransactionDate < customerTransactionsRequestDTO.StartDate || x.TransactionDate > customerTransactionsRequestDTO.EndDate);
                    transactionList.AddRange(accountTransactions);
                }
            }
            if(transactionList != null && transactionList.Any())
            {
                transactionList = transactionList.OrderByDescending(x => x.TransactionDate).ToList();
            }

            return ServiceResponse<List<AccountTransactionDTO>>.CreateSuccess(transactionList);
        }
        private ServiceResponse<List<AccountTransactionDTO>> ValidateListCustomerTransactionsRequest(CustomerTransactionsRequestDTO customerTransactionsRequestDTO)
        {
            if (string.IsNullOrEmpty(customerTransactionsRequestDTO?.CustomerId))
            {
                return ServiceResponse<List<AccountTransactionDTO>>.CreateError("Customer id must be filled.");
            }

            if(customerTransactionsRequestDTO.StartDate == DateTime.MinValue ||
                customerTransactionsRequestDTO.EndDate == DateTime.MinValue)
            {
                return ServiceResponse<List<AccountTransactionDTO>>.CreateError("Date range must be filled.");
            }

            if (customerTransactionsRequestDTO.StartDate > customerTransactionsRequestDTO.EndDate)
            {
                return ServiceResponse<List<AccountTransactionDTO>>.CreateError("Start date can not be later than the end date.");
            }

            return null; // No validation error
        }
    }
}
