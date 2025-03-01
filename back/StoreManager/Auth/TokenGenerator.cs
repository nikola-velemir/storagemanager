using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoreManager.Auth
{
    public class TokenGenerator
    {

        public string GenerateToken(string username)
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
                new (JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.PreferredUsername,username),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = "store-manager",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
