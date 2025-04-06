using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Invoice.DTO;
using StoreManager.Infrastructure.Invoice.Model;
using StoreManager.Infrastructure.Invoice.Repository;
using StoreManager.Infrastructure.MechanicalComponent.Repository;

namespace StoreManager.Infrastructure.Invoice.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IMechanicalComponentRepository _mechanicalComponentRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceItemRepository _invoiceItemRepository;
        public InvoiceService(IInvoiceItemRepository invoiceItemRepository, IMechanicalComponentRepository mechanicalComponentRepository, IInvoiceRepository invoiceRepository)
        {
            _mechanicalComponentRepository = mechanicalComponentRepository;
            _invoiceRepository = invoiceRepository;
            _invoiceItemRepository = invoiceItemRepository;
        }

        public async Task Create(Guid id, List<ExtractionMetadata> metadata)
        {
            var invoice = await _invoiceRepository.FindByDocumentId(id);
            if (invoice is null) { return; }
            var components = await _mechanicalComponentRepository.CreateFromExtractionMetadata(metadata);
            foreach (var data in metadata)
            {
                var component = await _mechanicalComponentRepository.FindByIdentifier(data.Identifier);
                if (component is null) { continue; }
                await _invoiceItemRepository.Create(new InvoiceItemModel { Component = component, ComponentId = component.Id, Invoice = invoice, InvoiceId = invoice.Id, PricePerPiece = data.Price, Quantity = data.Quantity });
            }
        }
        public async Task<InvoiceSearchResponsesDTO> FindFilteredInvoices(string? providerId, string? dateIssued, int pageNumber, int pageSize)
        {
            Guid? id = null;
            if(Guid.TryParse(providerId, out var tempId))
            {
                id = tempId; 
            }
            DateOnly? date = null;
            if(DateOnly.TryParse(dateIssued, out var tempDate))
            {
                date = tempDate;
            }
            var result = await _invoiceRepository.FindFiltered(id, date, pageNumber, pageSize);
            return new InvoiceSearchResponsesDTO(new Shared.PaginatedResult<InvoiceSearchResponseDTO>
            {
                Items = result.Items.Select(invoice =>
                    new InvoiceSearchResponseDTO(
                        invoice.Id,
                        invoice.DateIssued,
                        new InvoiceSearchProviderDTO(invoice.Provider.Name, invoice.Provider.Adress, invoice.Provider.PhoneNumber),
                        invoice.Items.Select(
                            item => new InvoiceSearchComponentDTO(
                                item.Component.Name, item.Component.Identifier, item.Quantity, item.PricePerPiece
                                )
                            ).ToList()
                        )
                    ).ToList(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = result.TotalCount
            });
        }
    }
}
