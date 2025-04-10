namespace StoreManager.Infrastructure.Invoice.DTO.Search
{
    public sealed record class InvoiceSearchResponseDto(Guid id, DateOnly date, InvoiceSearchProviderDto provider, List<InvoiceSearchComponentDto> components);

}
