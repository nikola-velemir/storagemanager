using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Domain.BusinessPartner.Exporter.Model;
using StoreManager.Domain.BusinessPartner.Provider.Model;

namespace StoreManager.Infrastructure.DB.BusinessPartner;

public class BusinessPartnerModelConfiguration : IEntityTypeConfiguration<BusinessPartnerModel>
{
    public void Configure(EntityTypeBuilder<BusinessPartnerModel> builder)
    {
        
        builder.ToTable("BusinessPartners");
        
        builder.HasKey(p => p.Id);
        builder.Property(p => p.PhoneNumber).IsRequired();

        builder.HasDiscriminator<BusinessPartnerType>("Type")
            .HasValue<ExporterModel>(BusinessPartnerType.Exporter)
            .HasValue<ProviderModel>(BusinessPartnerType.Provider);
    }
}