using StoreManager.Domain.BusinessPartner.Exporter.Model;

namespace StoreManager.Application.BusinessPartner.Exporter.Repository;

public interface IExporterRepository
{
    Task<ExporterModel?> FindById(Guid id);
    Task<ExporterModel> Create(ExporterModel exporter);
    Task<List<ExporterModel>> FindAll();
    
    Task<(ICollection<ExporterModel> Items, int TotalCount)> FindFiltered(string? exporterInfo, int pageNumber, int pageSize);
}