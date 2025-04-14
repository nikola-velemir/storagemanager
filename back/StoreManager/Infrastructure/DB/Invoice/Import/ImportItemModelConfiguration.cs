using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Infrastructure.Invoice.Import.Model;

namespace StoreManager.Infrastructure.DB.Invoice.Import
{
    public class ImportItemModelConfiguration : IEntityTypeConfiguration<ImportItemModel>
    {
        public void Configure(EntityTypeBuilder<ImportItemModel> builder)
        {
            builder.HasKey(ii => new { ii.InvoiceId, ii.ComponentId });
            builder.HasOne(ii => ii.Import).WithMany(i => i.Items).HasForeignKey(ii => ii.InvoiceId);
            builder.HasOne(ii => ii.Component).WithMany(mc=>mc.Items).HasForeignKey(ii => ii.ComponentId);
        }
    }
}
