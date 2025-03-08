using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.Tests.Auth.Tokens.AccessToken.Generator
{
    public class MockValidator
    {
        private readonly string _secret;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _config;
        public MockValidator(string secret, Microsoft.Extensions.Configuration.IConfiguration config)
        {
            _secret = secret;
            _config = config;
        }
        public SecurityToken ValidateToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateIssuer = true,
                ValidIssuer = _config["JwtSettings:Issuer"],
                ValidateAudience = true,
                ValidAudience = _config["JwtSettings:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var principal = handler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            return validatedToken;
        }
    }
}
