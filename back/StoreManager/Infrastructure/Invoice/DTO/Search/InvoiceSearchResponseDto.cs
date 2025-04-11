namespace StoreManager.Infrastructure.Invoice.DTO.Search
{
    public sealed record InvoiceSearchResponseDto(
        Guid id,
        DateOnly date,
        InvoiceSearchProviderDto provider,
        List<InvoiceSearchComponentDto> components);
}