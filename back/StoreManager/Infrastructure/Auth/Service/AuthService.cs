using StoreManager.Infrastructure.Auth.DTO;
using StoreManager.Infrastructure.Auth.Tokens.AcessToken.Generator;
using StoreManager.Infrastructure.Auth.Tokens.RedisCache;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Model;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Repository;
using StoreManager.Infrastructure.User.Model;
using StoreManager.Infrastructure.User.Repository;
using System.IdentityModel.Tokens.Jwt;

namespace StoreManager.Infrastructure.Auth.Service
{
    public class AuthService(
        IAccessTokenGenerator tokenGenerator,
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IRedisCacheService redis)
        : IAuthService
    {
        public async Task<LoginResponseDto?> Authenticate(LoginRequestDto request)
        {
            UserModel user = await userRepository.FindByUsername(request.Username);
            if (user.Password != request.Password) { throw new UnauthorizedAccessException("Invalid password"); }


            var role = user.Role.ToString();
            var accessToken =  tokenGenerator.GenerateToken(request.Username, role);

            var refreshToken = await refreshTokenRepository.Create(user);

            return new LoginResponseDto(accessToken, refreshToken.Token, role);

        }

        public async Task DeAuthenticate(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();


            var jwtToken = handler.ReadJwtToken(accessToken) ?? throw new BadHttpRequestException("Invalid token");


            var jti = jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Jti)?.Value;
            if (string.IsNullOrEmpty(jti))
            {
                throw new BadHttpRequestException("Invalid token");
            }

            await redis.RevokeToken(jti, jwtToken.ValidTo);

        }

        public async Task<LoginResponseDto?> RefreshAuthentication(RefreshRequestDto request)
        {
            var refreshToken = await refreshTokenRepository.FindRefreshToken(request.RefreshToken)
                ?? throw new InvalidOperationException("Not found");

            if (refreshToken.ExpiresOnUtc < DateTime.UtcNow)
            {
                throw new TimeoutException("The refresh token has expired");
            }

            string accessToken = tokenGenerator.GenerateToken(refreshToken.User.Username, refreshToken.User.Role.ToString());

            return new LoginResponseDto(accessToken, refreshToken.Token, refreshToken.User.Role.ToString());
        }
    }
}
