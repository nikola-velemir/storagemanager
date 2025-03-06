using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Model;
using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Repository
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenModel> Create(UserModel user);

        Task<RefreshTokenModel?> FindRefreshToken(string token);
    }
}
