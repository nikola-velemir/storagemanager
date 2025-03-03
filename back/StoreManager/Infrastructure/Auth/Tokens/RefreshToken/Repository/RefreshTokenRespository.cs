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

        public RefreshTokenRespository(WarehouseDbContext context,IRefreshTokenGenerator refreshTokenGenerator)
        {
            _context = context;
            _refreshTokens = _context.RefreshTokens;
            _refreshTokenGenerator = refreshTokenGenerator;
        }

        public RefreshTokenModel Create(UserModel user)
        {
            var token = new RefreshTokenModel
            {
                ExpiresOnUtc = DateTime.UtcNow.AddDays(1),
                Token = _refreshTokenGenerator.GenerateRefreshToken(),
                Id = Guid.NewGuid(),
                UserId = user.Id,
                User = user
            };

            _refreshTokens.Add(token);
            _context.SaveChanges();

            return token;
        }

        public RefreshTokenModel? FindRefreshToken(string token)
        {
           return _refreshTokens.Include(r => r.User).FirstOrDefault(r => r.Token == token);

        }
    }
}
