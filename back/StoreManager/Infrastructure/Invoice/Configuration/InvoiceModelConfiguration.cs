using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Domain.Invoice.Base.Model;
using StoreManager.Infrastructure.Invoice.Base;

namespace StoreManager.Infrastructure.Invoice.Configuration;

public class InvoiceModelConfiguration : IEntityTypeConfiguration<Domain.Invoice.Base.Model.Invoice>
{
    public void Configure(EntityTypeBuilder<Domain.Invoice.Base.Model.Invoice> builder)
    {
        builder.ToTable("Invoices");
        
        builder.HasKey(i => i.Id);
        
        builder.HasOne(i => i.Document)
            .WithOne().HasForeignKey<Domain.Invoice.Base.Model.Invoice>(i => i.DocumentId);
        
    }
}