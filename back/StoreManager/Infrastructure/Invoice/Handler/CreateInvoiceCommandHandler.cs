using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using MediatR;
using StoreManager.Infrastructure.Invoice.Command;
using StoreManager.Infrastructure.Invoice.Model;
using StoreManager.Infrastructure.Invoice.Repository;
using StoreManager.Infrastructure.MechanicalComponent.Repository;

namespace StoreManager.Infrastructure.Invoice.Handler
{
    public class CreateInvoiceCommandHandler(
        IInvoiceItemRepository invoiceItemRepository,
        IMechanicalComponentRepository mechanicalComponentRepository,
        IInvoiceRepository invoiceRepository)
        : IRequestHandler<CreateInvoiceCommand>
    {
        public async Task<Unit> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await invoiceRepository.FindByDocumentId(request.DocumentId);

            if (invoice is null) return Unit.Value;

            var components = await mechanicalComponentRepository.CreateFromExtractionMetadata(request.Metadata);
            foreach (var data in request.Metadata)
            {
                var component = await mechanicalComponentRepository.FindByIdentifier(data.Identifier);
                if (component is null) { continue; }
                var foundItem = await invoiceItemRepository.FindByInvoiceAndComponentId(invoice.Id, component.Id);
                if (foundItem is null)
                    await invoiceItemRepository.Create(new InvoiceItemModel { Component = component, ComponentId = component.Id, Invoice = invoice, InvoiceId = invoice.Id, PricePerPiece = data.Price, Quantity = data.Quantity });
            }
            return Unit.Value;
        }
    }
}
