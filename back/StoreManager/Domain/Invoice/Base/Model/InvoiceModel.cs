using StoreManager.Domain.Document.Model;
using StoreManager.Infrastructure.Invoice.Base;

namespace StoreManager.Domain.Invoice.Base.Model;

public class InvoiceModel
{
    public required Guid Id { get; set; }
    public required DocumentModel Document { get; set; }
    public required DateOnly DateIssued { get; set; }
    public required Guid DocumentId { get; set; }
    public InvoiceType Type { get; set; }
}