using MediatR;
using StoreManager.Application.BusinessPartner.Exporter.Command;
using StoreManager.Application.BusinessPartner.Exporter.DTO;
using StoreManager.Application.BusinessPartner.Exporter.Repository;
using StoreManager.Application.Common;

namespace StoreManager.Application.BusinessPartner.Exporter.Handler;

public class FindExporterProductInvolvementsQueryHandler(IExporterRepository exporterRepository)
    : IRequestHandler<FindExporterProductInvolvementsQuery, Result<ExporterProductInvolvementResponsesDto>>
{
    public async Task<Result<ExporterProductInvolvementResponsesDto>> Handle(
        FindExporterProductInvolvementsQuery request,
        CancellationToken cancellationToken)
    {
        var exporters = await exporterRepository.FindAllAsync();

        var exportTasks = exporters.Select(async e =>
        {
            var count = await exporterRepository.FindProductCountForExporterAsync(e);
            return new ExporterProductInvolvementResponseDto(e.Id, e.Name, count);
        }).ToList();
        var results = (await Task.WhenAll(exportTasks)).ToList();
        var response = new ExporterProductInvolvementResponsesDto(results);
        return Result.Success(response);
    }
}