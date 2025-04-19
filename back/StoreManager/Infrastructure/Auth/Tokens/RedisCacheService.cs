using StackExchange.Redis;
using StoreManager.Application.Auth.Tokens;

namespace StoreManager.Infrastructure.Auth.Tokens
{
    public class RedisCacheService(IConnectionMultiplexer redis) : IRedisCacheService
    {
        private readonly IDatabase _redisDb = redis.GetDatabase();

        public async Task RevokeToken(string jti, DateTime expiry)
        {
            var expirySeconds = (int)(expiry - DateTime.UtcNow.AddMinutes(0)).TotalSeconds;
            var span = TimeSpan.FromSeconds(expirySeconds);
            if (expirySeconds > 0)
            {
                await _redisDb.StringSetAsync($"revoked_token:{jti}", "revoked", span);
            }
        }

        public async Task<bool> IsTokenRevoked(string jti)
        {
            return await _redisDb.KeyExistsAsync($"revoked_token:{jti}");
        }
    }
}
