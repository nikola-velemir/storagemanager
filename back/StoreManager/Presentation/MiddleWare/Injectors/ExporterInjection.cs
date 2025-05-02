using StoreManager.Application.BusinessPartner.Base.Repository;
using StoreManager.Application.BusinessPartner.Exporter.Repository;
using StoreManager.Infrastructure.BusinessPartner.Base.Repository;
using StoreManager.Infrastructure.BusinessPartner.Exporter.Repository;

namespace StoreManager.Presentation.MiddleWare.Injectors;

public static class ExporterInjection
{
    public static IServiceCollection InjectExporterDependencies(this IServiceCollection services)
    {
        services.AddScoped<IBusinessPartnerRepository, BusinessPartnerRepository>();
        services.AddScoped<IExporterRepository, ExporterRepository>();
        return services;
    }
}