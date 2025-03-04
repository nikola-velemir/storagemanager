using StoreManager.Infrastructure.Auth.DTO;

namespace StoreManager.Infrastructure.Auth.Service
{
    public interface IAuthService
    {

        LoginResponseDTO? Authenticate(LoginRequestDTO request);
        LoginResponseDTO? RefreshAuthentication(RefreshRequestDTO request);
        Task DeAuthenticate(string authHeader);
    }
}
