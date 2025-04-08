using MediatR;
using StoreManager.Infrastructure.Auth.Command;
using StoreManager.Infrastructure.Auth.DTO;
using StoreManager.Infrastructure.Auth.Tokens.AcessToken.Generator;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Repository;

namespace StoreManager.Infrastructure.Auth.Handler
{
    public class RefreshAuthentificationQueryHandler : IRequestHandler<RefreshAuthentificationQuery, LoginResponseDTO?>
    {
        private IRefreshTokenRepository _refreshTokenRepository;
        private IAccessTokenGenerator _tokenGenerator;
        public RefreshAuthentificationQueryHandler(IRefreshTokenRepository refreshTokenRepository, IAccessTokenGenerator tokenGenerator)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<LoginResponseDTO?> Handle(RefreshAuthentificationQuery request, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenRepository.FindRefreshToken(request.RefreshToken)
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
