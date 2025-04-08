using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using MediatR;
using StoreManager.Infrastructure.Invoice.Command;
using StoreManager.Infrastructure.Invoice.Model;
using StoreManager.Infrastructure.Invoice.Repository;
using StoreManager.Infrastructure.MechanicalComponent.Repository;

namespace StoreManager.Infrastructure.Invoice.Handler
{
    public class CreateInvoiceCommandHanlder : IRequestHandler<CreateInvoiceCommand>
    {
        private readonly IMechanicalComponentRepository _mechanicalComponentRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceItemRepository _invoiceItemRepository;
        public CreateInvoiceCommandHanlder(IInvoiceItemRepository invoiceItemRepository, IMechanicalComponentRepository mechanicalComponentRepository, IInvoiceRepository invoiceRepository)
        {
            _mechanicalComponentRepository = mechanicalComponentRepository;
            _invoiceItemRepository = invoiceItemRepository;
            _invoiceRepository = invoiceRepository;
        }
        public async Task<Unit> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepository.FindByDocumentId(request.DocumentId);

            if (invoice is null) return Unit.Value;

            var components = await _mechanicalComponentRepository.CreateFromExtractionMetadata(request.Metadata);
            foreach (var data in request.Metadata)
            {
                var component = await _mechanicalComponentRepository.FindByIdentifier(data.Identifier);
                if (component is null) { continue; }
                var foundItem = await _invoiceItemRepository.FindByInvoiceAndComponentId(invoice.Id, component.Id);
                if (foundItem is null)
                    await _invoiceItemRepository.Create(new InvoiceItemModel { Component = component, ComponentId = component.Id, Invoice = invoice, InvoiceId = invoice.Id, PricePerPiece = data.Price, Quantity = data.Quantity });
            }
            return Unit.Value;
        }
    }
}
