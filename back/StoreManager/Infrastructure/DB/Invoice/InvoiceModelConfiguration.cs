using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Infrastructure.Invoice.Model;

namespace StoreManager.Infrastructure.DB.Invoice
{
    public class InvoiceModelConfiguration : IEntityTypeConfiguration<InvoiceModel>
    {
        public void Configure(EntityTypeBuilder<InvoiceModel> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasOne(i => i.Document).WithOne().HasForeignKey<InvoiceModel>(i => i.DocumentId);
            builder.HasOne(i => i.Provider).WithMany().HasForeignKey(i => i.ProviderId);
            builder.HasMany(i => i.Items).WithOne(ii => ii.Invoice);
        }
    }
}
