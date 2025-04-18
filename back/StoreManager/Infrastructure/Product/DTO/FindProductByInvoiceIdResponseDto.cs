namespace StoreManager.Infrastructure.Product.DTO;

public sealed record FindProductByInvoiceIdResponseDto(Guid Id, string Name, string Identifier, DateOnly DateCreated,int Quantity, double Price);