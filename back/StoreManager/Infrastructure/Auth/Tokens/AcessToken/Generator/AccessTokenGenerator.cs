using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoreManager.Infrastructure.Auth.Tokens.AcessToken.Generator
{
    public class AccessTokenGenerator(IConfiguration config) : IAccessTokenGenerator
    {
        public string GenerateToken(string username, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = Environment.GetEnvironmentVariable("JWT_SECRET", EnvironmentVariableTarget.User);

            if (string.IsNullOrEmpty(secret))
            {
                throw new InvalidOperationException("JWT Secret has not been set!");
            }

            var key = Encoding.UTF8.GetBytes(secret);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Name,username),
                new(ClaimTypes.Role, role)
            };

            var expiryTimeInMinutes = config.GetValue<int>("JwtSettings:ExpiryIntervalInMinutes");
            var issuer = config.GetValue<string>("JwtSettings:Issuer");
            var audience = config.GetValue<string>("JwtSettings:Audience");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expiryTimeInMinutes),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
