using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Infrastructure.BusinessPartner.Exporter.Model;
using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Infrastructure.DB.BusinessPartner.Exporter;

public class ExporterModelConfiguration : IEntityTypeConfiguration<ExporterModel>
{
    public void Configure(EntityTypeBuilder<ExporterModel> builder)
    {
        builder.HasMany(p => p.Exports)
            .WithOne(i => i.Exporter)
            .HasForeignKey(i => i.ExporterId);
    }
}