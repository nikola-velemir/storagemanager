namespace StoreManager.Infrastructure.Provider.DTO
{
    public sealed record class ProviderInvolementResponseDTO(Guid id, string name, int invoiceCount);
}
