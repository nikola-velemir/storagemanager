using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Domain.BusinessPartner.Exporter.Model;

namespace StoreManager.Infrastructure.BusinessPartner.Exporter.Configuration;

public class ExporterModelConfiguration : IEntityTypeConfiguration<Domain.BusinessPartner.Exporter.Model.Exporter>
{
    public void Configure(EntityTypeBuilder<Domain.BusinessPartner.Exporter.Model.Exporter> builder)
    {
        builder.HasMany(p => p.Exports)
            .WithOne(i => i.Exporter)
            .HasForeignKey(i => i.ExporterId);
    }
}