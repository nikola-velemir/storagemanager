namespace StoreManager.Application.Auth.Tokens
{
    public interface IAccessTokenGenerator
    {
        public string GenerateToken(string username,string role);
    }
}
