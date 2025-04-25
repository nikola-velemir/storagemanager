using StoreManager.Domain;
using StoreManager.Infrastructure.Context;

namespace StoreManager.Infrastructure;

public class UnitOfWork(WarehouseDbContext context) : IUnitOfWork
{
    private readonly WarehouseDbContext _context = context;

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}