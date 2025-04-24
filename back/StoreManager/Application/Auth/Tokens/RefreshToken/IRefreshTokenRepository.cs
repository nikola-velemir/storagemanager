using StoreManager.Domain.Auth.Tokens.RefreshToken.Model;
using StoreManager.Domain.User.Model;
using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Application.Auth.Tokens.RefreshToken
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenModel> CreateAsync(Domain.User.Model.User user);

        Task<RefreshTokenModel?> FindRefreshTokenAsync(string token);
    }
}
