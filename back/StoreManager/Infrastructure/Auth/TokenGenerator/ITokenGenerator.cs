namespace StoreManager.Infrastructure.Auth.TokenGenerator
{
    public interface ITokenGenerator
    {
        public string GenerateToken(string username,string role);
    }
}
