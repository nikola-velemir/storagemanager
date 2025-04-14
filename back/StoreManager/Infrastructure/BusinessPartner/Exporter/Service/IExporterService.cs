using StoreManager.Infrastructure.BusinessPartner.Provider.DTO;

namespace StoreManager.Infrastructure.BusinessPartner.Exporter.Service;

public interface IExporterService
{
    Task CreateExporter(ProviderCreateRequestDto request);
}