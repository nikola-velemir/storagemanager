using StoreManager.Application.User.Repository;
using StoreManager.Application.User.Service;
using StoreManager.Domain.User.Service;
using StoreManager.Infrastructure.User.Repository;

namespace StoreManager.Presentation.MiddleWare.Injectors;

public static class UserInjection
{
    public static IServiceCollection InjectUserDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}