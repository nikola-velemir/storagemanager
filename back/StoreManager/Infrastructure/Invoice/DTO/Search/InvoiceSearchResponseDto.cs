namespace StoreManager.Infrastructure.Invoice.DTO.Search
{
    public sealed record InvoiceSearchResponseDto(
        Guid Id,
        DateOnly Date,
        InvoiceSearchProviderResponseDto Provider,
        List<InvoiceSearchComponentResponseDto> Components);
}