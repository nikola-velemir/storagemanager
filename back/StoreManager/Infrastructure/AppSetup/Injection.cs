using StoreManager.Infrastructure.Auth.Service;
using StoreManager.Infrastructure.Auth.TokenGenerator;
using StoreManager.Infrastructure.User.Repository;
using StoreManager.Infrastructure.User.Service;

namespace StoreManager.Infrastructure.AppSetup
{
    public static class Injection
    {
        public static IServiceCollection InjectDependencies(this IServiceCollection services)
        {
            services.AddSingleton<ITokenGenerator, TokenGenerator>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
