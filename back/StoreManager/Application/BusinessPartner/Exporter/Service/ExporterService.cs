using StoreManager.Application.BusinessPartner.Exporter.DTO;
using StoreManager.Application.BusinessPartner.Exporter.Repository;
using StoreManager.Application.BusinessPartner.Provider.DTO;
using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Domain.BusinessPartner.Exporter.Model;
using StoreManager.Domain.BusinessPartner.Exporter.Service;
using StoreManager.Domain.BusinessPartner.Shared;
using StoreManager.Domain.Invoice.Export.Model;
using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Application.BusinessPartner.Exporter.Service;

public class ExporterService(IExporterRepository repository) : IExporterService
{
    public async Task CreateExporter(ProviderCreateRequestDto request)
    {
        var exporter = new Domain.BusinessPartner.Exporter.Model.Exporter
        {
            Address = new Address("a","a","a",1,4),
            Id = Guid.NewGuid(),
            Name = request.Name,
            PhoneNumber = request.PhoneNumber,
            Type = BusinessPartnerType.Exporter,
            Exports = new List<Export>()
        };
        await repository.CreateAsync(exporter);
    }

    public async Task<FindExporterResponsesDto> FindAll()
    {
        var exporters = await repository.FindAllAsync();
        return new FindExporterResponsesDto(
            exporters.Select(e => new FindExporterResponseDto(e.Id, e.Name, Utils.FormatAddress(e.Address), e.PhoneNumber)).ToList()
        );
    }
}