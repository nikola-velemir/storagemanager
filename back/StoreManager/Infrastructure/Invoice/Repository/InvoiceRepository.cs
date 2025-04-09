using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Invoice.DTO;
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
                .Include(invoice => invoice.Provider)
                .Include(invoice => invoice.Items)
                .ThenInclude(item => item.Component).ToListAsync();
        }

        public async Task<(ICollection<InvoiceModel> Items, int TotalCount)> FindFiltered(string? componentInfo, Guid? providerId, DateOnly? dateIssued, int pageNumber, int pageSize)
        {
            var query = _invoices.Include(i => i.Provider).Include(i => i.Items).ThenInclude(item => item.Component).AsQueryable();
            if (!string.IsNullOrEmpty(componentInfo))
            {
                query = query.Where(i => i.Items.Any(ii => ii.Component.Name.ToLower().Contains(componentInfo) || ii.Component.Identifier.Contains(componentInfo)));
            }
            if (providerId.HasValue)
            {
                query = query.Where(i => i.Provider.Id.Equals(providerId));
            }
            if (dateIssued.HasValue)
            {
                query = query.Where(i => i.DateIssued.Equals(dateIssued));
            }
            int skip = (pageNumber - 1) * pageSize;

            var totalCount = await query.CountAsync();
            var items = await query.Skip(skip).Take(pageSize).ToListAsync();

            return (items, totalCount);
        }

        public Task<InvoiceModel?> FindByDocumentId(Guid documentId)
        {
            return _invoices.FirstOrDefaultAsync(i => i.DocumentId.Equals(documentId));
        }

        public Task<InvoiceModel?> FindById(Guid id)
        {
            return _invoices.Include(i => i.Document).FirstOrDefaultAsync(i => i.Id.Equals(id));
        }

        public async Task<List<InvoiceModel>> FindByProviderId(Guid id)
        {
            var query = _invoices.Include(i => i.Provider).Where(i => i.Provider.Id.Equals(id)).AsQueryable();
            return await query.ToListAsync();
        }
        public Task<int> CountInvoicesThisWeek()
        {
            var startOfWeek = DateOnly.FromDateTime((DateTime.Now.StartOfWeek()));
            var endOfWeek = startOfWeek.AddDays(7);

            var query = _invoices.Where(i => i.DateIssued >= startOfWeek && i.DateIssued < endOfWeek);
            return query.CountAsync();
        }

        public Task<int> FindCountForTheDate(DateOnly date)
        {
            var query = _invoices.Where(i => i.DateIssued.Equals(date));
            return query.CountAsync();
        }
    }
}
