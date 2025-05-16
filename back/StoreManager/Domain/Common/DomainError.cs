namespace StoreManager.Domain.Common;

public record DomainError(string Name, string? Description = null)
{
    public static readonly DomainError None = new(string.Empty);
}