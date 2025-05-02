using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Domain.Product.Batch;
using StoreManager.Domain.Product.Batch.Model;

namespace StoreManager.Infrastructure.Product.Batch.Configuration;

public class ProductBatchConfiguration : IEntityTypeConfiguration<ProductBatch>
{
    public void Configure(EntityTypeBuilder<ProductBatch> builder)
    {
        builder.HasKey(pb => pb.Id);
        builder.HasOne(pb=>pb.Blueprint).WithMany(p=>p.Batches).HasForeignKey(pb=>pb.BlueprintId);
    }
}