using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using StoreManager.Infrastructure.Auth.Tokens.RedisCache;
using System.Text;

namespace StoreManager.Infrastructure.Auth
{
    public static class JwtAuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentications(this IServiceCollection services)
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

                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidIssuer = "storemanager",
                        ValidAudience = "storemanagers",
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
                            // Retrieve the JWT's jti (JWT ID)
                            var jti = context.Principal?.FindFirst("jti")?.Value;

                            if (!string.IsNullOrEmpty(jti))
                            {
                                // Retrieve the RedisCacheService from the HTTP context's service provider
                                var redisCacheService = context.HttpContext.RequestServices.GetRequiredService<IRedisCacheService>();

                                // Check if the token is revoked using RedisCacheService
                                var isRevoked = await redisCacheService.IsTokenRevoked(jti);

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
