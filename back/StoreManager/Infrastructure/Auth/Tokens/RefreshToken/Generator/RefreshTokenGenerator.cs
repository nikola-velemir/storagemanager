using System.Security.Cryptography;

namespace StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Generator
{
    public class RefreshTokenGenerator(IConfiguration config) : IRefreshTokenGenerator
    {
        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(config.GetValue<int>("RefreshTokenSettings:Length")));
        }
    }
}
