using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Generator;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Model;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Repository
{
    public class RefreshTokenRespository : IRefreshTokenRepository
    {
        private readonly WarehouseDbContext _context;
        private readonly DbSet<RefreshTokenModel> _refreshTokens;

        private readonly IRefreshTokenGenerator _refreshTokenGenerator;

        private readonly IConfiguration _config;

        public RefreshTokenRespository(WarehouseDbContext context, IRefreshTokenGenerator refreshTokenGenerator, IConfiguration config)
        {
            _context = context;
            _config = config;
            _refreshTokens = _context.RefreshTokens;
            _refreshTokenGenerator = refreshTokenGenerator;
        }

        public async Task<RefreshTokenModel> Create(UserModel user)
        {
            var token = new RefreshTokenModel
            {
                ExpiresOnUtc = DateTime.UtcNow.AddDays(_config.GetValue<int>("RefreshTokenSettings:ExpiryIntervalInDays")),
                Token = _refreshTokenGenerator.GenerateRefreshToken(),
                Id = Guid.NewGuid(),
                UserId = user.Id,
                User = user
            };

            _refreshTokens.Add(token);
            await _context.SaveChangesAsync();

            return token;


        }

        public async Task<RefreshTokenModel?> FindRefreshToken(string token)
        {
            return await _refreshTokens.Include(r => r.User).FirstOrDefaultAsync(r => r.Token == token);

        }
    }
}
