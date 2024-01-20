using GringottsBank.Model.DTO;
using GringottsBank.Service.Abstract;
using GringottsBank.Service.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GringottsBank.Api.Controllers
{
    /// <summary>
    /// Account Controller
    /// </summary>
    [ApiController]
    [Route("api/account")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILoggerHelper _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accountService"></param>
        public AccountController(IAccountService accountService, ILoggerHelper logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new account
        /// </summary>
        /// <param name="accountDTO"></param>
        /// <returns></returns>
        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountDTO accountDTO)
        {
            try
            {
                var result = await _accountService.CreateAccount(accountDTO);
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ServiceResponse<AccountDTO>.CreateError("Failed to create account."));

            }
        }

        /// <summary>
        /// Withdraw and deposit
        /// </summary>
        /// <param name="depositAndWithdrawDTO"></param>
        /// <returns></returns>
        [HttpPost("DepositAndWithdraw")]
        public async Task<IActionResult> DepositAndWithdraw([FromBody] DepositAndWithdrawDTO depositAndWithdrawDTO)
        {
            try
            {
                var result = await _accountService.DepositAndWithdraw(depositAndWithdrawDTO);
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ServiceResponse<AccountDTO>.CreateError("Failed transaction."));

            }
        }

        /// <summary>
        /// List account transactions
        /// </summary>
        /// <param name="accountDTO"></param>
        /// <returns></returns>
        [HttpPost("ListAccountTransactions")]
        public async Task<IActionResult> ListAccountTransactions([FromBody] AccountDTO accountDTO)
        {
            try
            {
                var result = await _accountService.ListAccountTransactions(accountDTO);
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ServiceResponse<AccountDTO>.CreateError("Failed to list transactions."));

            }
        }

        /// <summary>
        /// List customer all accounts transactions
        /// </summary>
        /// <param name="accountDTO"></param>
        /// <returns></returns>
        [HttpPost("ListCustomerTransactions")]
        public async Task<IActionResult> ListCustomerTransactions([FromBody] CustomerTransactionsRequestDTO customerTransactionsRequestDTO)
        {
            try
            {
                var result = await _accountService.ListCustomerTransactions(customerTransactionsRequestDTO);
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ServiceResponse<AccountDTO>.CreateError("Failed to list transactions."));

            }
        }
    }
}
