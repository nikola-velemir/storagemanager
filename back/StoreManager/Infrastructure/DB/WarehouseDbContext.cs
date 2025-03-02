using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.User.Model;
using System.Security.Cryptography.X509Certificates;

namespace StoreManager.Infrastructure.DB
{
    public class WarehouseDbContext:DbContext
    {
        public WarehouseDbContext() { }
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options) { }
        public DbSet<UserModel> Users { get; set; }

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
            modelBuilder.Entity<UserModel>().HasIndex(u => u.Username).IsUnique();
        }
    }
}
