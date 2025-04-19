namespace StoreManager.Application.BusinessPartner.Provider.DTO
{
    public sealed record ProviderFormDataRequestDto(
        string ProviderId,
        string ProviderAddress,
        string ProviderName,
        string ProviderPhoneNumber);
}