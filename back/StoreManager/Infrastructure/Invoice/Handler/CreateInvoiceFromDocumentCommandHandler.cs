using MediatR;
using StoreManager.Infrastructure.Invoice.Command;
using StoreManager.Infrastructure.Invoice.Repository;
using StoreManager.Infrastructure.MechanicalComponent.Repository;

namespace StoreManager.Infrastructure.Invoice.Handler
{
    public class CreateInvoiceFromDocumentCommandHandler(
        IInvoiceRepository invoiceRepository,
        IInvoiceItemRepository invoiceItemRepository,
        IMechanicalComponentRepository mechanicalComponentRepository)
        : IRequestHandler<CreateInvoiceFromDocumentCommand>
    {
        private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;
        private readonly IInvoiceItemRepository _invoiceItemRepository = invoiceItemRepository;
        private readonly IMechanicalComponentRepository _mechanicalComponentRepository = mechanicalComponentRepository;

        public Task<Unit> Handle(CreateInvoiceFromDocumentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
