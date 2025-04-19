using StoreManager.Domain.Auth.Tokens.RefreshToken.Model;
using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Application.Auth.Tokens.RefreshToken
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenModel> CreateAsync(UserModel user);

        Task<RefreshTokenModel?> FindRefreshTokenAsync(string token);
    }
}
