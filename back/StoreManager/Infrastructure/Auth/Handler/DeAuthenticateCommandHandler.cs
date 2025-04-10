using MediatR;
using StackExchange.Redis;
using StoreManager.Infrastructure.Auth.Command;
using StoreManager.Infrastructure.Auth.Tokens.RedisCache;
using System.IdentityModel.Tokens.Jwt;

namespace StoreManager.Infrastructure.Auth.Handler
{
    public class DeAuthenticateCommandHandler(IRedisCacheService redis) : IRequestHandler<DeAuthenticateCommand>
    {
        public async Task<Unit> Handle(DeAuthenticateCommand request, CancellationToken cancellationToken)
        {
            var handler = new JwtSecurityTokenHandler();


            var jwtToken = handler.ReadJwtToken(request.AccessToken) ?? throw new BadHttpRequestException("Invalid token");


            var jti = jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Jti)?.Value;
            if (string.IsNullOrEmpty(jti))
            {
                throw new BadHttpRequestException("Invalid token");
            }

            await redis.RevokeToken(jti, jwtToken.ValidTo);
            
            return Unit.Value;
        }
    }
}
