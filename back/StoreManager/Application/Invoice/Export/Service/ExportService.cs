using StoreManager.Application.BusinessPartner.Exporter.Repository;
using StoreManager.Application.Invoice.Export.DTO;
using StoreManager.Application.Invoice.Export.Repository;
using StoreManager.Application.Shared;
using StoreManager.Domain.Invoice.Export.Model;
using StoreManager.Domain.Invoice.Export.Specification;
using StoreManager.Infrastructure.Invoice.Base;
using StoreManager.Infrastructure.Invoice.Export.Model;
using StoreManager.Infrastructure.Invoice.Export.Service;
using StoreManager.Infrastructure.MiddleWare.Exceptions;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Application.Invoice.Export.Service;

public class ExportService(IExportRepository repository, IExporterRepository exporterRepository) : IExportService
{
    public async Task Create(CreateExportRequestDto request)
    {
        if (!Guid.TryParse(request.providerId, out _))
        {
            throw new ArgumentException("Invalid provider id");
        }

        var exporterId = Guid.Parse(request.providerId);
        var exporter = await exporterRepository.FindById((exporterId)) ?? throw new NotFoundException("AA");

        var export = await repository.CreateAsync(new ExportModel
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

    public async Task<PaginatedResult<ExportSearchResponseDto>> FindFiltered(string? ExporterId, string? ProductInfo, string? Date, int PageNumber, int PageSize)
    {
        Guid? exporterId = null;
        if (Guid.TryParse(ExporterId, out _))
            exporterId = Guid.Parse(ExporterId);
        DateOnly? date = null;
        if(DateOnly.TryParse(Date, out _))
            date = DateOnly.Parse(Date);
        
        var result = await repository.FindFilteredAsync(new FindFilteredExportsSpecification(), exporterId,ProductInfo, date, PageNumber, PageSize);
        return new PaginatedResult<ExportSearchResponseDto>
        {
            PageNumber = PageNumber,
            PageSize = PageSize,
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