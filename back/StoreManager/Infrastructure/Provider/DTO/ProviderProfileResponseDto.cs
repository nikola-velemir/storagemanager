using StoreManager.Infrastructure.Provider.DTO.Info;

namespace StoreManager.Infrastructure.Provider.DTO
{
    public sealed record ProviderProfileResponseDto(
        string Name,
        string Address,
        string PhoneNumber,
        List<ProviderProfileComponentResponseDto> Components,
        List<ProviderProfileInvoiceResponseDto> Invoices);
}