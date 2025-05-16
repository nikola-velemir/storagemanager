using StoreManager.Domain;
using StoreManager.Infrastructure.Context;

namespace StoreManager.Infrastructure;

public class UnitOfWork(WarehouseDbContext context) : IUnitOfWork
{
    private readonly WarehouseDbContext _context = context;

    public Task CommitAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}