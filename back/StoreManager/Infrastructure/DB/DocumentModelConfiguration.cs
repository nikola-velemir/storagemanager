using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Infrastructure.Document.Model;

namespace StoreManager.Infrastructure.DB
{
    internal sealed class DocumentModelConfiguration : IEntityTypeConfiguration<DocumentModel>
    {
        public void Configure(EntityTypeBuilder<DocumentModel> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Type).IsRequired();
            builder.Property(r => r.Date).IsRequired();
            builder.HasMany(r => r.Chunks).WithOne().HasForeignKey(r => r.DocumentId);
        }
    }
}
