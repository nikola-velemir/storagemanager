using Microsoft.EntityFrameworkCore;
using StoreManager.Domain.MechanicalComponent.Model;

namespace StoreManager.Infrastructure.MechanicalComponent.Configuration
{
    public class MechanicalComponentModelConfiguration : IEntityTypeConfiguration<Domain.MechanicalComponent.Model.MechanicalComponent>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.MechanicalComponent.Model.MechanicalComponent> builder)
        {
            builder.HasKey(mc => mc.Id);
            builder.HasAlternateKey(mc => mc.Identifier);
            builder.Property(mc=>mc.Identifier).HasMaxLength(15);
            builder.HasMany(mc => mc.Items).WithOne(i => i.Component);

            builder.HasCheckConstraint("CK_MECHANICAL_COMPONENT_STOCK_NONNEGATIVE", "\"CurrentStock\" >= 0");
        }
    }
}
