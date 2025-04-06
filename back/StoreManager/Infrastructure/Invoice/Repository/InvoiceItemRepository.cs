using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Invoice.Model;

namespace StoreManager.Infrastructure.Invoice.Repository
{
    public class InvoiceItemRepository : IInvoiceItemRepository
    {
        private readonly WarehouseDbContext _context;
        private readonly DbSet<InvoiceItemModel> _invoiceItems;
        public InvoiceItemRepository(WarehouseDbContext context)
        {
            _context = context;
            _invoiceItems = _context.InvoiceItems;
        }
        public async Task<InvoiceItemModel> Create(InvoiceItemModel invoiceItem)
        {
            var savedInstance = await _invoiceItems.AddAsync(invoiceItem);
            await _context.SaveChangesAsync();
            return savedInstance.Entity;
        }

        public Task<InvoiceItemModel?> FindByInvoiceAndComponentId(Guid invoiceId, Guid componentId)
        {
            return _invoiceItems.FirstOrDefaultAsync(ii => ii.InvoiceId.Equals(invoiceId) && ii.ComponentId.Equals(componentId));
        }
    }
}
