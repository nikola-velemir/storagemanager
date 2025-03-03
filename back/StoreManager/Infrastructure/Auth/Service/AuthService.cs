using StoreManager.Infrastructure.Auth.DTO;
using StoreManager.Infrastructure.Auth.Tokens.AcessToken;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Model;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Repository;
using StoreManager.Infrastructure.User.Model;
using StoreManager.Infrastructure.User.Repository;

namespace StoreManager.Infrastructure.Auth.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAcessTokenGenerator _tokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthService(IAcessTokenGenerator tokenGenerator, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _tokenGenerator = tokenGenerator;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public LoginResponseDTO? Authenticate(LoginRequestDTO request)
        {
            UserModel user = _userRepository.FindByUsername(request.username);
            if (user.Password != request.password) { throw new UnauthorizedAccessException("Invalid password"); }


            var role = user.Role.ToString();
            var accessToken = _tokenGenerator.GenerateToken(request.username, role);

            var refreshToken = _refreshTokenRepository.Create(user);

            return new LoginResponseDTO(accessToken, refreshToken.Token, role);

        }

        public LoginResponseDTO? RefreshAuthentication(RefreshRequestDTO request)
        {
            RefreshTokenModel refreshToken = _refreshTokenRepository.FindRefreshToken(request.refresh_token)
                ?? throw new InvalidOperationException("Not found");

            if(refreshToken.ExpiresOnUtc < DateTime.UtcNow)
            {
                throw new TimeoutException("The refresh token has expired");
            }

            string accessToken = _tokenGenerator.GenerateToken(refreshToken.User.Username, refreshToken.User.Role.ToString());

            return new LoginResponseDTO(accessToken, refreshToken.Token, refreshToken.User.Role.ToString());
        }
    }
}
