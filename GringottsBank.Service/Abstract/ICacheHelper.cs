using GringottsBank.Model.DTO;

namespace GringottsBank.Service.Abstract
{
    public interface ICacheHelper
    {
        Task AddCustomerToCache(CustomerDTO customerDTO);
        Task<object> GetCustomerWithIdFromCache(string customerId);
        Task<object> GetCustomerWithTcknFromCache(string tckn);
        Task AddAccountToCache(AccountDTO accountDTO);
        Task<AccountDTO> GetAccountFromCache(string accountNumber);
        Task UpdateAccountToCache(AccountDTO accountDTO, bool isWithdraw, decimal amount);
        Task<List<AccountTransactionDTO>> GetAccountTransactionsFromCache(string accountNumber);
        Task<List<AccountDTO>> GetCustomerAccountsFromCache(string customerId);
    }
}
