using Microsoft.EntityFrameworkCore;
using StoreManager.Application.BusinessPartner.Base.Repository;
using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Infrastructure.Context;
using StoreManager.Infrastructure.DB;

namespace StoreManager.Infrastructure.BusinessPartner.Base.Repository;

public class BusinessPartnerRepository(WarehouseDbContext context) : IBusinessPartnerRepository
{
    private readonly DbSet<Domain.BusinessPartner.Base.Model.BusinessPartner> _businessPartners = context.BusinessPartners;
    public async Task<Domain.BusinessPartner.Base.Model.BusinessPartner> CreateAsync(Domain.BusinessPartner.Base.Model.BusinessPartner businessPartner)
    {
        var savedInstance = await _businessPartners.AddAsync(businessPartner);
        await context.SaveChangesAsync();
        return savedInstance.Entity;
    }
    public Task<Domain.BusinessPartner.Base.Model.BusinessPartner?> FindById(Guid id)
    {
        return _businessPartners.FirstOrDefaultAsync(bp => bp.Id.Equals(id));
    }
}