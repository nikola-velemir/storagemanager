using MediatR;
using StoreManager.Application.BusinessPartner.Exporter.Command;
using StoreManager.Application.BusinessPartner.Exporter.DTO;
using StoreManager.Application.BusinessPartner.Exporter.Repository;
using StoreManager.Application.Common;

namespace StoreManager.Application.BusinessPartner.Exporter.Handler;

public class FindAllExportersQueryHandler(IExporterRepository repository)
    : IRequestHandler<FindAllExportersQuery, Result<FindExporterResponsesDto>>
{
    public async Task<Result<FindExporterResponsesDto>> Handle(FindAllExportersQuery request,
        CancellationToken cancellationToken)
    {
        var exporters = await repository.FindAllAsync();
        var response = new FindExporterResponsesDto(
            exporters.Select(e =>
                new FindExporterResponseDto(e.Id, e.Name, Utils.FormatAddress(e.Address), e.PhoneNumber)).ToList()
        );
        return Result.Success(response);
    }
}