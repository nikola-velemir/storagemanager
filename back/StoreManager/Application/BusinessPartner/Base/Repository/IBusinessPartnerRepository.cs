using StoreManager.Domain.BusinessPartner.Base.Model;

namespace StoreManager.Application.BusinessPartner.Base.Repository;

public interface IBusinessPartnerRepository
{
    public Task<Domain.BusinessPartner.Base.Model.BusinessPartner> CreateAsync(Domain.BusinessPartner.Base.Model.BusinessPartner businessPartner);
    Task<Domain.BusinessPartner.Base.Model.BusinessPartner?> FindById(Guid id);
}