namespace StoreManager.Infrastructure.Auth.Tokens.AcessToken
{
    public interface IAcessTokenGenerator
    {
        public string GenerateToken(string username,string role);
    }
}
