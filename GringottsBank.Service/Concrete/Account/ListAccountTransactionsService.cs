using GringottsBank.Model.DTO;
using GringottsBank.Service.Abstract;
using Microsoft.Extensions.Caching.Memory;
using System.Transactions;

namespace GringottsBank.Service.Concrete.Account
{
    public partial class AccountService : IAccountService
    {
        public async Task<ServiceResponse<List<AccountTransactionDTO>>> ListAccountTransactions(AccountDTO accountDTO)
        {
            var transactionList = new List<AccountTransactionDTO>();

            if (string.IsNullOrEmpty(accountDTO?.AccountNumber))
            {
                return ServiceResponse<List<AccountTransactionDTO>>.CreateError("Account number must be filled.");
            }

            var account = await _cacheHelper.GetAccountFromCache(accountDTO.AccountNumber);
            if (account == null)
            {
                return ServiceResponse<List<AccountTransactionDTO>>.CreateError("Account not found.");
            }

            var accountTransactions = await _cacheHelper.GetAccountTransactionsFromCache(accountDTO.AccountNumber);

            if (accountTransactions != null && accountTransactions.Any())
            {
                accountTransactions = accountTransactions.OrderByDescending(x => x.TransactionDate).ToList();
            }


            return ServiceResponse<List<AccountTransactionDTO>>.CreateSuccess(accountTransactions);
        }
    }
}
