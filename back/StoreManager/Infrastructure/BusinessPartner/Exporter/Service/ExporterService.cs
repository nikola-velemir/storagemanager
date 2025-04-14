using StoreManager.Infrastructure.BusinessPartner.Base;
using StoreManager.Infrastructure.BusinessPartner.Exporter.Model;
using StoreManager.Infrastructure.BusinessPartner.Exporter.Repository;
using StoreManager.Infrastructure.BusinessPartner.Provider.DTO;
using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Infrastructure.BusinessPartner.Exporter.Service;

public class ExporterService(IExporterRepository repository) : IExporterService
{
    public async Task CreateExporter(ProviderCreateRequestDto request)
    {
        var exporter = new ExporterModel
        {
            Address = request.Address,
            Id = Guid.NewGuid(),
            Name = request.Name,
            PhoneNumber = request.PhoneNumber,
            Type = BusinessPartnerType.Exporter,
            Exports = new List<ExportModel>()
        };
        await repository.Create(exporter);
    }
}