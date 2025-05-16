using MediatR;
using StoreManager.Application.Common;
using StoreManager.Domain.Document.Model;

namespace StoreManager.Application.Invoice.Import.Command
{
    public sealed record CreateImportFromDocumentCommand(Guid DocumentId, List<ExtractionMetadata> Metadata) : IRequest<Result>;
}
