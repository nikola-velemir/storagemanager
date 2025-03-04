using StoreManager.Infrastructure.Auth.Tokens.RedisCache;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace StoreManager.Infrastructure.MiddleWare
{
    public class TokenValidation
    {
        private readonly RequestDelegate _next;
        private readonly RedisCacheService _redisCacheService;

        public TokenValidation(RequestDelegate next, RedisCacheService redisCacheService)
        {
            _next = next;
            _redisCacheService = redisCacheService;
        }

        public async Task Invoke(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader?.StartsWith("Bearer ") == true)
            {
                var token = authHeader.Substring(7);
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var jti = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

                if (jti != null && await _redisCacheService.IsTokenRevoked(jti))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token has been revoked");
                    return;
                }
            }
            await _next(context);
        }
    }
}
