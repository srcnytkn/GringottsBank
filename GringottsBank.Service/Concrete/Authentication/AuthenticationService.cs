using GringottsBank.Model.DTO;
using GringottsBank.Service.Abstract;
using GringottsBank.Service.Helper;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace GringottsBank.Service.Concrete.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtSettings _jwtSettings;
        public AuthenticationService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public async Task<ServiceResponse<GenerateTokenResponse>> GenerateToken(GenerateTokenRequest generateTokenRequest)
        {
            var generateTokenResponse = new GenerateTokenResponse();

            if (IsValidUser(generateTokenRequest))
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, generateTokenRequest.Username),
                    // Add additional claims as needed
                };

                var token = new JwtSecurityToken(
                    _jwtSettings.Issuer,
                    _jwtSettings.Audience,
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtSettings.ExpirationSeconds)),
                    signingCredentials: credentials
                );

                generateTokenResponse.Token = new JwtSecurityTokenHandler().WriteToken(token);
            }

            return ServiceResponse<GenerateTokenResponse>.CreateSuccess(generateTokenResponse);


        }
        private bool IsValidUser(GenerateTokenRequest generateTokenRequest)
        {
            return generateTokenRequest.Username == "testuser" && generateTokenRequest.Password == "testpassword";
        }
    }
}
