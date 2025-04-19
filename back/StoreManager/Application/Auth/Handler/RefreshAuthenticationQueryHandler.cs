using MediatR;
using StoreManager.Application.Auth.DTO;
using StoreManager.Application.Auth.Query;
using StoreManager.Application.Auth.Tokens;
using StoreManager.Application.Auth.Tokens.RefreshToken;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Application.Auth.Handler
{
    public class RefreshAuthenticationQueryHandler(
        IRefreshTokenRepository refreshTokenRepository,
        IAccessTokenGenerator tokenGenerator)
        : IRequestHandler<RefreshAuthenticationQuery, LoginResponseDto?>
    {
        public async Task<LoginResponseDto?> Handle(RefreshAuthenticationQuery request, CancellationToken cancellationToken)
        {
            Validate(request);
            var refreshToken = await refreshTokenRepository.FindRefreshToken(request.RefreshToken)
                ?? throw new NotFoundException("Not found");

            if (refreshToken.ExpiresOnUtc < DateTime.UtcNow)
            {
                throw new TimeoutException("The refresh token has expired");
            }

            string accessToken = tokenGenerator.GenerateToken(refreshToken.User.Username, refreshToken.User.Role.ToString());

            return new LoginResponseDto(accessToken, refreshToken.Token, refreshToken.User.Role.ToString());
        }

        private static void Validate(RefreshAuthenticationQuery request)
        {
            if(string.IsNullOrEmpty(request.RefreshToken) || string.IsNullOrEmpty(request.RefreshToken))
                throw new ValidationException("Refresh token is required");
        }
        
        

    }
}
