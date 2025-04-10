using StoreManager.Infrastructure.User.Repository;
using StoreManager.Infrastructure.User.Service;

namespace StoreManager.Infrastructure.MiddleWare.Injectors;

public static class UserInjection
{
    public static IServiceCollection InjectUserDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}