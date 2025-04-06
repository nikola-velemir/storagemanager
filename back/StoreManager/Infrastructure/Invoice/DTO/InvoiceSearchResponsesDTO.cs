using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Invoice.DTO
{
    public sealed record class InvoiceSearchResponsesDTO(PaginatedResult<InvoiceSearchResponseDTO> responses);
}
