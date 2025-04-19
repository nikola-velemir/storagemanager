using System.Security.Cryptography;
using StoreManager.Application.Auth.Tokens.RefreshToken;

namespace StoreManager.Infrastructure.Auth.Tokens.RefreshTokens
{
    public class RefreshTokenGenerator(IConfiguration config) : IRefreshTokenGenerator
    {
        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(config.GetValue<int>("RefreshTokenSettings:Length")));
        }
    }
}
