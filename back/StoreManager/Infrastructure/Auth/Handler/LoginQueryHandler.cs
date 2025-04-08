using MediatR;
using StoreManager.Infrastructure.Auth.Command;
using StoreManager.Infrastructure.Auth.DTO;
using StoreManager.Infrastructure.Auth.Tokens.AcessToken.Generator;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Repository;
using StoreManager.Infrastructure.User.Model;
using StoreManager.Infrastructure.User.Repository;

namespace StoreManager.Infrastructure.Auth.Handler
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponseDTO?>
    {
        private IUserRepository _userRepository;
        private IAccessTokenGenerator _tokenGenerator;
        private IRefreshTokenRepository _refreshTokenRepository;
        public LoginQueryHandler(IUserRepository userRepository, IAccessTokenGenerator tokenGenerator, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<LoginResponseDTO?> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            UserModel user = await _userRepository.FindByUsername(request.Username);
            if (user.Password != request.Username) { throw new UnauthorizedAccessException("Invalid password"); }


            var role = user.Role.ToString();
            var accessToken = _tokenGenerator.GenerateToken(request.Username, role);

            var refreshToken = await _refreshTokenRepository.Create(user);

            return new LoginResponseDTO(accessToken, refreshToken.Token, role);
        }
    }
}
