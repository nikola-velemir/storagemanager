using MediatR;
using StoreManager.Application.Invoice.Export.Command;
using StoreManager.Application.Invoice.Export.DTO;
using StoreManager.Application.Invoice.Export.Repository;
using StoreManager.Application.Shared;
using StoreManager.Domain.Invoice.Export.Specification;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Application.Invoice.Export.Handler;

public class ExportSearchQueryHandler(IExportRepository repository)
    : IRequestHandler<ExportSearchQuery, PaginatedResult<ExportSearchResponseDto>>
{
    public async Task<PaginatedResult<ExportSearchResponseDto>> Handle(ExportSearchQuery request,
        CancellationToken cancellationToken)
    {
        Guid? exporterId = null;
        if (Guid.TryParse(request.ExporterId, out _))
            exporterId = Guid.Parse(request.ExporterId);
        DateOnly? date = null;
        if(DateOnly.TryParse(request.Date, out _))
            date = DateOnly.Parse(request.Date);
        
        var result = await repository.FindFilteredAsync(new FindFilteredExportsSpecification(), exporterId,request.ProductInfo, date, request.PageNumber, request.PageSize);
        return new PaginatedResult<ExportSearchResponseDto>
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
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