using MediatR;
using StoreManager.Application.BusinessPartner.Exporter.Command;
using StoreManager.Application.BusinessPartner.Exporter.DTO;
using StoreManager.Application.BusinessPartner.Exporter.Repository;

namespace StoreManager.Application.BusinessPartner.Exporter.Handler;

public class FindAllExportersQueryHandler(IExporterRepository repository)
    : IRequestHandler<FindAllExportersQuery, FindExporterResponsesDto>
{
    public async Task<FindExporterResponsesDto> Handle(FindAllExportersQuery request,
        CancellationToken cancellationToken)
    {
        var exporters = await repository.FindAllAsync();
        return new FindExporterResponsesDto(
            exporters.Select(e =>
                new FindExporterResponseDto(e.Id, e.Name, Utils.FormatAddress(e.Address), e.PhoneNumber)).ToList()
        );
    }
}