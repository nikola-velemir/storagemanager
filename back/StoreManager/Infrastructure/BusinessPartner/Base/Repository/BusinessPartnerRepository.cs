using Microsoft.EntityFrameworkCore;
using StoreManager.Application.BusinessPartner.Base.Repository;
using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Infrastructure.Context;
using StoreManager.Infrastructure.DB;

namespace StoreManager.Infrastructure.BusinessPartner.Base.Repository;

public class BusinessPartnerRepository(WarehouseDbContext context) : IBusinessPartnerRepository
{
    private readonly DbSet<BusinessPartnerModel> _businessPartners = context.BusinessPartners;

    public async Task<BusinessPartnerModel> CreateAsync(BusinessPartnerModel businessPartner)
    {
        var savedInstance = await _businessPartners.AddAsync(businessPartner);
        await context.SaveChangesAsync();
        return savedInstance.Entity;
    }
}