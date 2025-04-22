using StoreManager.Domain.BusinessPartner.Base.Model;

namespace StoreManager.Application.BusinessPartner.Base.Repository;

public interface IBusinessPartnerRepository
{
    public Task<BusinessPartnerModel> CreateAsync(BusinessPartnerModel businessPartner);
    Task<BusinessPartnerModel?> FindById(Guid id);
}