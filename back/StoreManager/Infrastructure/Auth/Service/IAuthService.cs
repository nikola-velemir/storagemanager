using StoreManager.Infrastructure.Auth.DTO;

namespace StoreManager.Infrastructure.Auth.Service
{
    public interface IAuthService
    {

        Task<LoginResponseDTO?> Authenticate(LoginRequestDTO request);
        Task<LoginResponseDTO?> RefreshAuthentication(RefreshRequestDTO request);
        Task DeAuthenticate(string authHeader);
    }
}
