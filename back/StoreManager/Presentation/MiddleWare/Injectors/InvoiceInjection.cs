using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Application.Invoice.Import.Service;
using StoreManager.Infrastructure.Invoice.Base.Repository;
using StoreManager.Infrastructure.Invoice.Import.Repository;
using StoreManager.Infrastructure.Invoice.Import.Service;

namespace StoreManager.Infrastructure.MiddleWare.Injectors;

public static class InvoiceInjection
{
    public static IServiceCollection InjectInvoiceDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IInvoiceRepository,InvoiceRepository>();
        services.AddScoped<IImportRepository, ImportRepository>();
        services.AddScoped<IImportItemRepository, ImportItemRepository>();
        services.AddScoped<IImportService, ImportService>();

        return services;
    }
}