using StoreManager.Infrastructure.Document.Model;

namespace StoreManager.Infrastructure.Invoice.Model
{
    public class InvoiceModel
    {
        public required Guid Id { get; set; }
        public required DocumentModel Document { get; set; }
        public required DateOnly DateIssued { get; set; }
        public required Guid DocumentId { get; set; }
    }
}
