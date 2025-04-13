namespace StoreManager.Infrastructure.Provider.DTO.Search
{
    public sealed record ProviderFindResponseDto(Guid Id, string Name, string Address, string PhoneNumber);
}