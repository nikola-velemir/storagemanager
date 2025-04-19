using StoreManager.Application.BusinessPartner.Provider.DTO.Info;

namespace StoreManager.Application.BusinessPartner.Provider.DTO
{
    public sealed record ProviderProfileResponseDto(
        string Name,
        string Address,
        string PhoneNumber,
        List<ProviderProfileComponentResponseDto> Components,
        List<ProviderProfileInvoiceResponseDto> Invoices);
}