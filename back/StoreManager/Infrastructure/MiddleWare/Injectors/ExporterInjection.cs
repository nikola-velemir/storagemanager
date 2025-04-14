using StoreManager.Infrastructure.BusinessPartner.Exporter.Repository;

namespace StoreManager.Infrastructure.MiddleWare.Injectors;

public static class ExporterInjection
{
    public static IServiceCollection InjectExporterDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IExporterRepository, ExporterRepository>();
        return services;
    }
}