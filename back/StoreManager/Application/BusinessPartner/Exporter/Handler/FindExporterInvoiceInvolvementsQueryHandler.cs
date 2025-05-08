using MediatR;
using StoreManager.Application.BusinessPartner.Exporter.Command;
using StoreManager.Application.BusinessPartner.Exporter.DTO;
using StoreManager.Application.BusinessPartner.Exporter.Repository;
using StoreManager.Application.Common;

namespace StoreManager.Application.BusinessPartner.Exporter.Handler;

public class FindExporterInvoiceInvolvementsQueryHandler(IExporterRepository exporterRepository) :
    IRequestHandler<FindInvoiceInvolvementsQuery, Result<ExporterInvoiceInvolvementResponsesDto>>
{
    public async Task<Result<ExporterInvoiceInvolvementResponsesDto>> Handle(FindInvoiceInvolvementsQuery request,
        CancellationToken cancellationToken)
    {
        var exporters = await exporterRepository.FindAllAsync();
        var responses = new List<ExporterInvoiceInvolvementResponseDto>();
        foreach (var exporter in exporters)
        {
            var count = await exporterRepository.FindInvoiceCountForProviderAsync(exporter);
            responses.Add(new ExporterInvoiceInvolvementResponseDto(exporter.Id, exporter.Name, count));
        }
        var response = new ExporterInvoiceInvolvementResponsesDto(responses);
        return Result.Success(response);
    }
}