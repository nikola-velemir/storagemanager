using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.MiddleWare.Injectors;
using StoreManager.Infrastructure.Product.Repository;
using StoreManager.Infrastructure.Product.Service;

namespace StoreManager.Infrastructure.MiddleWare
{
    public static class Injection
    {
        public static IServiceCollection InjectDependencies(this IServiceCollection services, IConfiguration configuration)
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
            
            services.AddMediatR(typeof(Program));

            return services;
        }
    }
}
