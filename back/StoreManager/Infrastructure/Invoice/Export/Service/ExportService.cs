using StoreManager.Infrastructure.BusinessPartner.Exporter.Model;
using StoreManager.Infrastructure.BusinessPartner.Exporter.Repository;
using StoreManager.Infrastructure.Invoice.Base;
using StoreManager.Infrastructure.Invoice.Export.DTO;
using StoreManager.Infrastructure.Invoice.Export.Model;
using StoreManager.Infrastructure.Invoice.Export.Repository;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Invoice.Export.Service;

public class ExportService(IExportRepository repository, IExporterRepository exporterRepository) : IExportService
{
    public async Task Create(CreateExportRequestDto request)
    {
        if (!Guid.TryParse(request.providerId, out _))
        {
            throw new ArgumentException("Invalid provider id");
        }

        var exporterId = Guid.Parse(request.providerId);
        var exporter = await exporterRepository.FindById((exporterId));

        var export = await repository.Create(new ExportModel
        {
            Document = null,
            Exporter = exporter,
            Id = Guid.NewGuid(),
            DateIssued = DateOnly.FromDateTime(DateTime.UtcNow),
            DocumentId = Guid.Empty,
            ExporterId = exporter.Id,
            Type = InvoiceType.Export,
            Items = new List<ExportItemModel>()
        });
    }

    public async Task<PaginatedResult<ExportSearchResponseDto>> FindFiltered(int pageNumber, int pageSize)
    {
        var result = await repository.FindFiltered(pageNumber, pageSize);
        return new PaginatedResult<ExportSearchResponseDto>
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Items = result.Items.Select(e =>
                new ExportSearchResponseDto(
                    e.Id,
                    e.DateIssued,
                    e.Exporter.Name,
                    e.Items.Select(i =>
                        new ExportSearchProductResponseDto(i.ProductId, i.Product.Name, i.Product.Identifier,
                            i.Quantity,
                            i.PricePerPiece)).ToList()
                )
            ).ToList(),
            TotalCount = result.TotalCount
        };
    }
}