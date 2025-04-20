using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Infrastructure.Invoice.Export.Configuration;

public class ExportItemModelConfiguration : IEntityTypeConfiguration<ExportItemModel>
{
    public void Configure(EntityTypeBuilder<ExportItemModel> builder)
    {
        builder.HasKey(e => new { e.ExportId, e.ProductId });

        builder.HasOne(ei => ei.Export)
            .WithMany(e => e.Items)
            .HasForeignKey(ei => ei.ExportId);

        builder.HasOne(ei => ei.Product)
            .WithMany(p => p.Exports)
            .HasForeignKey(ei => ei.ProductId);
        

        
    }
}