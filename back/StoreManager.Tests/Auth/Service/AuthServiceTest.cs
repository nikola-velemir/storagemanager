using Microsoft.AspNetCore.Identity.Data;
using Moq;
using StoreManager.Infrastructure.Auth.DTO;
using StoreManager.Infrastructure.Auth.Service;
using StoreManager.Infrastructure.Auth.Tokens.AcessToken;
using StoreManager.Infrastructure.Auth.Tokens.RedisCache;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Model;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Repository;
using StoreManager.Infrastructure.User.Model;
using StoreManager.Infrastructure.User.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.Tests.Auth.Service
{
    public sealed class AuthServiceTest : IAsyncLifetime
    {

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private Mock<IAccessTokenGenerator> _tokenGenerator;
        private Mock<IUserRepository> _userRepository;
        private Mock<IRefreshTokenRepository> _refreshTokenRepository;
        private Mock<IRedisCacheService> _redis;
        private AuthService _service;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.



        private static readonly string VALID_USERNAME = "TEST";
        private static readonly string VALID_PASSWORD = "TEST";
        private static readonly string INVALID_USERNAME = "INVALID";
        private static readonly string INVALID_PASSWORD = "INVALID";

        private static readonly UserModel VALID_USER = new(1, VALID_USERNAME, VALID_PASSWORD, "TEST", "TEST", UserRole.ADMIN);
        private static readonly string VALID_JWT_TOKEN = "validToken";
        private static readonly string VALID_JTI = "aaa";
        private static readonly string VALID_REFRESH_TOKEN = "/ZvvyVtCy+4O5hH5HdgJcYAApL6D1LZpArL8GaO+E6Y=";
        private static readonly RefreshTokenModel VALID_REFRESH_TOKEN_MODEL = new RefreshTokenModel
        {
            Token = VALID_REFRESH_TOKEN,
            User = VALID_USER,
            ExpiresOnUtc = DateTime.Now.AddHours(4),
            Id = Guid.NewGuid(),
            UserId = VALID_USER.Id
        };
        private static readonly LoginResponseDTO VALID_RESPONSE = new LoginResponseDTO(VALID_JWT_TOKEN, VALID_REFRESH_TOKEN, UserRole.ADMIN.ToString());
        private static readonly LoginRequestDTO VALID_REQUEST = new LoginRequestDTO(VALID_USERNAME, VALID_PASSWORD);
        private static readonly LoginRequestDTO INVALID_REQUEST_INVALID_USERNAME = new LoginRequestDTO(INVALID_USERNAME, VALID_PASSWORD);
        private static readonly LoginRequestDTO INVALID_REQUEST_INVALID_PASSWORD = new LoginRequestDTO(VALID_USERNAME, INVALID_PASSWORD);

        [Fact(DisplayName = "Authenticate test - Invalid Username")]
        public async Task Authenticate_InvalidUsernameTest()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                await _service.Authenticate(INVALID_REQUEST_INVALID_USERNAME);
            });


            Assert.NotNull(exception);
            Assert.IsType<InvalidOperationException>(exception);
            Assert.Equal("Not found", exception.Message);
            _userRepository.Verify(repo => repo.FindByUsername(INVALID_USERNAME), Times.Once);

        }
        [Fact(DisplayName = "Authenticate test - Valid")]
        public async Task Authenticate_ValidTest()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                var response = await _service.Authenticate(VALID_REQUEST);
                Assert.Equal(VALID_RESPONSE, response);
            });
            Assert.Null(exception);
            _userRepository.Verify(repo => repo.FindByUsername(VALID_USERNAME), Times.Once);

        }

        [Fact(DisplayName = "Authenticate test - Invalid Password")]
        public async Task Authenticate_InvalidPasswordTest()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                await _service.Authenticate(INVALID_REQUEST_INVALID_PASSWORD);
            });
            Assert.NotNull(exception);
            Assert.IsType<UnauthorizedAccessException>(exception);
            Assert.Equal("Invalid password", exception.Message);
            _userRepository.Verify(repo => repo.FindByUsername(VALID_USERNAME), Times.Once);
        }
        public async Task DisposeAsync()
        {
            await Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            _tokenGenerator = new Mock<IAccessTokenGenerator>();
            _userRepository = new Mock<IUserRepository>();
            _refreshTokenRepository = new Mock<IRefreshTokenRepository>();
            _redis = new Mock<IRedisCacheService>();

            MockUserRepository();

            _tokenGenerator.Setup(generator => generator.GenerateToken(VALID_USER.Username, VALID_USER.Role.ToString())).Returns(VALID_JWT_TOKEN);

            MockRedis();
            MockRefreshTokenRepository();

            _service = new AuthService(_tokenGenerator.Object, _userRepository.Object, _refreshTokenRepository.Object, _redis.Object);

            await Task.CompletedTask;
        }
        private void MockUserRepository()
        {
            _userRepository.Setup(repo => repo.FindByUsername(VALID_USERNAME)).ReturnsAsync(VALID_USER);
            _userRepository.Setup(repo => repo.FindByUsername(INVALID_USERNAME)).ThrowsAsync(new InvalidOperationException("Not found"));

        }
        private void MockRedis()
        {
            _redis.Setup(cache => cache.RevokeToken(VALID_JTI, DateTime.Now.AddHours(2))).Returns(Task.CompletedTask);
            _redis.Setup(cache => cache.IsTokenRevoked(VALID_JTI)).ReturnsAsync(true);

        }
        private void MockRefreshTokenRepository()
        {
            _refreshTokenRepository.Setup(repo => repo.Create(It.Is<UserModel>(u => u.Equals(VALID_USER)))).ReturnsAsync(VALID_REFRESH_TOKEN_MODEL);
            _refreshTokenRepository.Setup(repo => repo.FindRefreshToken(VALID_REFRESH_TOKEN)).ReturnsAsync(VALID_REFRESH_TOKEN_MODEL);

        }
    }
}
