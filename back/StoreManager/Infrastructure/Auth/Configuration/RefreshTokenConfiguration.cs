using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Domain.Auth.Tokens.RefreshToken.Model;

namespace StoreManager.Infrastructure.Auth.Configuration
{
    internal sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenModel>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenModel> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Token).HasMaxLength(256);
            builder.HasIndex(r => r.Token).IsUnique();
            builder.HasOne(r => r.User).WithMany().HasForeignKey(r => r.UserId);
        }
    }
}
