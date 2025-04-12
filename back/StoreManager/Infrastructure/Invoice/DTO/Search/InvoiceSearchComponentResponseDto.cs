namespace StoreManager.Infrastructure.Invoice.DTO.Search
{
    public sealed record InvoiceSearchComponentDto(Guid Id, string Name, string identifier, int quantity, double price);
}