using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.User.Model;
using StoreManager.Infrastructure.User.Repository;

namespace StoreManager.Tests.User.Repository
{
    public sealed class UserRepositoryTest : IAsyncLifetime
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private WarehouseDbContext _context;
        private UserRepository _repository;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        private readonly string VALID_USERNAME = "testuser";
        private readonly string INVALID_USERNAME = "invalid";
        private readonly string VALID_CREATION_USERNAME = "test2";


        [Fact(DisplayName = "Find User - Valid username")]
        public async Task FindByUserName_ValidUsernameTest()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                var user = await _repository.FindByUsername(VALID_USERNAME);
                Assert.NotNull(user);

            });
            Assert.Null(exception);
        }
        [Fact(DisplayName = "Find User - Invalid username")]
        public async Task FindByUserName_InvalidUsernameTest()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                var user = await _repository.FindByUsername(INVALID_USERNAME);

            });
            Assert.NotNull(exception);
            Assert.IsType<InvalidOperationException>(exception);
        }

        [Fact(DisplayName = "Create user")]
        public async Task Create_Test()
        {
            UserModel user = new UserModel(2, VALID_CREATION_USERNAME, "test2", "test2", "test2", UserRole.MANAGER);
            await _repository.Create(user);

            UserModel fetchUser = await _repository.FindByUsername(VALID_CREATION_USERNAME);
            Assert.Equal(user, fetchUser);
        }

        public async Task InitializeAsync()
        {
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new WarehouseDbContext(options);
            await SeedTestData();
        }

        private async Task SeedTestData()
        {
            _context.Users.Add(new UserModel(1, VALID_USERNAME, "testuser", "Test", "Test", UserRole.ADMIN));
            await _context.SaveChangesAsync();
            _repository = new UserRepository(_context);
        }

        public async Task DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
