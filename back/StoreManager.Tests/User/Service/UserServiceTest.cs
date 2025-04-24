using Moq;
using StoreManager.Application.User.DTO;
using StoreManager.Application.User.Repository;
using StoreManager.Application.User.Service;
using StoreManager.Domain.User.Model;
using StoreManager.Infrastructure.User.Model;
using StoreManager.Infrastructure.User.Service;

namespace StoreManager.Tests.User.Service
{
    public sealed class UserServiceTest : IAsyncLifetime
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private Mock<IUserRepository> _userRepositoryMock;
        private UserService _service;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        private static readonly string VALID_USERNAME = "TEST";
        private static readonly  Domain.User.Model.User VALID_USER = new(1, "TEST", "TEST", "TEST", "TEST", UserRole.ADMIN);
        private static readonly UserCreateRequestDto VALID_CREATE_REQUEST = new(VALID_USERNAME, "TEST", "TEST", "TEST", UserRole.ADMIN.ToString());
        private static readonly UserCreateResponseDto VALID_CREATE_RESPONSE = new(VALID_USERNAME, "TEST", "TEST", "TEST", UserRole.ADMIN.ToString());

        [Fact(DisplayName = "Create user test")]
        public async Task CreateUser_Test()
        {
            var response = await _service.CreateUser(VALID_CREATE_REQUEST);
            Assert.Equal(VALID_CREATE_RESPONSE, response);
            _userRepositoryMock.Verify(repo => repo.CreateAsync(It.Is<Domain.User.Model.User>(u=>u.Equals(VALID_USER))), Times.Once);
        }

        public async Task DisposeAsync()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            _userRepositoryMock = null;
            _service = null;
            await Task.CompletedTask;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        }

        public async Task InitializeAsync()
        {
            _userRepositoryMock = new Mock<IUserRepository>();

            MockRepository();

            _service = new UserService(_userRepositoryMock.Object);
            await Task.CompletedTask;

        }

        private void MockRepository()
        {
            _userRepositoryMock.Setup(repo => repo.FindByUsernameAsync(VALID_USERNAME)).ReturnsAsync(VALID_USER);
            _userRepositoryMock.Setup(repo => repo.CreateAsync(It.Is<Domain.User.Model.User>(u=>u.Equals(VALID_USER)))).ReturnsAsync(VALID_USER);
        }
    }
}
