using MediatR;
using StoreManager.Application.Auth.DTO;
using StoreManager.Application.Auth.Errors;
using StoreManager.Application.Auth.Query;
using StoreManager.Application.Auth.Tokens;
using StoreManager.Application.Auth.Tokens.RefreshToken;
using StoreManager.Application.Common;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Application.Auth.Handler
{
    public class RefreshAuthenticationQueryHandler(
        IRefreshTokenRepository refreshTokenRepository,
        IAccessTokenGenerator tokenGenerator)
        : IRequestHandler<RefreshAuthenticationQuery, Result<LoginResponseDto>>
    {
        public async Task<Result<LoginResponseDto>> Handle(RefreshAuthenticationQuery request,
            CancellationToken cancellationToken)
        {
            Validate(request);
            var refreshToken = await refreshTokenRepository.FindRefreshTokenAsync(request.RefreshToken)
                               ?? throw new NotFoundException("Not found");

            if (refreshToken.ExpiresOnUtc < DateTime.UtcNow)
            {
                return RefreshTokenErrors.TokenExpiredError;
            }

            var accessToken =
                tokenGenerator.GenerateToken(refreshToken.User.Username, refreshToken.User.Role.ToString());

            var response = new LoginResponseDto(accessToken, refreshToken.Token, refreshToken.User.Role.ToString());
            return Result.Success(response);
        }

        private static void Validate(RefreshAuthenticationQuery request)
        {
            if (string.IsNullOrEmpty(request.RefreshToken) || string.IsNullOrEmpty(request.RefreshToken))
                throw new ValidationException("Refresh token is required");
        }
    }
}