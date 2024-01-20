using GringottsBank.Model.DTO;
using GringottsBank.Service.Abstract;
using Microsoft.Extensions.Caching.Memory;

namespace GringottsBank.Service.Helper
{
    public class CacheHelper : ICacheHelper
    {
        private static MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        private static MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
        };
        public CacheHelper()
        {

        }
        public async Task AddCustomerToCache(CustomerDTO customerDTO)
        {
            _cache.Set(customerDTO.Id + "_customer", customerDTO, cacheEntryOptions);
            _cache.Set(customerDTO.Tckn + "_customer", customerDTO, cacheEntryOptions);
        }
        public async Task<object> GetCustomerWithIdFromCache(string customerId)
        {
            return _cache.Get(customerId + "_customer");
        }
        public async Task<object> GetCustomerWithTcknFromCache(string tckn)
        {
            return _cache.Get(tckn + "_customer");
        }
        public async Task AddAccountToCache(AccountDTO accountDTO)
        {
            _cache.Set(accountDTO.AccountNumber + "_account", accountDTO, cacheEntryOptions);
            await addCustomerAccountList(accountDTO.CustomerId, accountDTO);
        }
        private async Task addCustomerAccountList(string customerId, AccountDTO account)
        {
            if (!_cache.TryGetValue(customerId + "_customerAccounts", out List<AccountDTO> accounts))
            {
                accounts = new List<AccountDTO>();
            }

            accounts.Add(account);
            _cache.Set(customerId + "_customerAccounts", accounts, cacheEntryOptions);
        }
        public async Task<AccountDTO> GetAccountFromCache(string accountNumber)
        {
            return _cache.Get(accountNumber + "_account") as AccountDTO;
        }
        public async Task UpdateAccountToCache(AccountDTO accountDTO, bool isWithdraw, decimal amount)
        {
            _cache.Set(accountDTO.AccountNumber + "_account", accountDTO, cacheEntryOptions);
            var transcation = new AccountTransactionDTO
            {
                AccountNumber = accountDTO.AccountNumber,
                Amount = isWithdraw ? amount * -1 : amount,
                TransactionDate = DateTime.Now
            };
            await addTransactionToCache(accountDTO.AccountNumber, transcation);
        }
        private async Task addTransactionToCache(string accountNumber, AccountTransactionDTO transaction)
        {
            if (!_cache.TryGetValue(accountNumber + "_transaction", out List<AccountTransactionDTO> transactions))
            {
                transactions = new List<AccountTransactionDTO>();
            }

            transactions.Add(transaction);
            _cache.Set(accountNumber + "_transaction", transactions, cacheEntryOptions);
        }
        public async Task<List<AccountTransactionDTO>> GetAccountTransactionsFromCache(string accountNumber)
        {
            return _cache.TryGetValue(accountNumber + "_transaction", out List<AccountTransactionDTO> transactions)
                ? transactions
                : new List<AccountTransactionDTO>();
        }
        public async Task<List<AccountDTO>> GetCustomerAccountsFromCache(string customerId)
        {
            return _cache.TryGetValue(customerId + "_customerAccounts", out List<AccountDTO> accounts)
                ? accounts
                : new List<AccountDTO>();
        }
    }
}
