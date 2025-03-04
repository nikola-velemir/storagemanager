namespace StoreManager.Infrastructure.Auth.Tokens.RedisCache
{
    public interface IRedisCacheService
    {
        public  Task RevokeToken(string jti, DateTime expiry);


        public  Task<bool> IsTokenRevoked(string jti);
       
    }
}
