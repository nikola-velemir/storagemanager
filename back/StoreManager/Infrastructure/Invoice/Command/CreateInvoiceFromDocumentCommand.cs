using MediatR;
using StoreManager.Infrastructure.Document.Model;

namespace StoreManager.Infrastructure.Invoice.Command
{
    public sealed record CreateInvoiceFromDocumentCommand(Guid DocumentId, List<ExtractionMetadata> Metadata) : IRequest;
}
