using StoreManager.Application.BusinessPartner.Provider.Repository;
using StoreManager.Application.BusinessPartner.Provider.Service;
using StoreManager.Domain.BusinessPartner.Provider.Service;
using StoreManager.Infrastructure.BusinessPartner.Provider.Repository;

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