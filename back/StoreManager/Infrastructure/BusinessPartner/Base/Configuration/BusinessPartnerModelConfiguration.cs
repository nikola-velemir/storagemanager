using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Domain.BusinessPartner.Exporter.Model;
using StoreManager.Domain.BusinessPartner.Provider.Model;

namespace StoreManager.Infrastructure.BusinessPartner.Base.Configuration;

public class BusinessPartnerModelConfiguration : IEntityTypeConfiguration<Domain.BusinessPartner.Base.Model.BusinessPartner>
{
    public void Configure(EntityTypeBuilder<Domain.BusinessPartner.Base.Model.BusinessPartner> builder)
    {
        builder.ToTable("BusinessPartners");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.PhoneNumber).IsRequired();
        builder.Property(p => p.PhoneNumber).HasMaxLength(20);
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Name).HasMaxLength(50);

        builder.OwnsOne(b => b.Address, a =>
        {
            a.Property(p => p.City).HasColumnName("City");
            a.Property(p => p.Street).HasColumnName("Street");
            a.Property(p => p.StreetNumber).HasColumnName("StreetNumber");
            a.Property(p => p.Latitude).HasColumnName("Latitude").IsRequired();
            a.Property(p => p.Longitude).HasColumnName("Longitude").IsRequired();
        });

        builder.HasDiscriminator<BusinessPartnerType>("Type")
            .HasValue<Domain.BusinessPartner.Base.Model.BusinessPartner>(BusinessPartnerType.None)
            .HasValue<Domain.BusinessPartner.Exporter.Model.Exporter>(BusinessPartnerType.Exporter)
            .HasValue<Domain.BusinessPartner.Provider.Model.Provider>(BusinessPartnerType.Provider);
    }
}