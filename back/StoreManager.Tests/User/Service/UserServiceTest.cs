using Moq;
using StoreManager.Infrastructure.User.DTO;
using StoreManager.Infrastructure.User.Model;
using StoreManager.Infrastructure.User.Repository;
using StoreManager.Infrastructure.User.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.Tests.User.Service
{
    public sealed class UserServiceTest : IAsyncLifetime
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private Mock<IUserRepository> _userRepositoryMock;
        private UserService _service;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        private static readonly string VALID_USERNAME = "TEST";
        private static readonly UserModel VALID_USER = new(1, "TEST", "TEST", "TEST", "TEST", UserRole.ADMIN);
        private static readonly UserCreateRequestDTO VALID_CREATE_REQUEST = new(VALID_USERNAME, "TEST", "TEST", "TEST", UserRole.ADMIN.ToString());

        [Fact(DisplayName = "Create user test")]
        public async Task CreateUser_Test()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                await _service.CreateUser(VALID_CREATE_REQUEST);
            });
            Assert.Null(exception);

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
            _service = new UserService(_userRepositoryMock.Object);

            MockRepository();

            await Task.CompletedTask;
        }

        private void MockRepository()
        {
            _userRepositoryMock.Setup(repo => repo.FindByUsername(VALID_USERNAME)).ReturnsAsync(VALID_USER);
        }
    }
}
