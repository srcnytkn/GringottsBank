using GringottsBank.Model.DTO;
using GringottsBank.Service.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IAuthenticationService = GringottsBank.Service.Abstract.IAuthenticationService;

namespace GringottsBank.Api.Controllers
{
    /// <summary>
    /// Authentication Controller
    /// </summary>
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILoggerHelper _logger;
        public AuthenticationController(IAuthenticationService authenticationService,
                                        ILoggerHelper logger)
        {
            _authenticationService = authenticationService;
            _logger = logger;   
        }

        /// <summary>
        /// Generates jwt token
        /// </summary>
        /// <param name="generateTokenRequest"></param>
        /// <returns></returns>
        [HttpPost("GenerateToken")]
        public async Task<IActionResult> GenerateToken([FromBody] GenerateTokenRequest generateTokenRequest)
        {
            try
            {
                var result = await _authenticationService.GenerateToken(generateTokenRequest);
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return Unauthorized(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ServiceResponse<CustomerDTO>.CreateError("Failed to create token."));

            }
        }
    }
}
