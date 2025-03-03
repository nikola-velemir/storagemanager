using System.Security.Cryptography;

namespace StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Generator
{
    public interface IRefreshTokenGenerator
    {
        public string GenerateRefreshToken();
        
    }
}
