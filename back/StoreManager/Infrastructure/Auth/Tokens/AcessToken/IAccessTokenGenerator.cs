namespace StoreManager.Infrastructure.Auth.Tokens.AcessToken
{
    public interface IAccessTokenGenerator
    {
        public string GenerateToken(string username,string role);
    }
}
