using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Infrastructure.Provider.Model;

namespace StoreManager.Infrastructure.DB.Provider
{
    public class ProviderModelConfiguration : IEntityTypeConfiguration<ProviderModel>
    {
        public void Configure(EntityTypeBuilder<ProviderModel> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.PhoneNumber).IsRequired();
            builder.HasMany(p => p.Invoices).WithOne(i => i.Provider).HasForeignKey(i => i.ProviderId);
        }
    }
}
