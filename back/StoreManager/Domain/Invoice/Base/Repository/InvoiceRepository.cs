using Microsoft.EntityFrameworkCore;
using StoreManager.Domain.Invoice.Base.Model;
using StoreManager.Infrastructure.Context;
using StoreManager.Infrastructure.Invoice.Base;

namespace StoreManager.Domain.Invoice.Base.Repository;

public class InvoiceRepository(WarehouseDbContext context) : IInvoiceRepository
{
    private readonly WarehouseDbContext _context = context;
    private readonly DbSet<Model.Invoice> _invoice = context.Invoices;

    public Task<Model.Invoice?> FindById(Guid id)
    {
        return _invoice.Include(i=>i.Document).FirstOrDefaultAsync(i => i.Id.Equals(id));
    }

    public Task<List<Model.Invoice>> FindByPartnerId(Guid partnerId)
    {
        return _invoice.ToListAsync();
    }
}