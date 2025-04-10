using StoreManager.Infrastructure.Invoice.Repository;
using StoreManager.Infrastructure.Invoice.Service;

namespace StoreManager.Infrastructure.MiddleWare.Injectors;

public static class InvoiceInjection
{
    public static IServiceCollection InjectInvoiceDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IInvoiceItemRepository, InvoiceItemRepository>();
        services.AddScoped<IInvoiceService, InvoiceService>();

        return services;
    }
}