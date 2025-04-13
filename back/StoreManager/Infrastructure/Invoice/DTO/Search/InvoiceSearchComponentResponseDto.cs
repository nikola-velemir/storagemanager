namespace StoreManager.Infrastructure.Invoice.DTO.Search
{
    public sealed record InvoiceSearchComponentResponseDto(Guid Id, string Name, string Identifier, int Quantity, double Price);
}