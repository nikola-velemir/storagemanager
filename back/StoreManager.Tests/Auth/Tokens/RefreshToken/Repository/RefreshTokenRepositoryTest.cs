using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Moq;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Generator;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Model;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Repository;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.User.Model;
using StoreManager.Infrastructure.User.Repository;
using System.Threading.Tasks;

namespace StoreManager.Tests.Auth.Tokens.RefreshToken.Repository
{
    public sealed class RefreshTokenRepositoryTest : IAsyncLifetime
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private WarehouseDbContext _context;
        private RefreshTokenRepository _repository;
        private IConfiguration _config;
        private Mock<IRefreshTokenGenerator> _generator;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        private static readonly UserModel USER = new(1, "test", "testuser", "Test", "Test", UserRole.ADMIN);
        private static readonly RefreshTokenModel EXISTING_TOKEN = new RefreshTokenModel { Token = "asdjasdas", User = USER, ExpiresOnUtc = DateTime.UtcNow.AddMinutes(30), Id = Guid.NewGuid(), UserId = USER.Id };
        [Fact(DisplayName = "Create refresh token")]
        public async Task Create_Test()
        {
            var token = await _repository.Create(USER);
            var foundToken = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == "hjdjklsahdjalskdhjkasdhjkasdhkas");
            Assert.NotNull(token);
            Assert.NotNull(foundToken);
            Assert.Equal(token, foundToken);
            _generator.Verify(g => g.GenerateRefreshToken(), Times.Once);
        }
        [Fact(DisplayName = "Find refresh token - Invalid token")]
        public async Task FindRefreshToken_InvalidTokenTest()
        {
            var token = await _repository.FindRefreshToken("asdjasdas");
            Assert.NotNull(token);
            Assert.Equal(EXISTING_TOKEN, token);
            Assert.NotNull(token.User);
            Assert.Equal(USER, token.User);
        }
        private async Task SeedTestData()
        {
            var user = await _context.Users.AddAsync(USER);
            await _context.SaveChangesAsync();

            await _context.RefreshTokens.AddAsync(EXISTING_TOKEN);

            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
        public async Task DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task InitializeAsync()
        {
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new WarehouseDbContext(options);
            await SeedTestData();

            var configValues = new Dictionary<string, string>
           {
                { "RefreshTokenSettings:ExpiryIntervalInDays", "5" },
                { "RefreshTokenSettings:Length", "32" }
            };
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            _config = new ConfigurationBuilder().Add(new MemoryConfigurationSource { InitialData = configValues }).Build();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            _generator = new Mock<IRefreshTokenGenerator>();

            _generator.Setup(g => g.GenerateRefreshToken()).Returns("hjdjklsahdjalskdhjkasdhjkasdhkas");


            _repository = new RefreshTokenRepository(_context, _generator.Object, _config);
        }
    }
}
