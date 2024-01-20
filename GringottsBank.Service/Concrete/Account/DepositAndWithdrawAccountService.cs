using GringottsBank.Model.DTO;
using GringottsBank.Service.Abstract;
using Microsoft.Extensions.Caching.Memory;
using System.Transactions;

namespace GringottsBank.Service.Concrete.Account
{
    public partial class AccountService : IAccountService
    {
        public async Task<ServiceResponse<AccountDTO>> DepositAndWithdraw(DepositAndWithdrawDTO depositAndWithdrawDTO)
        {
            var validationError = ValidateDepositAndWithdrawDTO(depositAndWithdrawDTO);
            if (validationError != null)
            {
                return validationError;
            }

            var account = await _cacheHelper.GetAccountFromCache(depositAndWithdrawDTO.AccountNumber);
            if (account == null)
            {
                return ServiceResponse<AccountDTO>.CreateError("Account not found.");
            }
            
            var isWithdraw = depositAndWithdrawDTO.TransactionWay == TransactionWay.Withdraw;
            if(isWithdraw)
            {
                if(depositAndWithdrawDTO.Amount > account.Balance)
                {
                    return ServiceResponse<AccountDTO>.CreateError("Not enough balance.");
                }
                account.Balance -= depositAndWithdrawDTO.Amount;
                await _cacheHelper.UpdateAccountToCache(account, true, depositAndWithdrawDTO.Amount);
            }
            else
            {
                account.Balance += depositAndWithdrawDTO.Amount;
                await _cacheHelper.UpdateAccountToCache(account, false, depositAndWithdrawDTO.Amount);
            }

            return ServiceResponse<AccountDTO>.CreateSuccess(account);
        }

        private ServiceResponse<AccountDTO> ValidateDepositAndWithdrawDTO(DepositAndWithdrawDTO depositAndWithdrawDTO)
        {
            if (depositAndWithdrawDTO == null)
            {
                return ServiceResponse<AccountDTO>.CreateError("DepositAndWithdrawDTO must be filled.");
            }

            if (string.IsNullOrEmpty(depositAndWithdrawDTO.AccountNumber))
            {
                return ServiceResponse<AccountDTO>.CreateError("Account number must be filled.");
            }

            if (depositAndWithdrawDTO.Amount <= 0)
            {
                return ServiceResponse<AccountDTO>.CreateError("Amount must be bigger than 0.");
            }

            return null; // No validation error
        }
    }
}
