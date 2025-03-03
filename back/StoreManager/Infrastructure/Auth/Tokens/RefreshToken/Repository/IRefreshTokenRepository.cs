using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Model;
using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Repository
{
    public interface IRefreshTokenRepository
    {
        RefreshTokenModel Create(UserModel user);

        RefreshTokenModel? FindRefreshToken(string token);
    }
}
