using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Domain.Product.Blueprint.Model;

namespace StoreManager.Infrastructure.Product.Blueprint.Configuration;

public class ProductBlueprintModelConfiguration : IEntityTypeConfiguration<ProductBlueprint>
{
    public void Configure(EntityTypeBuilder<ProductBlueprint> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Identifier).IsUnique();
        builder.Property(p => p.Description).HasMaxLength(200);
        builder.Property(p => p.Name).HasMaxLength(50);
        builder.Property(p => p.Identifier).HasMaxLength(15);
        builder.HasMany(p => p.Components).WithOne(pc => pc.Product).HasForeignKey(pc => pc.ProductId);
        builder.HasMany(p => p.Batches).WithOne(b => b.Blueprint).HasForeignKey(b => b.BlueprintId);

    }
}