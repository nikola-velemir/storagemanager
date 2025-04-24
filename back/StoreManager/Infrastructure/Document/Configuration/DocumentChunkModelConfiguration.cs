using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Domain.Document.Model;

namespace StoreManager.Infrastructure.Document.Configuration
{
    public class DocumentChunkModelConfiguration : IEntityTypeConfiguration<DocumentChunk>
    {
        public void Configure(EntityTypeBuilder<DocumentChunk> builder)
        {
            builder.HasKey(d => d.Id);
            builder.HasIndex(d => d.SupaBasePath).IsUnique();
            builder.HasOne(d => d.Document).WithMany(d=>d.Chunks).HasForeignKey(d => d.DocumentId);
        }
    }
}
