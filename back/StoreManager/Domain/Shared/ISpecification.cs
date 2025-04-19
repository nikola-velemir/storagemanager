namespace StoreManager.Infrastructure.Shared;

public interface ISpecification<T>
{
    IQueryable<T> Apply(IQueryable<T> query);
}