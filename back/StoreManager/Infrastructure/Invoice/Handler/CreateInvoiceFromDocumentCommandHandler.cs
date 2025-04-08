using MediatR;
using StoreManager.Infrastructure.Invoice.Command;
using StoreManager.Infrastructure.Invoice.Repository;
using StoreManager.Infrastructure.MechanicalComponent.Repository;

namespace StoreManager.Infrastructure.Invoice.Handler
{
    public class CreateInvoiceFromDocumentCommandHandler : IRequestHandler<CreateInvoiceFromDocumentCommand>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceItemRepository _invoiceItemRepository;
        private readonly IMechanicalComponentRepository _mechanicalComponentRepository;
        public CreateInvoiceFromDocumentCommandHandler(IInvoiceRepository invoiceRepository, IInvoiceItemRepository invoiceItemRepository, IMechanicalComponentRepository mechanicalComponentRepository)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceItemRepository = invoiceItemRepository;
            _mechanicalComponentRepository = mechanicalComponentRepository;
        }

        public Task<Unit> Handle(CreateInvoiceFromDocumentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
