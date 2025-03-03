using StoreManager.Infrastructure.Auth.Service;
using StoreManager.Infrastructure.Auth.Tokens.AcessToken;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Generator;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Repository;
using StoreManager.Infrastructure.User.Repository;
using StoreManager.Infrastructure.User.Service;

namespace StoreManager.Infrastructure.AppSetup
{
    public static class Injection
    {
        public static IServiceCollection InjectDependencies(this IServiceCollection services)
        {
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
