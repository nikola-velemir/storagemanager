using MediatR;
using StoreManager.Infrastructure.Auth.Command;
using StoreManager.Infrastructure.Auth.DTO;
using StoreManager.Infrastructure.Auth.Tokens.AcessToken.Generator;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Repository;
using StoreManager.Infrastructure.MiddleWare.Exceptions;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace StoreManager.Infrastructure.Auth.Handler
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
