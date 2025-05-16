using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using StoreManager.Application.Document;
using StoreManager.Application.Document.Repository;
using StoreManager.Application.Document.Service.FileService;
using StoreManager.Application.Document.Service.Reader;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Application.MechanicalComponent.Repository;
using StoreManager.Domain;
using StoreManager.Domain.Document.Specification;
using StoreManager.Domain.Invoice.Import.Service;
using StoreManager.Infrastructure.Context;

namespace StoreManager.outbox;

public class OutboxWorker(IServiceProvider serviceProvider, ILogger<OutboxWorker> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<WarehouseDbContext>();
            var unprocessed = await dbContext.OutboxMessages.Where(x => x.ProcessedAt == null)
                .Take(10)
                .ToListAsync(cancellationToken: stoppingToken);
            foreach (var message in unprocessed)

                try
                {
                    if (message.Type == "DocumentProcessingRequest")
                    {
                        var data = JsonSerializer.Deserialize<DocumentProcessingRequest>(message.Payload)!;
                        var readerFactory = scope.ServiceProvider.GetRequiredService<IDocumentReaderFactory>();
                        var mechanicalComponentRepository =
                            scope.ServiceProvider.GetRequiredService<IMechanicalComponentRepository>();
                        var fileService = scope.ServiceProvider.GetRequiredService<IFileService>();
                        var importService = scope.ServiceProvider.GetRequiredService<IImportService>();
                        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                        var documentRepository = scope.ServiceProvider.GetRequiredService<IDocumentRepository>();
                        var importRepository = scope.ServiceProvider.GetRequiredService<IImportRepository>();

                        var document =
                            await documentRepository.FindByNameAsync(new DocumentWithDocumentChunks(), data.FileName);
                        if (document is null){
                            logger.LogWarning("Document not found for file: {FileName}", data.FileName);
                            continue;
                        }
                        var import = await importRepository.FindByDocumentId(document.Id);
                        if (import is null)
                        {
                            logger.LogWarning("Import not found for document id: {DocumentId}", document.Id);
                            continue;
                        }
                        var filePath = Path.Combine(env.WebRootPath, "uploads", "invoice",
                            $"{data.DocumentId}.{DocumentUtils.GetRawMimeType(data.MimeType)}");

                        var reader = readerFactory.GetReader(DocumentUtils.GetRawMimeType(data.MimeType));
                        var metadata = reader.ExtractDataFromDocument(filePath);

                        var components =
                            await mechanicalComponentRepository.CreateFromExtractionMetadataAsync(metadata);
                        await importService.Create(import!, metadata,components);
                        await fileService.DeleteAllChunks(document!);
                        document.IsProcessed = true;


                        await unitOfWork.CommitAsync(stoppingToken);
                    }

                    message.ProcessedAt = DateTime.UtcNow;
                }
                catch (Exception ex)
                {
                    message.Error = ex.ToString();
                    logger.LogError(ex, "Failed to process outbox message {Id}", message.Id);
                }

            await dbContext.SaveChangesAsync(stoppingToken);

            await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
        }
    }
}