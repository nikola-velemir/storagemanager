using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StoreManager.Application.Auth.Tokens;

namespace StoreManager.Infrastructure.MiddleWare
{
    public static class JwtAuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentications(this IServiceCollection services, IConfiguration _config)
        {
            services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    var secret = Environment.GetEnvironmentVariable("JWT_SECRET", EnvironmentVariableTarget.User);

                    if (string.IsNullOrEmpty(secret))
                    {
                        throw new InvalidOperationException("JWT Secret has not been set!");
                    }

                    var key = Encoding.UTF8.GetBytes(secret);

                    var issuer = _config.GetValue<string>("JwtSettings:Issuer");
                    var audience = _config.GetValue<string>("JwtSettings:Audience");
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        ValidateLifetime = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero
                    };
                    x.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async context =>
                        {
                            var jti = context.Principal?.FindFirst("jti")?.Value;

                            if (!string.IsNullOrEmpty(jti))
                            {
                                var redisCacheService = context.HttpContext.RequestServices.GetRequiredService<IRedisCacheService>();

                                var isRevoked = await redisCacheService.IsTokenRevokedAsync(jti);

                                if (isRevoked)
                                {
                                    context.Fail("The token has been revoked.");
                                }
                            }
                        }
                    };
                });
            return services;
        }
    }
}
