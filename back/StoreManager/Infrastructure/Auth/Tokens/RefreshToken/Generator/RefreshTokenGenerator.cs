using System.Security.Cryptography;

namespace StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Generator
{
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }
    }
}
