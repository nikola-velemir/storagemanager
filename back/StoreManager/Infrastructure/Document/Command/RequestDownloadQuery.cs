using MediatR;
using StoreManager.Infrastructure.Document.DTO;

namespace StoreManager.Infrastructure.Document.Command
{
    public record RequestDownloadQuery(string InvoiceId) : IRequest<RequestDocumentDownloadResponseDto>;
}