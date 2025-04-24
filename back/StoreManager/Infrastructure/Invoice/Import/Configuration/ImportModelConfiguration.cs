using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Infrastructure.Invoice.Import.Model;

namespace StoreManager.Infrastructure.Invoice.Import.Configuration
{
    public class ImportModelConfiguration : IEntityTypeConfiguration<Model.Import>
    {
        public void Configure(EntityTypeBuilder<Model.Import> builder)
        {
            builder.ToTable("Imports");
            
            builder.HasOne(i => i.Provider).WithMany().HasForeignKey(i => i.ProviderId);
            builder.HasMany(i => i.Items).WithOne(ii => ii.Import);
        }
    }
}
