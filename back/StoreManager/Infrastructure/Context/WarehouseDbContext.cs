using Microsoft.EntityFrameworkCore;
using StoreManager.Application.Document;
using StoreManager.Domain.Auth.Tokens.RefreshToken.Model;
using StoreManager.Domain.BusinessPartner.Exporter.Model;
using StoreManager.Domain.BusinessPartner.Provider.Model;
using StoreManager.Domain.Document.Model;
using StoreManager.Domain.Invoice.Export.Model;
using StoreManager.Domain.Invoice.Import.Model;
using StoreManager.Domain.Product.Batch;
using StoreManager.Domain.Product.Batch.Model;
using StoreManager.Domain.Product.Blueprint.Model;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.outbox;

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
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<Domain.User.Model.User> Users { get; set; }
        public DbSet<Domain.Document.Model.Document> Documents { get; set; }
        public DbSet<RefreshTokenModel> RefreshTokens { get; set; }
        public DbSet<DocumentChunk> DocumentChunks { get; set; }
        public DbSet<Domain.Invoice.Base.Model.Invoice> Invoices { get; set; }
        public DbSet<Export> Exports { get; set; }
        public DbSet<ExportItem> ExportItems { get; set; }
        public DbSet<Import> Imports { get; set; }
        public DbSet<ImportItemModel> ImportItems { get; set; }
        public DbSet<Domain.MechanicalComponent.Model.MechanicalComponent> MechanicalComponents { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Domain.BusinessPartner.Base.Model.BusinessPartner> BusinessPartners { get; set; }
        public DbSet<Exporter> Exporters { get; set; }
        public DbSet<ProductBlueprint> ProductBlueprints { get; set; }
        public DbSet<ProductBlueprintLineItem> ProductComponents { get; set; }
        public DbSet<ProductBatch> ProductBatches { get; set; }

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