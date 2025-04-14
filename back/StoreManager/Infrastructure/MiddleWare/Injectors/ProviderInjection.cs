using StoreManager.Infrastructure.BusinessPartner.Provider.Repository;
using StoreManager.Infrastructure.BusinessPartner.Provider.Service;

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