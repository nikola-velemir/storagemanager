using StoreManager.Application.BusinessPartner.Exporter.DTO;
using StoreManager.Application.BusinessPartner.Exporter.Repository;
using StoreManager.Application.BusinessPartner.Provider.DTO;
using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Domain.BusinessPartner.Exporter.Model;
using StoreManager.Domain.BusinessPartner.Exporter.Service;
using StoreManager.Domain.BusinessPartner.Shared;
using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Application.BusinessPartner.Exporter.Service;

public class ExporterService(IExporterRepository repository) : IExporterService
{
    public async Task CreateExporter(ProviderCreateRequestDto request)
    {
        var exporter = new ExporterModel
        {
            Address = new Address("a","a","a",1,4),
            Id = Guid.NewGuid(),
            Name = request.Name,
            PhoneNumber = request.PhoneNumber,
            Type = BusinessPartnerType.Exporter,
            Exports = new List<ExportModel>()
        };
        await repository.Create(exporter);
    }

    public async Task<FindExporterResponsesDto> FindAll()
    {
        var exporters = await repository.FindAll();
        return new FindExporterResponsesDto(
            exporters.Select(e => new FindExporterResponseDto(e.Id, e.Name, Utils.FormatAddress(e.Address), e.PhoneNumber)).ToList()
        );
    }
}