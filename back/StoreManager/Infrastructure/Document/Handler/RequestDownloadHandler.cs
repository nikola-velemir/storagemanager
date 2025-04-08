using MediatR;
using StoreManager.Infrastructure.Document.Command;
using StoreManager.Infrastructure.Document.DTO;
using StoreManager.Infrastructure.Document.Repository;
using StoreManager.Infrastructure.Invoice.Repository;

namespace StoreManager.Infrastructure.Document.Handler
{
    public class RequestDownloadHandler : IRequestHandler<RequestDownloadQuery, RequestDocumentDownloadResponseDTO>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IInvoiceRepository _invoiceRepository;

        public RequestDownloadHandler(IDocumentRepository documentRepository, IInvoiceRepository invoiceRepository)
        {
            _documentRepository = documentRepository;
            _invoiceRepository = invoiceRepository;
        }

        public async Task<RequestDocumentDownloadResponseDTO> Handle(RequestDownloadQuery request, CancellationToken cancellationToken)
        {

            if (!Guid.TryParse(request.InvoiceId, out var tempId))
            {
                throw new InvalidCastException("Guid cannot be parsed");
            }
            Guid invoiceGuid = Guid.Parse(request.InvoiceId);

            var invoice = await _invoiceRepository.FindById(invoiceGuid);
            if (invoice is null)
            {
                throw new EntryPointNotFoundException("Invoice not found");
            }
            var file = await _documentRepository.FindByName(invoice.Document.FileName);
            if (file == null)
            {
                throw new FileNotFoundException("File not found");
            }
            return new RequestDocumentDownloadResponseDTO(file.FileName, DocumentUtils.GetPresentationalMimeType(file.Type), file.Chunks.Count);

        }
    }
}
