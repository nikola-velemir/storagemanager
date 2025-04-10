using StoreManager.Infrastructure.Auth.DTO;

namespace StoreManager.Infrastructure.Auth.Service
{
    public interface IAuthService
    {

        Task<LoginResponseDto?> Authenticate(LoginRequestDto request);
        Task<LoginResponseDto?> RefreshAuthentication(RefreshRequestDto request);
        Task DeAuthenticate(string authHeader);
    }
}
