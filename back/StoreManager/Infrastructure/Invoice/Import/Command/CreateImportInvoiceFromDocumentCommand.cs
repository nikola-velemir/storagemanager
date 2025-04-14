using MediatR;
using StoreManager.Infrastructure.Document.Model;

namespace StoreManager.Infrastructure.Invoice.Import.Command
{
    public sealed record CreateImportFromDocumentCommand(Guid DocumentId, List<ExtractionMetadata> Metadata) : IRequest;
}
