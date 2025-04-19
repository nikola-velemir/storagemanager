using StoreManager.Domain.Auth.Tokens.RefreshToken.Model;
using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Application.Auth.Tokens.RefreshToken
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenModel> Create(UserModel user);

        Task<RefreshTokenModel?> FindRefreshToken(string token);
    }
}
