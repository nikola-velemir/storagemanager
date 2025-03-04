using StackExchange.Redis;
using System.Threading.Tasks;

namespace StoreManager.Infrastructure.Auth.Tokens.RedisCache
{
    public class RedisCacheService
    {
        private readonly IDatabase _redisDb;
        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }
        public  async Task RevokeToken(string jti, DateTime expiry)
        {
            var expirySeconds = (int)(expiry - DateTime.UtcNow).TotalSeconds;
            if(expirySeconds > 0)
            {
               await  _redisDb.StringSetAsync($"revoked_token:{jti}", "revoked", TimeSpan.FromSeconds(expirySeconds));
            }
        }

        public async Task<bool> IsTokenRevoked(string jti)
        {
            return await _redisDb.KeyExistsAsync($"revoked_token:{jti}");
        }
    }
}
