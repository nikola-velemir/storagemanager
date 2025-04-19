using MediatR;
using StoreManager.Application.Document.Command;
using StoreManager.Application.Document.DTO;
using StoreManager.Application.Document.Repository;
using StoreManager.Domain.Document.Specification;
using StoreManager.Infrastructure.Invoice.Base.Repository;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Application.Document.Handler
{
    public class RequestDownloadHandler(IDocumentRepository documentRepository, IInvoiceRepository importRepository)
        : IRequestHandler<RequestDownloadQuery, RequestDocumentDownloadResponseDto>
    {
        public async Task<RequestDocumentDownloadResponseDto> Handle(RequestDownloadQuery request, CancellationToken cancellationToken)
        {

            if (!Guid.TryParse(request.InvoiceId, out var tempId))
            {
                throw new InvalidCastException("Guid cannot be parsed");
            }
            var invoiceGuid = Guid.Parse(request.InvoiceId);

            var invoice = await importRepository.FindById(invoiceGuid);
            if (invoice is null)
            {
                throw new NotFoundException("Invoice not found");
            }
            var file = await documentRepository.FindByNameAsync(new DocumentWithDocumentChunks(), invoice.Document.FileName);
            if (file == null)
            {
                throw new NotFoundException("File not found");
            }
            return new RequestDocumentDownloadResponseDto(file.FileName, DocumentUtils.GetPresentationalMimeType(file.Type), file.Chunks.Count);

        }
    }
}
