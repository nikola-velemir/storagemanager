using System.IdentityModel.Tokens.Jwt;
using MediatR;
using StoreManager.Application.Auth.Command;
using StoreManager.Application.Auth.Errors;
using StoreManager.Application.Auth.Tokens;
using StoreManager.Application.Common;

namespace StoreManager.Application.Auth.Handler
{
    public class DeAuthenticateCommandHandler(IRedisCacheService redis) : IRequestHandler<DeAuthenticateCommand,Result>
    {
        public async Task<Result> Handle(DeAuthenticateCommand request, CancellationToken cancellationToken)
        {
            var handler = new JwtSecurityTokenHandler();


            var jwtToken = handler.ReadJwtToken(request.AccessToken) ?? throw new BadHttpRequestException("Invalid token");


            var jti = jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Jti)?.Value;
            if (string.IsNullOrEmpty(jti))
            {
                return RefreshTokenErrors.InvalidTokenError;
            }

            await redis.RevokeTokenAsync(jti, jwtToken.ValidTo);
            
            return Result.Success();
        }
    }
}
