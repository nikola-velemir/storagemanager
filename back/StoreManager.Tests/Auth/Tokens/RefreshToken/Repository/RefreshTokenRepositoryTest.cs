using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Model;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Repository;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.User.Model;
using StoreManager.Infrastructure.User.Repository;

namespace StoreManager.Tests.Auth.Tokens.RefreshToken.Repository
{
    public sealed class RefreshTokenRepositoryTest : IAsyncLifetime
    {
        private WarehouseDbContext _context;
        private RefreshTokenRespository _repository;
        private async Task SeedTestData()
        {
            var user = await _context.Users.AddAsync(new UserModel(1, "test", "testuser", "Test", "Test", UserRole.ADMIN));
            await _context.SaveChangesAsync();

            await _context.RefreshTokens.AddAsync(new RefreshTokenModel { Token = "asdjasdas", User = user.Entity, ExpiresOnUtc = DateTime.UtcNow.AddMinutes(30), Id = Guid.NewGuid(), UserId = user.Entity.Id });

            await _context.SaveChangesAsync();
            _repository = new RefreshTokenRespository(_context);
        }
        public async Task DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public Task InitializeAsync()
        {
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new WarehouseDbContext(options);
            throw new NotImplementedException();
        }
    }
}
