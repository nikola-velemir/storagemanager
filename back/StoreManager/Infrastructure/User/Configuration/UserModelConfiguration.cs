using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Infrastructure.User.Configuration
{
    public class UserModelConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Username).IsUnique();
            builder.Property(u => u.Username).IsRequired();
            builder.Property(u => u.Password).IsRequired();
        }
    }
}
