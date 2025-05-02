using StoreManager.Application.MechanicalComponent.Repository;
using StoreManager.Application.MechanicalComponent.Service;
using StoreManager.Infrastructure.MechanicalComponent.Repository;
using StoreManager.Infrastructure.MechanicalComponent.Service;

namespace StoreManager.Presentation.MiddleWare.Injectors;

public static class MechanicalComponentInjection
{
    public static IServiceCollection InjectMechanicalComponentDependencies(this IServiceCollection services)
    {
        services.AddScoped<IMechanicalComponentRepository, MechanicalComponentRepository>();
        services.AddScoped<IMechanicalComponentService, MechanicalComponentService>();
        return services;
    }
}