using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Provider.Model;

namespace StoreManager.Infrastructure.Invoice.Model
{
    public class InvoiceModel
    {
        public required Guid Id { get; set; }
        public required ProviderModel Provider { get; set; }
        public required DocumentModel Document { get; set; }
        public required DateOnly DateIssued { get; set; }
        public required Guid DocumentId { get; set; }

        public required Guid ProviderId { get; set; }
        public ICollection<InvoiceItemModel> Items { get; set; } = new List<InvoiceItemModel>();
    }
}
