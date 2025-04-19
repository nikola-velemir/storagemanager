using StackExchange.Redis;
using StoreManager.Application.Auth.Service;
using StoreManager.Application.Auth.Tokens;
using StoreManager.Application.Auth.Tokens.RefreshToken;
using StoreManager.Domain.Auth.Service;
using StoreManager.Infrastructure.Auth.Tokens;
using StoreManager.Infrastructure.Auth.Tokens.RefreshTokens;

namespace StoreManager.Infrastructure.MiddleWare.Injectors;

public static class AuthInjection
{
    public static IServiceCollection InjectAuthDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        var redisConnectionString = configuration.GetConnectionString("Redis")
                                    ?? throw new InvalidProgramException("Redis connection not found");

        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));
        services.AddSingleton<IRedisCacheService, RedisCacheService>();

        services.AddSingleton<IAccessTokenGenerator, AccessTokenGenerator>();

        services.AddSingleton<IRefreshTokenGenerator, RefreshTokenGenerator>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddHostedService<RefreshTokenCleanupService>();
        
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}