using StoreManager.Application.Product.Repository;
using StoreManager.Application.Product.Service;
using StoreManager.Infrastructure.Product.Repository;
using StoreManager.Infrastructure.Product.Service;

namespace StoreManager.Infrastructure.MiddleWare.Injectors;

public static class ProductInjection
{
    public static IServiceCollection InjectProductDependencies(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddScoped<IProductRepository, ProductRepository>();
        serviceCollection.AddScoped<IProductService, ProductService>();
        return serviceCollection;
    }
}