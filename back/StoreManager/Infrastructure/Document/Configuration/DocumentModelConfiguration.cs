using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Domain.Document.Model;

namespace StoreManager.Infrastructure.Document.Configuration
{
    internal sealed class DocumentModelConfiguration : IEntityTypeConfiguration<DocumentModel>
    {
        public void Configure(EntityTypeBuilder<DocumentModel> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Type).IsRequired();
            builder.Property(r => r.Date).IsRequired();
            builder.HasIndex(r => r.FileName).IsUnique();
            builder.Property(r => r.FileName).IsRequired();
            builder.Property(r => r.FileName).HasMaxLength(50);
            builder.HasMany(r => r.Chunks).WithOne(c=>c.Document).HasForeignKey(r => r.DocumentId);
        }
    }
}
