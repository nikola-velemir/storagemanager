using StoreManager.Application.MechanicalComponent.Repository;
using StoreManager.Application.MechanicalComponent.Service;
using StoreManager.Infrastructure.MechanicalComponent.Repository;
using StoreManager.Infrastructure.MechanicalComponent.Service;

namespace StoreManager.Infrastructure.MiddleWare.Injectors;

public static class MechanicalComponentInjection
{
    public static IServiceCollection InjectMechanicalComponentDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IMechanicalComponentRepository, MechanicalComponentRepository>();
        services.AddScoped<IMechanicalComponentService, MechanicalComponentService>();
        return services;
    }
}