using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.MechanicalComponent.Model;

namespace StoreManager.Infrastructure.MechanicalComponent.Configuration
{
    public class MechanicalComponentModelConfiguration : IEntityTypeConfiguration<MechanicalComponentModel>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MechanicalComponentModel> builder)
        {
            builder.HasKey(mc => mc.Id);
            builder.HasAlternateKey(mc => mc.Identifier);
            builder.Property(mc=>mc.Identifier).HasMaxLength(15);
            builder.HasMany(mc => mc.Items).WithOne(i => i.Component);
        }
    }
}
