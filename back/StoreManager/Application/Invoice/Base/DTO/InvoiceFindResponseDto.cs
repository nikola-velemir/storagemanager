namespace StoreManager.Application.Invoice.Base.DTO;

public sealed record InvoiceFindResponseDto(Guid Id, DateOnly DateIssued);