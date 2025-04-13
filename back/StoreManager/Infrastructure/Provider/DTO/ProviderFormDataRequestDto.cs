namespace StoreManager.Infrastructure.Provider.DTO
{
    public sealed record ProviderFormDataRequestDto(
        string ProviderId,
        string ProviderAddress,
        string ProviderName,
        string ProviderPhoneNumber);
}