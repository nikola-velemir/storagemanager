using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Domain.User.Model;
using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Infrastructure.User.Configuration
{
    public class UserModelConfiguration : IEntityTypeConfiguration<Domain.User.Model.User>
    {
        public void Configure(EntityTypeBuilder<Domain.User.Model.User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Username).IsUnique();
            builder.Property(u => u.Username).HasMaxLength(20);
            builder.Property(u => u.Password).HasMaxLength(256);
            builder.Property(u => u.FirstName).HasMaxLength(20);
            builder.Property(u => u.LastName).HasMaxLength(20);
            builder.Property(u => u.Username).IsRequired();
            builder.Property(u => u.Password).IsRequired();
        }
    }
}