namespace StoreManager.Application.BusinessPartner.Provider.DTO.Search
{
    public sealed record ProviderFindResponseDto(Guid Id, string Name, string Address, string PhoneNumber);
}