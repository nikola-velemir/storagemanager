using MediatR;
using StoreManager.Infrastructure.BusinessPartner.Exporter.Command;
using StoreManager.Infrastructure.BusinessPartner.Exporter.DTO;
using StoreManager.Infrastructure.BusinessPartner.Exporter.Repository;

namespace StoreManager.Infrastructure.BusinessPartner.Exporter.Handler;

public class FindAllExportersQueryHandler(IExporterRepository repository)
    : IRequestHandler<FindAllExportersQuery, FindExporterResponsesDto>
{
    public async Task<FindExporterResponsesDto> Handle(FindAllExportersQuery request,
        CancellationToken cancellationToken)
    {
        var exporters = await repository.FindAll();
        return new FindExporterResponsesDto(
            exporters.Select(e => new FindExporterResponseDto(e.Id, e.Name, e.Address, e.PhoneNumber)).ToList()
        );
    }
}