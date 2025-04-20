using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Infrastructure.Invoice.Import.Model;

namespace StoreManager.Infrastructure.Invoice.Import.Configuration
{
    public class ImportModelConfiguration : IEntityTypeConfiguration<ImportModel>
    {
        public void Configure(EntityTypeBuilder<ImportModel> builder)
        {
            builder.ToTable("Imports");
            
            builder.HasOne(i => i.Provider).WithMany().HasForeignKey(i => i.ProviderId);
            builder.HasMany(i => i.Items).WithOne(ii => ii.Import);
        }
    }
}
