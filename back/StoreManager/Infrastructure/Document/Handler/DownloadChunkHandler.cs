using MediatR;
using StoreManager.Infrastructure.Document.Command;
using StoreManager.Infrastructure.Document.DTO;
using StoreManager.Infrastructure.Document.Repository;
using StoreManager.Infrastructure.Document.SupaBase.Service;
using StoreManager.Infrastructure.Invoice.Repository;

namespace StoreManager.Infrastructure.Document.Handler
{
    public class DownloadChunkHandler : IRequestHandler<DownloadChunkQuery, DocumentDownloadResponseDTO>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICloudStorageService _supaService;
        private readonly IDocumentRepository _documentRepository;
        public DownloadChunkHandler(IInvoiceRepository invoiceRepository, ICloudStorageService cloudStorageService, IDocumentRepository documentRepository)
        {
            _invoiceRepository = invoiceRepository;
            _supaService = cloudStorageService;
            _documentRepository = documentRepository;
        }
        public async Task<DocumentDownloadResponseDTO> Handle(DownloadChunkQuery request, CancellationToken cancellationToken)
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

            var file = await _documentRepository.FindByName(invoice.Document.FileName) ?? throw new FileNotFoundException("File not found");



            var chunk = file.Chunks.FirstOrDefault(chunk => chunk.ChunkNumber == request.ChunkIndex)
                ?? throw new EntryPointNotFoundException("Chunk not found");

            var response = new DocumentDownloadResponseDTO(await _supaService.DownloadChunk(chunk), DocumentUtils.GetPresentationalMimeType(file.Type));
            return response;
        }
    }
}
