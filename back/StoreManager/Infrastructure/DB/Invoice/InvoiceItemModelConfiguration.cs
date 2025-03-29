using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Infrastructure.Invoice.Model;

namespace StoreManager.Infrastructure.DB.Invoice
{
    public class InvoiceItemModelConfiguration : IEntityTypeConfiguration<InvoiceItemModel>
    {
        public void Configure(EntityTypeBuilder<InvoiceItemModel> builder)
        {
            builder.HasKey(ii => new { ii.InvoiceId, ii.ComponentId });
            builder.HasOne(ii => ii.Invoice).WithMany(i => i.Items).HasForeignKey(ii => ii.InvoiceId);
            builder.HasOne(ii => ii.Component).WithMany().HasForeignKey(ii => ii.ComponentId);
        }
    }
}
