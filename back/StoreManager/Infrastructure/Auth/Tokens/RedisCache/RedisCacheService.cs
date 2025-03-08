using StackExchange.Redis;
using System.Threading.Tasks;

namespace StoreManager.Infrastructure.Auth.Tokens.RedisCache
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDatabase _redisDb;
        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }
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
