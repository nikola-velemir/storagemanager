using StoreManager.Domain.Document.Model;
using StoreManager.Infrastructure.Invoice.Import.Model;

namespace StoreManager.Infrastructure.Invoice.Base;

public class InvoiceModel
{
    public required Guid Id { get; set; }
    public required DocumentModel Document { get; set; }
    public required DateOnly DateIssued { get; set; }
    public required Guid DocumentId { get; set; }
    public InvoiceType Type { get; set; }
}