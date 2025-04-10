using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Infrastructure.Product.Model;

namespace StoreManager.Infrastructure.DB.Product;

public class ProductModelConfiguration : IEntityTypeConfiguration<ProductModel>
{
    public void Configure(EntityTypeBuilder<ProductModel> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasMany(p => p.Components).WithOne(pc => pc.Product).HasForeignKey(pc => pc.ProductId);
        
    }
}