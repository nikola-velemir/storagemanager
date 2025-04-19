namespace StoreManager.Application.Invoice.Import.DTO.Search
{
    public sealed record ImportInvoiceSearchComponentResponseDto(Guid Id, string Name, string Identifier, int Quantity, double Price);
}