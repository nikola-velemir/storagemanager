using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Application.Invoice.Import.Service;
using StoreManager.Domain.Invoice.Base.Repository;
using StoreManager.Domain.Invoice.Import.Service;
using StoreManager.Infrastructure.Invoice.Import.Repository;

namespace StoreManager.Presentation.MiddleWare.Injectors;

public static class InvoiceInjection
{
    public static IServiceCollection InjectInvoiceDependencies(this IServiceCollection services)
    {
        services.AddScoped<IInvoiceRepository,InvoiceRepository>();
        services.AddScoped<IImportRepository, ImportRepository>();
        services.AddScoped<IImportService, ImportService>();

        return services;
    }
}