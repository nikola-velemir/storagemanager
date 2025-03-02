using StoreManager.Infrastructure.Auth.DTO;

namespace StoreManager.Infrastructure.Auth.Service
{
    public interface ILoginService
    {

        LoginResponseDTO? Authenticate(string username, string password);
    }
}
