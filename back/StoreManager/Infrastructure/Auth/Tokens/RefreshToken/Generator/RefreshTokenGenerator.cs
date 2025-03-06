using System.Security.Cryptography;

namespace StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Generator
{
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        private readonly IConfiguration _config;
        public RefreshTokenGenerator(IConfiguration config) {
            _config = config;
        }
        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(_config.GetValue<int>("RefreshTokenSettings:Length")));
        }
    }
}
