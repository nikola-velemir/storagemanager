using MediatR;
using StoreManager.Application.BusinessPartner.Exporter.Command;
using StoreManager.Application.BusinessPartner.Exporter.DTO;
using StoreManager.Application.BusinessPartner.Exporter.Repository;

namespace StoreManager.Application.BusinessPartner.Exporter.Handler;

public class FindExporterProductInvolvementsQueryHandler(IExporterRepository exporterRepository)
    : IRequestHandler<FindExporterProductInvolvementsQuery, ExporterProductInvolvementResponsesDto>
{
    public async Task<ExporterProductInvolvementResponsesDto> Handle(FindExporterProductInvolvementsQuery request,
        CancellationToken cancellationToken)
    {
        var exporters = await exporterRepository.FindAllAsync();

        var exportTasks = exporters.Select(async e =>
        {
            var count = await exporterRepository.FindProductCountForExporterAsync(e);
            return new ExporterProductInvolvementResponseDto(e.Id, e.Name, count);
        }).ToList();
        var results =  (await Task.WhenAll(exportTasks)).ToList();
        return new ExporterProductInvolvementResponsesDto(results);
    }
}