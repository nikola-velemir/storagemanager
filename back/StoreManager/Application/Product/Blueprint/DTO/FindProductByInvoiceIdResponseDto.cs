namespace StoreManager.Application.Product.Blueprint.DTO;

public sealed record FindProductByInvoiceIdResponseDto(Guid Id, string Name, string Identifier, DateOnly DateCreated,int Quantity, double Price);