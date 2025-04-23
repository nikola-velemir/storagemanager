using StoreManager.Application.Invoice.Export.Repository;
using StoreManager.Application.Invoice.Export.Service;
using StoreManager.Infrastructure.Invoice.Export.Repository;
using StoreManager.Infrastructure.Invoice.Export.Service;

namespace StoreManager.Infrastructure.MiddleWare.Injectors;

public static class ExportInjection
{
    public static IServiceCollection InjectExportDependencies(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddScoped<IExportRepository, ExportRepository>();
        serviceCollection.AddScoped<IExportService, ExportService>();
        return serviceCollection;
    }
}