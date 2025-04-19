using MediatR;
using StoreManager.Application.BusinessPartner.Exporter.Repository;
using StoreManager.Application.Invoice.Export.Command;
using StoreManager.Application.Invoice.Export.DTO;
using StoreManager.Application.Invoice.Export.Repository;
using StoreManager.Application.Product.Repository;
using StoreManager.Domain.Document.Service;
using StoreManager.Infrastructure.Invoice.Base;
using StoreManager.Infrastructure.Invoice.Export.Model;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Application.Invoice.Export.Handler;

public class CreateExportCommandHandler(
    IExporterRepository exporterRepository,
    IExportRepository exportRepository,
    IExportItemRepository  exportItemRepository,
    IProductRepository productRepository,
    IDocumentService documentService)
    : IRequestHandler<CreateExportCommand>
{
    public async Task<Unit> Handle(CreateExportCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.ProviderId, out _))
        {
            throw new ArgumentException("Invalid provider id");
        }

        var exporterId = Guid.Parse(request.ProviderId);
        var exporter = await exporterRepository.FindById((exporterId)) ??
                       throw new NotFoundException("Exporter not found");

        var exportId = Guid.NewGuid();

        var fileId = Guid.NewGuid();
        var productRows = await ConvertToProductRows(request.Products);

        var dateIssued = DateOnly.FromDateTime(DateTime.UtcNow);
        
        var document = await documentService.UploadExport(exporter,dateIssued,productRows, fileId.ToString()+".pdf");
        var export = await exportRepository.CreateAsync(new ExportModel
        {
            Document = document,
            Exporter = exporter,
            Id = exportId,
            DateIssued = dateIssued,
            DocumentId = document.Id,
            ExporterId = exporter.Id,
            Type = InvoiceType.Export
        });
        await exportItemRepository.CreateFromProductRowsAsync(export, productRows);
        return Unit.Value;
    }

    private async Task<List<ProductRow>> ConvertToProductRows(List<CreateExportRequestProductDto> products)
    {
        var productRows = products.Select(async p =>
        {
            if (!Guid.TryParse(p.id, out _))
                return null;

            var productId = Guid.Parse(p.id);

            var product = await productRepository.FindByIdAsync(productId);
            if (product is null) return null;

            return new ProductRow
            {
                Name = product.Name,
                Identifier = product.Identifier,
                Price = p.price,
                Quantity = p.quantity,
            };
        });
        var results = await Task.WhenAll(productRows);
        return results.Where(p => p != null).ToList();
    }
}