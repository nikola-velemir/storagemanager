using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Infrastructure.Invoice.Base;
using StoreManager.Infrastructure.Invoice.Export.Model;

namespace StoreManager.Infrastructure.DB.Invoice.Export;

public class ExportModelConfiguration : IEntityTypeConfiguration<ExportModel>
{
    public void Configure(EntityTypeBuilder<ExportModel> builder)
    {
       builder.ToTable("Exports");
    }
}