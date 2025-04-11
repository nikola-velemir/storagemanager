using StoreManager.Infrastructure.Provider.DTO.Info;

namespace StoreManager.Infrastructure.Provider.DTO
{
    public sealed record ProviderProfileResponseDto(
        string name,
        string address,
        string phoneNumber,
        List<ProviderProfileComponentResponseDto> components,
        List<ProviderProfileInvoiceResponseDto> invoices);
}