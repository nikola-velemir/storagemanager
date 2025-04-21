using MediatR;
using StoreManager.Application.BusinessPartner.Exporter.Command;
using StoreManager.Application.BusinessPartner.Exporter.DTO;
using StoreManager.Application.BusinessPartner.Exporter.Repository;
using StoreManager.Application.Shared;

namespace StoreManager.Application.BusinessPartner.Exporter.Handler;

public class FindFilteredQueryHandler(IExporterRepository repository)
    : IRequestHandler<FindFilteredQuery, PaginatedResult<ExporterSearchResponseDto>>
{
    public async Task<PaginatedResult<ExporterSearchResponseDto>> Handle(FindFilteredQuery request,
        CancellationToken cancellationToken)
    {
        var result = await repository.FindFiltered(request.ExporterInfo, request.PageNumber, request.PageSize);
        return new PaginatedResult<ExporterSearchResponseDto>
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            Items = result.Items.Select(e => new ExporterSearchResponseDto(
                e.Id,
                e.Name,
                Utils.FormatAddress(e.Address),
                e.PhoneNumber,
                e.Exports.Select(ee => new ExporterSearchExportResponse(
                    ee.Id, ee.DateIssued)).ToList())).ToList()
        };
    }
}