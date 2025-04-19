using StoreManager.Application.BusinessPartner.Exporter.DTO;
using StoreManager.Application.BusinessPartner.Provider.DTO;

namespace StoreManager.Domain.BusinessPartner.Exporter.Service;

public interface IExporterService
{
    Task CreateExporter(ProviderCreateRequestDto request);
    Task<FindExporterResponsesDto> FindAll();
}