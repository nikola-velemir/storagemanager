using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Infrastructure.Invoice.Model;

namespace StoreManager.Infrastructure.DB
{
    public class InvoiceModelConfiguration : IEntityTypeConfiguration<InvoiceModel>
    {
        public void Configure(EntityTypeBuilder<InvoiceModel> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasOne(i => i.Document).WithOne().HasForeignKey<InvoiceModel>(i => i.DocumentId);
        }
    }
}
