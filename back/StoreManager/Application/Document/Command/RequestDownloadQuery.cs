using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.Document.DTO;

namespace StoreManager.Application.Document.Command
{
    public record RequestDownloadQuery(string InvoiceId) : IRequest<Result<RequestDocumentDownloadResponseDto>>;
}