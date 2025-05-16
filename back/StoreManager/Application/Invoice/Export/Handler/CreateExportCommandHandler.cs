using MediatR;
using StoreManager.Application.BusinessPartner.Base.Errors;
using StoreManager.Application.BusinessPartner.Exporter.Repository;
using StoreManager.Application.Common;
using StoreManager.Application.Invoice.Base.Error;
using StoreManager.Application.Invoice.Export.Command;
using StoreManager.Application.Invoice.Export.DTO;
using StoreManager.Application.Invoice.Export.Repository;
using StoreManager.Application.Product.Blueprint.Repository;
using StoreManager.Domain;
using StoreManager.Domain.Document.Service;
using StoreManager.Infrastructure.Invoice.Base;

namespace StoreManager.Application.Invoice.Export.Handler;

public class CreateExportCommandHandler(
    IUnitOfWork unitOfWork,
    IExporterRepository exporterRepository,
    IExportRepository exportRepository,
    IProductBlueprintRepository productBlueprintRepository,
    IDocumentService documentService)
    : IRequestHandler<CreateExportCommand, Result>
{
    public async Task<Result> Handle(CreateExportCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.ProviderId, out _))
            return BusinessPartnerErrors.PartnerIdParseError;

        var exporterId = Guid.Parse(request.ProviderId);
        var exporter = await exporterRepository.FindById((exporterId));
        if (exporter == null)
            return BusinessPartnerErrors.PartnerNotFoundError;

        var exportId = Guid.NewGuid();

        var fileId = Guid.NewGuid();
        var productRows = await ConvertToProductRows(request.Products);

        var dateIssued = DateOnly.FromDateTime(DateTime.UtcNow);

        var document =
            await documentService.UploadExport(exporter, dateIssued, productRows, fileId.ToString() + ".pdf");
        var export = await exportRepository.CreateAsync(new Domain.Invoice.Export.Model.Export
        {
            Document = document,
            Exporter = exporter,
            Id = exportId,
            DateIssued = dateIssued,
            DocumentId = document.Id,
            ExporterId = exporter.Id,
            Type = InvoiceType.Export
        });
        try
        {
            await exportRepository.CreateFromProductRowsAsync(export, productRows);
        }
        catch (ArgumentOutOfRangeException)
        {
            return InvoiceErrors.QuantityInsufficient;
        }

        await unitOfWork.CommitAsync(cancellationToken);
        return Result.Success();
    }

    private async Task<List<ProductRow>> ConvertToProductRows(List<CreateExportRequestProductDto> products)
    {
        var productRows = products.Select(async p =>
        {
            if (!Guid.TryParse(p.id, out _))
                return null;

            var productId = Guid.Parse(p.id);

            var product = await productBlueprintRepository.FindByIdAsync(productId);
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