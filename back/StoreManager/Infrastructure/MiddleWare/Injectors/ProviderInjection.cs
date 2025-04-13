using StoreManager.Infrastructure.Provider.Repository;
using StoreManager.Infrastructure.Provider.Service;

namespace StoreManager.Infrastructure.MiddleWare.Injectors;

public static class ProviderInjection
{
    public static IServiceCollection InjectProviderDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IProviderRepository, ProviderRepository>();
        services.AddScoped<IProviderService, ProviderService>();
        return services;
    }
}