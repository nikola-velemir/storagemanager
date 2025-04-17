using StoreManager.Infrastructure.BusinessPartner.Exporter.DTO;
using StoreManager.Infrastructure.BusinessPartner.Provider.DTO;

namespace StoreManager.Infrastructure.BusinessPartner.Exporter.Service;

public interface IExporterService
{
    Task CreateExporter(ProviderCreateRequestDto request);
    Task<FindExporterResponsesDto> FindAll();
}