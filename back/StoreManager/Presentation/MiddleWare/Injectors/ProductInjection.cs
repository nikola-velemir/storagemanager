using StoreManager.Application.Product.Blueprint.Repository;
using StoreManager.Application.Product.Blueprint.Service;
using StoreManager.Domain.Product.Batch;
using StoreManager.Domain.Product.Batch.Repository;
using StoreManager.Domain.Product.Batch.Service;
using StoreManager.Domain.Product.Blueprint.Service;
using StoreManager.Infrastructure.Product.Batch.Repository;
using StoreManager.Infrastructure.Product.Blueprint.Repository;

namespace StoreManager.Presentation.MiddleWare.Injectors;

public static class ProductInjection
{
    public static IServiceCollection InjectProductDependencies(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IProductBlueprintRepository, ProductBlueprintBlueprintRepository>();
        serviceCollection.AddScoped<IProductBlueprintService, ProductBlueprintService>();
        serviceCollection.AddScoped<IProductBatchRepository, ProductBatchRepository>();
        serviceCollection.AddScoped<IProductBatchCheckService,ProductBatchCheckService>();
        return serviceCollection;
    }
}