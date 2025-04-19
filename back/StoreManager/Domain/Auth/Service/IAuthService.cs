using StoreManager.Application.Auth.DTO;

namespace StoreManager.Domain.Auth.Service
{
    public interface IAuthService
    {

        Task<LoginResponseDto?> Authenticate(LoginRequestDto request);
        Task<LoginResponseDto?> RefreshAuthentication(RefreshRequestDto request);
        Task DeAuthenticate(string authHeader);
    }
}
