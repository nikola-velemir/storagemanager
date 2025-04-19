namespace StoreManager.Application.Auth.Tokens.RefreshToken
{
    public interface IRefreshTokenGenerator
    {
        public string GenerateRefreshToken();
        
    }
}
