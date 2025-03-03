using StoreManager.Infrastructure.Auth.DTO;

namespace StoreManager.Infrastructure.Auth.Service
{
    public interface IAuthService
    {

        LoginResponseDTO? Authenticate(LoginRequestDTO request);
        public LoginResponseDTO? RefreshAuthentication(RefreshRequestDTO request);
    }
}
