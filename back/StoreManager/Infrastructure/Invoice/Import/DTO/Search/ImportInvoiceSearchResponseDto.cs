namespace StoreManager.Infrastructure.Invoice.Import.DTO.Search
{
    public sealed record ImportInvoiceSearchResponseDto(
        Guid Id,
        DateOnly Date,
        ImportInvoiceSearchProviderResponseDto Provider,
        List<ImportInvoiceSearchComponentResponseDto> Components);
}