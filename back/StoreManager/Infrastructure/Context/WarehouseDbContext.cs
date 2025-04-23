using Microsoft.EntityFrameworkCore;
using StoreManager.Domain.Auth.Tokens.RefreshToken.Model;
using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Domain.BusinessPartner.Exporter.Model;
using StoreManager.Domain.BusinessPartner.Provider.Model;
using StoreManager.Domain.Document.Model;
using StoreManager.Domain.Invoice.Base.Model;
using StoreManager.Domain.Invoice.Export.Model;
using StoreManager.Infrastructure.BusinessPartner.Base.Configuration;
using StoreManager.Infrastructure.BusinessPartner.Exporter.Configuration;
using StoreManager.Infrastructure.DB.BusinessPartner.Provider;
using StoreManager.Infrastructure.Document.Configuration;
using StoreManager.Infrastructure.Invoice.Base;
using StoreManager.Infrastructure.Invoice.Configuration;
using StoreManager.Infrastructure.Invoice.Export.Configuration;
using StoreManager.Infrastructure.Invoice.Export.Model;
using StoreManager.Infrastructure.Invoice.Import.Configuration;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.MechanicalComponent.Configuration;
using StoreManager.Infrastructure.MechanicalComponent.Model;
using StoreManager.Infrastructure.Product.Configuration;
using StoreManager.Infrastructure.Product.Model;
using StoreManager.Infrastructure.User.Configuration;
using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Infrastructure.Context
{
    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext()
        {
        }

        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<DocumentModel> Documents { get; set; }
        public DbSet<RefreshTokenModel> RefreshTokens { get; set; }
        public DbSet<DocumentChunkModel> DocumentChunks { get; set; }
        public DbSet<InvoiceModel> Invoices { get; set; }
        public DbSet<ExportModel> Exports { get; set; }
        public DbSet<ExportItemModel> ExportItems { get; set; }
        public DbSet<ImportModel> Imports { get; set; }
        public DbSet<ImportItemModel> ImportItems { get; set; }
        public DbSet<MechanicalComponentModel> MechanicalComponents { get; set; }
        public DbSet<ProviderModel> Providers { get; set; }
        public DbSet<BusinessPartnerModel> BusinessPartners { get; set; }
        public DbSet<ExporterModel> Exporters { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<ProductComponentsModel> ProductComponents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString("PostgresConnection");
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarehouseDbContext).Assembly);

        }
    }
}