using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Infrastructure.Product.Model;

namespace StoreManager.Infrastructure.DB.Product;

public class ProductComponentsModelConfiguration :IEntityTypeConfiguration<ProductComponentsModel>
{
    public void Configure(EntityTypeBuilder<ProductComponentsModel> builder)
    {
        builder.HasKey(pc => new { pc.ProductId, pc.ComponentId });
        builder.HasOne(pc => pc.Product).WithMany(p => p.Components).HasForeignKey(pc => pc.ProductId);
        builder.HasOne(pc => pc.Component).WithMany(c => c.Products).HasForeignKey(pc => pc.ComponentId);
    }
}