namespace StoreManager.Infrastructure.Provider.DTO.Search
{
    public sealed record ProviderFindResponseDto(Guid id, string name, string address, string phoneNumber);
}