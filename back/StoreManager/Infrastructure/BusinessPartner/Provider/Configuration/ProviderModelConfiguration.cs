using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Domain.BusinessPartner.Provider.Model;

namespace StoreManager.Infrastructure.DB.BusinessPartner.Provider
{
    public class ProviderModelConfiguration : IEntityTypeConfiguration<Domain.BusinessPartner.Provider.Model.Provider>
    {
        public void Configure(EntityTypeBuilder<Domain.BusinessPartner.Provider.Model.Provider> builder)
        {
            builder.HasMany(p => p.Imports)
                .WithOne(i => i.Provider)
                .HasForeignKey(i => i.ProviderId);
        }
    }
}
