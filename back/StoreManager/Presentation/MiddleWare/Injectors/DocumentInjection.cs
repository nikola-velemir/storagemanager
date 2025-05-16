using StoreManager.Application.Document.Repository;
using StoreManager.Application.Document.Service;
using StoreManager.Application.Document.Service.FileService;
using StoreManager.Application.Document.Service.Reader;
using StoreManager.Domain.Document.Service;
using StoreManager.Domain.Document.Storage.Service;
using StoreManager.Infrastructure.Document.FileService;
using StoreManager.Infrastructure.Document.Reader;
using StoreManager.Infrastructure.Document.Repository;
using StoreManager.Infrastructure.Document.Storage.Service;

namespace StoreManager.Presentation.MiddleWare.Injectors;

public static class DocumentInjection
{
    public static IServiceCollection InjectDocumentDependencies(this IServiceCollection services)
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