namespace StoreManager.Infrastructure.Provider.DTO
{
    public sealed record ProviderFormDataRequestDto(
        string providerId,
        string providerAddress,
        string providerName,
        string providerPhoneNumber);
}