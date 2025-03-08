using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Moq;
using StoreManager.Infrastructure.Auth.Service;
using StoreManager.Infrastructure.Auth.Tokens.AcessToken.Generator;
using StoreManager.Infrastructure.Auth.Tokens.RedisCache;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Model;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Repository;
using StoreManager.Infrastructure.User.Model;
using StoreManager.Infrastructure.User.Repository;

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

        [Fact(DisplayName = "Authenticate test - Invalid Username")]
        public async Task Authenticate_InvalidUsernameTest()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                await _service.Authenticate(AuthServiceTestData.INVALID_REQUEST_INVALID_USERNAME);
            });


            Assert.NotNull(exception);
            Assert.IsType<InvalidOperationException>(exception);
            Assert.Equal("Not found", exception.Message);
            _userRepository.Verify(repo => repo.FindByUsername(AuthServiceTestData.INVALID_USERNAME), Times.Once);

        }
        [Fact(DisplayName = "Authenticate test - Valid")]
        public async Task Authenticate_ValidTest()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                var response = await _service.Authenticate(AuthServiceTestData.VALID_REQUEST);
                Assert.Equal(AuthServiceTestData.VALID_RESPONSE, response);
            });
            Assert.Null(exception);
            _userRepository.Verify(repo => repo.FindByUsername(AuthServiceTestData.VALID_USERNAME), Times.Once);

        }

        [Fact(DisplayName = "Authenticate test - Invalid Password")]
        public async Task Authenticate_InvalidPasswordTest()
        {
            Exception exception = await Record.ExceptionAsync(async () =>
            {
                await _service.Authenticate(AuthServiceTestData.INVALID_REQUEST_INVALID_PASSWORD);
            });
            Assert.NotNull(exception);
            Assert.IsType<UnauthorizedAccessException>(exception);
            Assert.Equal("Invalid password", exception.Message);
            _userRepository.Verify(repo => repo.FindByUsername(AuthServiceTestData.VALID_USERNAME), Times.Once);
        }

        [Fact(DisplayName = "DeAuthenticate test - Invalid Access Token")]
        public async Task DeAuthenticate_InvalidAccessTokenTest()
        {
            Exception ex = await Record.ExceptionAsync(async () =>
            {
                await _service.DeAuthenticate(AuthServiceTestData.INVALID_JWT_TOKEN);
            });
            Assert.NotNull(ex);
            Assert.IsType<SecurityTokenMalformedException>(ex);
        }
        [Fact(DisplayName = "DeAuthenticate test - Invalid Access Token")]
        public async Task DeAuthenticate_InvalidAccessTokenNoJtiTest()
        {
            Exception ex = await Record.ExceptionAsync(async () =>
            {
                await _service.DeAuthenticate(AuthServiceTestData.INVALID_JWT_TOKEN_NO_JTI);
            });
            Assert.NotNull(ex);
            Assert.IsType<BadHttpRequestException>(ex);
            Assert.Equal("Invalid token", ex.Message);
        }

        [Fact(DisplayName = "DeAuthenticate test - Valid")]
        public async Task DeAuthenticate_ValidTest()
        {
            Exception ex = await Record.ExceptionAsync(async () =>
            {
                await _service.DeAuthenticate(AuthServiceTestData.VALID_JWT_TOKEN);
            });
            Assert.Null(ex);
        }

        [Fact(DisplayName = "RefreshAuthentication test - Invalid Refresh Token")]
        public async Task RefreshAuthentication_InvalidRefreshTokenTest()
        {
            Exception ex = await Record.ExceptionAsync(async () =>
            {
                await _service.RefreshAuthentication(AuthServiceTestData.EXPIRED_REFRESH_REQUEST);
            });
            Assert.NotNull(ex);
            Assert.IsType<TimeoutException>(ex);
            Assert.Equal("The refresh token has expired", ex.Message);
            _refreshTokenRepository.Verify(repo => repo.FindRefreshToken(AuthServiceTestData.VALID_REFRESH_TOKEN_EXPIRED), Times.Once);
        }

        [Fact(DisplayName = "RefreshAuthentication test - Expired Token")]
        public async Task RefreshAuthentication_ExpiredTokenTest()
        {
            Exception ex = await Record.ExceptionAsync(async () =>
            {
                await _service.RefreshAuthentication(AuthServiceTestData.INVALID_REFRESH_REQUEST);
            });
            Assert.NotNull(ex);
            Assert.IsType<InvalidOperationException>(ex);
            Assert.Equal("Not found", ex.Message);
            _refreshTokenRepository.Verify(repo => repo.FindRefreshToken(AuthServiceTestData.INVALID_REFRESH_TOKEN), Times.Once);
        }

        [Fact(DisplayName = "RefreshAuthentication test - Valid")]
        public async Task RefreshAuthentication_ValidTest()
        {
            Exception ex = await Record.ExceptionAsync(async () =>
            {
                var response = await _service.RefreshAuthentication(AuthServiceTestData.VALID_REFRESH_REQUEST);
                Assert.Equal(AuthServiceTestData.VALID_RESPONSE, response);
            });
            Assert.Null(ex);
            _refreshTokenRepository.Verify(repo => repo.FindRefreshToken(AuthServiceTestData.VALID_REFRESH_TOKEN), Times.Once);
            _refreshTokenRepository.Verify(repo => repo.Create(AuthServiceTestData.VALID_USER), Times.Never);
        }
        public async Task DisposeAsync()
        {

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            _tokenGenerator = null;
            _userRepository = null;
            _refreshTokenRepository = null;
            _redis = null;
            _service = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            await Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            _tokenGenerator = new Mock<IAccessTokenGenerator>();
            _userRepository = new Mock<IUserRepository>();
            _refreshTokenRepository = new Mock<IRefreshTokenRepository>();
            _redis = new Mock<IRedisCacheService>();

            MockUserRepository();

            _tokenGenerator.Setup(generator => generator.GenerateToken(AuthServiceTestData.VALID_USER.Username, AuthServiceTestData.VALID_USER.Role.ToString())).Returns(AuthServiceTestData.VALID_JWT_TOKEN);

            MockRedis();
            MockRefreshTokenRepository();

            _service = new AuthService(_tokenGenerator.Object, _userRepository.Object, _refreshTokenRepository.Object, _redis.Object);

            await Task.CompletedTask;
        }
        private void MockUserRepository()
        {
            _userRepository.Setup(repo => repo.FindByUsername(AuthServiceTestData.VALID_USERNAME)).ReturnsAsync(AuthServiceTestData.VALID_USER);
            _userRepository.Setup(repo => repo.FindByUsername(AuthServiceTestData.INVALID_USERNAME)).ThrowsAsync(new InvalidOperationException("Not found"));

        }
        private void MockRedis()
        {
            _redis.Setup(cache => cache.RevokeToken(AuthServiceTestData.VALID_JTI, DateTime.Now.AddHours(2))).Returns(Task.CompletedTask);
            _redis.Setup(cache => cache.IsTokenRevoked(AuthServiceTestData.VALID_JTI)).ReturnsAsync(true);

        }
        private void MockRefreshTokenRepository()
        {
            _refreshTokenRepository.Setup(repo => repo.Create(It.Is<UserModel>(u => u.Equals(AuthServiceTestData.VALID_USER)))).ReturnsAsync(AuthServiceTestData.VALID_REFRESH_TOKEN_MODEL);
            _refreshTokenRepository.Setup(repo => repo.FindRefreshToken(AuthServiceTestData.VALID_REFRESH_TOKEN)).ReturnsAsync(AuthServiceTestData.VALID_REFRESH_TOKEN_MODEL);
            _refreshTokenRepository.Setup(repo => repo.FindRefreshToken(AuthServiceTestData.INVALID_REFRESH_TOKEN)).ReturnsAsync((RefreshTokenModel?)null);
            _refreshTokenRepository.Setup(repo => repo.FindRefreshToken(AuthServiceTestData.VALID_REFRESH_TOKEN_EXPIRED)).ReturnsAsync(AuthServiceTestData.EXPIRED_REFRESH_TOKEN_MODEL);
        }
    }
}
