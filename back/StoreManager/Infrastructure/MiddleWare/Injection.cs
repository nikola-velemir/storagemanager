using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using StoreManager.Infrastructure.Auth.Service;
using StoreManager.Infrastructure.Auth.Tokens.AcessToken;
using StoreManager.Infrastructure.Auth.Tokens.RedisCache;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Generator;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Repository;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.User.Repository;
using StoreManager.Infrastructure.User.Service;

namespace StoreManager.Infrastructure.AppSetup
{
    public static class Injection
    {
        public static IServiceCollection InjectDependencies(this IServiceCollection services,IConfiguration configuration)
        {
            var redisConnectionString = configuration.GetValue<string>("Redis") 
                ?? throw new InvalidProgramException("Redis connection not found");

            var connectionString = configuration.GetConnectionString("PostgresConnection");


            services.AddDbContext<WarehouseDbContext>(options => options.UseNpgsql(connectionString));
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));

            services.AddSingleton<IRedisCacheService, RedisCacheService>();
            services.AddSingleton<IAcessTokenGenerator, AcessTokenGenerator>();
            services.AddSingleton<IRefreshTokenGenerator, RefreshTokenGenerator>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRespository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            
            return services;
        }
    }
}
