using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Model;
using StoreManager.Infrastructure.Document.Model;
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
        public DbSet<DocumentChunkModel> DocumentChunks { get; set; }

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
            modelBuilder.ApplyConfiguration(new DocumentChunkModelConfiguration());
            modelBuilder.Entity<UserModel>().HasIndex(u => u.Username).IsUnique();
        }
    }
}
