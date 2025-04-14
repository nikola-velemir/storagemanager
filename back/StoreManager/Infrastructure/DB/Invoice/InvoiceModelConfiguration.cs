using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Infrastructure.Invoice.Base;
using StoreManager.Infrastructure.Invoice.Export.Model;
using StoreManager.Infrastructure.Invoice.Import.Model;

namespace StoreManager.Infrastructure.DB.Invoice;

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