using StoreManager.Infrastructure.BusinessPartner.Exporter.Model;
using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Infrastructure.BusinessPartner.Exporter.Repository;

public interface IExporterRepository
{
    Task<ExporterModel?> FindById(Guid id);
    Task<ExporterModel> Create(ExporterModel exporter);
}