namespace StoreManager.Infrastructure.Auth.Tokens.AcessToken.Generator
{
    public interface IAccessTokenGenerator
    {
        public string GenerateToken(string username,string role);
    }
}
