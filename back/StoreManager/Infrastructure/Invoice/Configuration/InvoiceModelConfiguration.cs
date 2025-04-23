using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Domain.Invoice.Base.Model;
using StoreManager.Infrastructure.Invoice.Base;

namespace StoreManager.Infrastructure.Invoice.Configuration;

public class InvoiceModelConfiguration : IEntityTypeConfiguration<InvoiceModel>
{
    public void Configure(EntityTypeBuilder<InvoiceModel> builder)
    {
        builder.ToTable("Invoices");
        
        builder.HasKey(i => i.Id);
        
        builder.HasOne(i => i.Document)
            .WithOne().HasForeignKey<InvoiceModel>(i => i.DocumentId);
        
    }
}