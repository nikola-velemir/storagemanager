using Microsoft.EntityFrameworkCore;
using StoreManager.Application.Auth.Tokens.RefreshToken;
using StoreManager.Domain.Auth.Tokens.RefreshToken.Model;
using StoreManager.Infrastructure.Context;

namespace StoreManager.Infrastructure.Auth.Tokens.RefreshTokens
{
    public class RefreshTokenRepository(
        WarehouseDbContext context,
        IRefreshTokenGenerator refreshTokenGenerator,
        IConfiguration config)
        : IRefreshTokenRepository
    {
        private readonly DbSet<RefreshTokenModel> _refreshTokens = context.RefreshTokens;

        public Task<RefreshTokenModel> CreateAsync(Domain.User.Model.User user)
        {
            var token = new RefreshTokenModel
            {
                ExpiresOnUtc =
                    DateTime.UtcNow.AddDays(config.GetValue<int>("RefreshTokenSettings:ExpiryIntervalInDays")),
                Token = refreshTokenGenerator.GenerateRefreshToken(),
                Id = Guid.NewGuid(),
                UserId = user.Id,
                User = user
            };

            _refreshTokens.Add(token);

            return Task.FromResult(token);
        }

        public async Task<RefreshTokenModel?> FindRefreshTokenAsync(string token)
        {
            return await _refreshTokens.Include(r => r.User).FirstOrDefaultAsync(r => r.Token == token);
        }
    }
}