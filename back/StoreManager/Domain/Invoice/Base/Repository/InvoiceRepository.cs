using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.Context;
using StoreManager.Infrastructure.DB;

namespace StoreManager.Infrastructure.Invoice.Base.Repository;

public class InvoiceRepository(WarehouseDbContext context) : IInvoiceRepository
{
    private readonly WarehouseDbContext _context = context;
    private readonly DbSet<InvoiceModel> _invoice = context.Invoices;

    public Task<InvoiceModel?> FindById(Guid id)
    {
        return _invoice.Include(i=>i.Document).FirstOrDefaultAsync(i => i.Id.Equals(id));
    }
}