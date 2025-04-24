using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Domain.Invoice.Export.Model;
using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Infrastructure.Invoice.Export.Configuration;

public class ExportModelConfiguration : IEntityTypeConfiguration<Domain.Invoice.Export.Model.Export>
{
    public void Configure(EntityTypeBuilder<Domain.Invoice.Export.Model.Export> builder)
    {
       builder.ToTable("Exports");

       builder.HasMany<ExportItemModel>(e => e.Items)
           .WithOne(e => e.Export)
           .HasForeignKey(e => e.ExportId);

    }
}