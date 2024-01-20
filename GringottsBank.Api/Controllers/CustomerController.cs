using GringottsBank.Model.DTO;
using GringottsBank.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GringottsBank.Api.Controllers
{
    /// <summary>
    /// Customer Controller
    /// </summary>
    [ApiController]
    [Route("api/customer")]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILoggerHelper _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customerService"></param>
        public CustomerController(ICustomerService customerService, ILoggerHelper logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new customer
        /// </summary>
        /// <param name="customerDTO"></param>
        /// <returns></returns>
        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDTO customerDTO)
        {
            try
            {
                var result = await _customerService.CreateCustomer(customerDTO);
                if(result.Success)
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
                return BadRequest(ServiceResponse<CustomerDTO>.CreateError("Failed to create customer."));

            }
        }
    }
}
