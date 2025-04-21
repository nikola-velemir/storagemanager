using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using StoreManager.Infrastructure.Context;

namespace StoreManager.Infrastructure.DB
{
    public class WarehouseDbContextFactory : IDesignTimeDbContextFactory<WarehouseDbContext>
    {
        public WarehouseDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json")
                     .Build();

            var optionsBuilder = new DbContextOptionsBuilder<WarehouseDbContext>();
            var connectionString = config.GetConnectionString("PostgresConnection");

            optionsBuilder.UseNpgsql(connectionString);

            return new WarehouseDbContext(optionsBuilder.Options);
        }
    }
}
