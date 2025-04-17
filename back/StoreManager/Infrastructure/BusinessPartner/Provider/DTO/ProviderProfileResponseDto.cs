using StoreManager.Infrastructure.BusinessPartner.Provider.DTO.Info;

namespace StoreManager.Infrastructure.BusinessPartner.Provider.DTO
{
    public sealed record ProviderProfileResponseDto(
        string Name,
        string Address,
        string PhoneNumber,
        List<ProviderProfileComponentResponseDto> Components,
        List<ProviderProfileInvoiceResponseDto> Invoices);
}