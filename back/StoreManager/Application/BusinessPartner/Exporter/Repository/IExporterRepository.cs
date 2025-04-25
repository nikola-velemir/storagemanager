using System.Collections;
using StoreManager.Domain.BusinessPartner.Exporter.Model;

namespace StoreManager.Application.BusinessPartner.Exporter.Repository;

public interface IExporterRepository
{
    Task<Domain.BusinessPartner.Exporter.Model.Exporter?> FindById(Guid id);
    Task<Domain.BusinessPartner.Exporter.Model.Exporter> CreateAsync(Domain.BusinessPartner.Exporter.Model.Exporter exporter);
    Task<List<Domain.BusinessPartner.Exporter.Model.Exporter>> FindAllAsync();
    
    Task<(ICollection<Domain.BusinessPartner.Exporter.Model.Exporter> Items, int TotalCount)> FindFiltered(string? exporterInfo, int pageNumber, int pageSize);
    Task<int> FindInvoiceCountForProviderAsync(Domain.BusinessPartner.Exporter.Model.Exporter exporter);
    Task<int> FindProductCountForExporterAsync(Domain.BusinessPartner.Exporter.Model.Exporter exporter);
}