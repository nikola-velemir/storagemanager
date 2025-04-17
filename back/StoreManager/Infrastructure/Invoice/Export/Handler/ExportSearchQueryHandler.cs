using MediatR;
using StoreManager.Infrastructure.Invoice.Export.Command;
using StoreManager.Infrastructure.Invoice.Export.DTO;
using StoreManager.Infrastructure.Invoice.Export.Repository;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Invoice.Export.Handler;

public class ExportSearchQueryHandler(IExportRepository repository)
    : IRequestHandler<ExportSearchQuery, PaginatedResult<ExportSearchResponseDto>>
{
    public async Task<PaginatedResult<ExportSearchResponseDto>> Handle(ExportSearchQuery request,
        CancellationToken cancellationToken)
    {
        var result = await repository.FindFiltered(request.PageNumber, request.PageSize);
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
                        new ExportSearchProductResponseDto(i.Product.Name, i.Product.Identifier, i.Quantity,
                            i.PricePerPiece)).ToList()
                )
            ).ToList(),
            TotalCount = result.TotalCount
        };
    }
}