using StoreManager.Application.Product.Repository;
using StoreManager.Application.Product.Service;
using StoreManager.Domain.Product.Service;
using StoreManager.Infrastructure.Product.Repository;

namespace StoreManager.Infrastructure.MiddleWare.Injectors;

public static class ProductInjection
{
    public static IServiceCollection InjectProductDependencies(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddScoped<IProductRepository, ProductBlueprintRepository>();
        serviceCollection.AddScoped<IProductBlueprintService, ProductBlueprintService>();
        return serviceCollection;
    }
}