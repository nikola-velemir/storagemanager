using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.Document.Command;
using StoreManager.Application.Document.DTO;
using StoreManager.Application.Document.Errors;
using StoreManager.Application.Document.Repository;
using StoreManager.Domain.Document.Specification;
using StoreManager.Domain.Invoice.Base.Repository;

namespace StoreManager.Application.Document.Handler
{
    public class RequestDownloadHandler(IDocumentRepository documentRepository, IInvoiceRepository importRepository)
        : IRequestHandler<RequestDownloadQuery, Result<RequestDocumentDownloadResponseDto>>
    {
        public async Task<Result<RequestDocumentDownloadResponseDto>> Handle(RequestDownloadQuery request,
            CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.InvoiceId, out var tempId))
            {
                return DocumentErrors.DocumentIdParseError;
            }


            var invoice = await importRepository.FindById(tempId);
            if (invoice is null)
            {
                return DocumentErrors.DocumentNotFound;
            }

            var file = await documentRepository.FindByNameAsync(new DocumentWithDocumentChunks(),
                invoice.Document.FileName);
            if (file == null)
            {
                return DocumentErrors.DocumentNotFound;
            }

            return Result.Success(new RequestDocumentDownloadResponseDto(file.FileName,
                DocumentUtils.GetPresentationalMimeType(file.Type), file.Chunks.Count));
        }
    }
}