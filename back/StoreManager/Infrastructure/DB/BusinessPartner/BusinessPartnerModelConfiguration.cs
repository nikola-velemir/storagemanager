using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Infrastructure.BusinessPartner.Base;
using StoreManager.Infrastructure.BusinessPartner.Exporter.Model;
using StoreManager.Infrastructure.BusinessPartner.Provider.Model;
using StoreManager.Infrastructure.Invoice.Export.Model;

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