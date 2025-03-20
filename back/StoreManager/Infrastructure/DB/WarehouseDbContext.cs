using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Model;
using StoreManager.Infrastructure.Document;
using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Infrastructure.DB
{
    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext() { }
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options) { }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<DocumentModel> Documents { get; set; }

        public DbSet<RefreshTokenModel> RefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json")
                     .Build();

                var connectionString = config.GetConnectionString("PostgresConnection");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.ApplyConfiguration(new DocumentModelConfiguration());
            modelBuilder.Entity<UserModel>().HasIndex(u => u.Username).IsUnique();
        }
    }
}
