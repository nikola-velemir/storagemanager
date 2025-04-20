using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Infrastructure.Invoice.Export.Configuration;

public class ExportModelConfiguration : IEntityTypeConfiguration<ExportModel>
{
    public void Configure(EntityTypeBuilder<ExportModel> builder)
    {
       builder.ToTable("Exports");

       builder.HasMany<ExportItemModel>(e => e.Items)
           .WithOne(e => e.Export)
           .HasForeignKey(e => e.ExportId);

    }
}