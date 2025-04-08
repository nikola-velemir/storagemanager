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
    public class AuthService : IAuthService
    {
        private readonly IAccessTokenGenerator _tokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IRedisCacheService _redis;

        public AuthService(
            IAccessTokenGenerator tokenGenerator,
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IRedisCacheService redis)
        {
            _tokenGenerator = tokenGenerator;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _redis = redis;
        }
        public async Task<LoginResponseDTO?> Authenticate(LoginRequestDTO request)
        {
            UserModel user = await _userRepository.FindByUsername(request.username);
            if (user.Password != request.password) { throw new UnauthorizedAccessException("Invalid password"); }


            var role = user.Role.ToString();
            var accessToken =  _tokenGenerator.GenerateToken(request.username, role);

            var refreshToken = await _refreshTokenRepository.Create(user);

            return new LoginResponseDTO(accessToken, refreshToken.Token, role);

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

            await _redis.RevokeToken(jti, jwtToken.ValidTo);

        }

        public async Task<LoginResponseDTO?> RefreshAuthentication(RefreshRequestDTO request)
        {
            var refreshToken = await _refreshTokenRepository.FindRefreshToken(request.refresh_token)
                ?? throw new InvalidOperationException("Not found");

            if (refreshToken.ExpiresOnUtc < DateTime.UtcNow)
            {
                throw new TimeoutException("The refresh token has expired");
            }

            string accessToken = _tokenGenerator.GenerateToken(refreshToken.User.Username, refreshToken.User.Role.ToString());

            return new LoginResponseDTO(accessToken, refreshToken.Token, refreshToken.User.Role.ToString());
        }
    }
}
