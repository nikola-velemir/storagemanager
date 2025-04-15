using StoreManager.Infrastructure.Document.Repository;
using StoreManager.Infrastructure.Document.Service;
using StoreManager.Infrastructure.Document.Service.FileService;
using StoreManager.Infrastructure.Document.Service.Reader;
using StoreManager.Infrastructure.Document.SupaBase.Service;

namespace StoreManager.Infrastructure.MiddleWare.Injectors;

public static class DocumentInjection
{
    public static IServiceCollection InjectDocumentDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddScoped<ICloudStorageService, SupabaseService>();

        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IDocumentService, DocumentService>();

        services.AddScoped<IDocumentReaderFactory, DocumentReaderFactory>();
        services.AddScoped<PdfService>();
        services.AddScoped<ExcelService>();
            
        services.AddScoped<IFileService, FileService>();

        
        return services;
    }

}