using MediatR;
using StoreManager.Infrastructure.Document.Model;

namespace StoreManager.Infrastructure.Invoice.Command
{
    public record class CreateInvoiceCommand(Guid DocumentId, List<ExtractionMetadata> Metadata) : IRequest;

}
