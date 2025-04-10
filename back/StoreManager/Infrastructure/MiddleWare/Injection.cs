using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.MiddleWare.Injectors;
using StoreManager.Infrastructure.Product.Repository;

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
           
            services.AddScoped<IProductRepository, ProductRepository>();
            
            services.AddMediatR(typeof(Program));

            return services;
        }
    }
}
