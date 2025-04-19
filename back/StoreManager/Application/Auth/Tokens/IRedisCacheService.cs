namespace StoreManager.Application.Auth.Tokens
{
    public interface IRedisCacheService
    {
        public Task RevokeTokenAsync(string jti, DateTime expiry);
        public Task<bool> IsTokenRevokedAsync(string jti);

    }
}
