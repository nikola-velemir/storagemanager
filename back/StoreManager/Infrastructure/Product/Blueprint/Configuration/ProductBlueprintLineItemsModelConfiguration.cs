using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Domain.Product.Blueprint.Model;

namespace StoreManager.Infrastructure.Product.Blueprint.Configuration;

public class ProductBlueprintLineItemsModelConfiguration :IEntityTypeConfiguration<ProductBlueprintLineItems>
{
    public void Configure(EntityTypeBuilder<ProductBlueprintLineItems> builder)
    {
        builder.HasKey(pc => new { pc.ProductId, pc.ComponentId });
        builder.HasOne(pc => pc.Product).WithMany(p => p.Components).HasForeignKey(pc => pc.ProductId);
        builder.HasOne(pc => pc.Component).WithMany(c => c.Products).HasForeignKey(pc => pc.ComponentId);
    }
}