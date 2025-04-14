namespace StoreManager.Infrastructure.BusinessPartner.Provider.DTO.Search
{
    public sealed record ProviderSearchResponseDto(
        Guid Id,
        string Name,
        string Address,
        string PhoneNumber,
        List<ProviderInvoiceSearchResponseDto> Invoices);
}