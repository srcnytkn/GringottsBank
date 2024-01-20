using GringottsBank.Model.DTO;

namespace GringottsBank.Service.Abstract
{
    public interface IAccountService
    {
        Task<ServiceResponse<AccountDTO>> CreateAccount(AccountDTO accountDTO);
        Task<ServiceResponse<AccountDTO>> DepositAndWithdraw(DepositAndWithdrawDTO depositAndWithdrawDTO);
        Task<ServiceResponse<List<AccountTransactionDTO>>> ListAccountTransactions(AccountDTO accountDTO);
        Task<ServiceResponse<List<AccountTransactionDTO>>> ListCustomerTransactions(CustomerTransactionsRequestDTO customerTransactionsRequestDTO);
    }
}
