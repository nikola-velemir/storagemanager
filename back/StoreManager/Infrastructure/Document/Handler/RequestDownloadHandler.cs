using MediatR;
using StoreManager.Infrastructure.Document.Command;
using StoreManager.Infrastructure.Document.DTO;
using StoreManager.Infrastructure.Document.Repository;
using StoreManager.Infrastructure.Invoice.Repository;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Infrastructure.Document.Handler
{
    public class RequestDownloadHandler(IDocumentRepository documentRepository, IInvoiceRepository invoiceRepository)
        : IRequestHandler<RequestDownloadQuery, RequestDocumentDownloadResponseDto>
    {
        public async Task<RequestDocumentDownloadResponseDto> Handle(RequestDownloadQuery request, CancellationToken cancellationToken)
        {

            if (!Guid.TryParse(request.InvoiceId, out var tempId))
            {
                throw new InvalidCastException("Guid cannot be parsed");
            }
            Guid invoiceGuid = Guid.Parse(request.InvoiceId);

            var invoice = await invoiceRepository.FindById(invoiceGuid);
            if (invoice is null)
            {
                throw new NotFoundException("Invoice not found");
            }
            var file = await documentRepository.FindByName(invoice.Document.FileName);
            if (file == null)
            {
                throw new NotFoundException("File not found");
            }
            return new RequestDocumentDownloadResponseDto(file.FileName, DocumentUtils.GetPresentationalMimeType(file.Type), file.Chunks.Count);

        }
    }
}
