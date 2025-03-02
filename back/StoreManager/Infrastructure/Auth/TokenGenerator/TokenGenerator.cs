using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoreManager.Infrastructure.Auth.TokenGenerator
{
    public class TokenGenerator : ITokenGenerator
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
                new(JwtRegisteredClaimNames.Name,username),
                new(ClaimTypes.Role, role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = "storemanager",
                Audience= "storemanagers",

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
