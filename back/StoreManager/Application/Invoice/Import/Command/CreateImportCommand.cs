using MediatR;
using StoreManager.Domain.Document.Model;

namespace StoreManager.Application.Invoice.Import.Command
{
    public sealed record CreateImportCommand(Guid DocumentId, List<ExtractionMetadata> Metadata) : IRequest;

}
