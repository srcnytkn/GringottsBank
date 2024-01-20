using GringottsBank.Model.DTO;

namespace GringottsBank.Service.Abstract
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse<GenerateTokenResponse>> GenerateToken(GenerateTokenRequest generateTokenRequest);
    }
}
