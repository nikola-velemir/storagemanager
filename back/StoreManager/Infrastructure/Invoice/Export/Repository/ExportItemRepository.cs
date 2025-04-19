using Microsoft.EntityFrameworkCore;
using StoreManager.Application.Invoice.Export.Repository;
using StoreManager.Domain.Document.Service;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Invoice.Export.Model;
using StoreManager.Infrastructure.Product.Model;

namespace StoreManager.Infrastructure.Invoice.Export.Repository;

public class ExportItemRepository(WarehouseDbContext context) : IExportItemRepository
{
    private readonly DbSet<ExportItemModel> _exportItems = context.ExportItems;
    private readonly DbSet<ProductModel> _products = context.Products;

    public async Task CreateFromProductRowsAsync(ExportModel export, List<ProductRow> productRows)
    {
        foreach (var productRow in productRows)
        {
            var product =
                await _products.FirstOrDefaultAsync(p =>
                    p.Identifier.ToLower().Equals(productRow.Identifier.ToLower()));
            if (product is null)
            {
                continue;
            }

            var item = new ExportItemModel
            {
                Product = product,
                Export = export,
                ExportId = export.Id,
                ProductId = product.Id,
                PricePerPiece = productRow.Price,
                Quantity = productRow.Quantity
            };
            await _exportItems.AddAsync(item);
            await context.SaveChangesAsync();
        }
    }
}