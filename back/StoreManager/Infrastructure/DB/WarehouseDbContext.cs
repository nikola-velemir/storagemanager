using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Model;
using StoreManager.Infrastructure.BusinessPartner.Base;
using StoreManager.Infrastructure.BusinessPartner.Exporter.Model;
using StoreManager.Infrastructure.BusinessPartner.Provider.Model;
using StoreManager.Infrastructure.DB.Auth;
using StoreManager.Infrastructure.DB.BusinessPartner.Provider;
using StoreManager.Infrastructure.DB.Document;
using StoreManager.Infrastructure.DB.Invoice;
using StoreManager.Infrastructure.DB.Invoice.Export;
using StoreManager.Infrastructure.DB.Invoice.Import;
using StoreManager.Infrastructure.DB.MechanicalComponent;
using StoreManager.Infrastructure.DB.Product;
using StoreManager.Infrastructure.DB.Users;
using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Invoice.Base;
using StoreManager.Infrastructure.Invoice.Export.Model;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.MechanicalComponent.Model;
using StoreManager.Infrastructure.Product.Model;
using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Infrastructure.DB
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
            modelBuilder.ApplyConfiguration(new UserModelConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentModelConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentChunkModelConfiguration());
            modelBuilder.ApplyConfiguration(new ProviderModelConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceModelConfiguration());
            modelBuilder.ApplyConfiguration(new ExportModelConfiguration());
            modelBuilder.ApplyConfiguration(new ExportItemModelConfiguration());
            modelBuilder.ApplyConfiguration(new ImportModelConfiguration());
            modelBuilder.ApplyConfiguration(new ImportItemModelConfiguration());
            modelBuilder.ApplyConfiguration(new MechanicalComponentModelConfiguration());
            modelBuilder.ApplyConfiguration(new ProductModelConfiguration());
            modelBuilder.ApplyConfiguration(new ProductComponentsModelConfiguration());
        }
    }
}