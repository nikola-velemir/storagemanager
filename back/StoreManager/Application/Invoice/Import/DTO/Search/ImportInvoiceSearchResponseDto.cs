namespace StoreManager.Application.Invoice.Import.DTO.Search
{
    public sealed record ImportInvoiceSearchResponseDto(
        Guid Id,
        DateOnly Date,
        ImportInvoiceSearchProviderResponseDto Provider,
        List<ImportInvoiceSearchComponentResponseDto> Components);
}