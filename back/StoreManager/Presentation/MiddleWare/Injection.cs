using MediatR;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Fonts;
using StoreManager.Application.GeoCoding;
using StoreManager.Fonts;
using StoreManager.Infrastructure.Context;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.GeoCoding;
using StoreManager.Infrastructure.MiddleWare.Injectors;

namespace StoreManager.Infrastructure.MiddleWare
{
    public static class Injection
    {
        public static IServiceCollection InjectDependencies(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgresConnection") ??
                                   throw new InvalidProgramException("Postgres connection not found");

            services.AddDbContext<WarehouseDbContext>(options => options.UseNpgsql(connectionString));

            services.InjectAuthDependencies(configuration);
            services.InjectDocumentDependencies(configuration);
            services.InjectUserDependencies(configuration);
            services.InjectMechanicalComponentDependencies(configuration);
            services.InjectInvoiceDependencies(configuration);
            services.InjectProviderDependencies(configuration);
            services.InjectProductDependencies(configuration);
            services.InjectExporterDependencies(configuration);
            services.InjectExportDependencies(configuration);

            services.AddScoped<IGeoCodingService, LocationIqService>();
            services.AddMediatR(typeof(Program));


            GlobalFontSettings.FontResolver = new FontResolver();

            return services;
        }
    }
}