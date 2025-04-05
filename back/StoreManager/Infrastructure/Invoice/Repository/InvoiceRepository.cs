using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Invoice.Model;

namespace StoreManager.Infrastructure.Invoice.Repository
{
    public sealed class InvoiceRepository : IInvoiceRepository
    {
        private readonly WarehouseDbContext _context;
        private readonly DbSet<InvoiceModel> _invoices;
        public InvoiceRepository(WarehouseDbContext context)
        {
            _context = context;
            _invoices = context.Invoices;
        }
        public async Task<InvoiceModel> Create(InvoiceModel invoice)
        {
            var savedInstance = await _invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
            return savedInstance.Entity;
        }

        public async Task<List<InvoiceModel>> FindAll()
        {
            return await _invoices
                .Include(invoice=>invoice.Provider)
                .Include(invoice=>invoice.Items)
                .ThenInclude(item=>item.Component).ToListAsync();
        }

        public async Task<(ICollection<InvoiceModel> Items, int TotalCount)> FindAllByDate(DateOnly date, int pageNumber, int pageSize)
        {
            var query = _invoices.Where(i => i.DateIssued == date);
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public Task<InvoiceModel?> FindByDocumentId(Guid documentId)
        {
            return _invoices.FirstOrDefaultAsync(i => i.DocumentId.Equals(documentId));
        }
    }
}
