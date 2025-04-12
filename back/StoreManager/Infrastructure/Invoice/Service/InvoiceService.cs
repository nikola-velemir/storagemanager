using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Invoice.DTO.Search;
using StoreManager.Infrastructure.Invoice.DTO.Statistics;
using StoreManager.Infrastructure.Invoice.Model;
using StoreManager.Infrastructure.Invoice.Repository;
using StoreManager.Infrastructure.MechanicalComponent.Repository;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Invoice.Service
{
    public class InvoiceService(
        IInvoiceItemRepository invoiceItemRepository,
        IMechanicalComponentRepository mechanicalComponentRepository,
        IInvoiceRepository invoiceRepository)
        : IInvoiceService
    {
        public async Task<ThisWeekInvoiceCountResponseDto> CountInvoicesThisWeek()
        {
            return new ThisWeekInvoiceCountResponseDto(await invoiceRepository.CountInvoicesThisWeek());
        }

        public async Task Create(Guid id, List<ExtractionMetadata> metadata)
        {
            var invoice = await invoiceRepository.FindByDocumentId(id);
            if (invoice is null) { return; }
            var components = await mechanicalComponentRepository.CreateFromExtractionMetadata(metadata);
            foreach (var data in metadata)
            {
                var component = await mechanicalComponentRepository.FindByIdentifier(data.Identifier);
                if (component is null) { continue; }
                var foundItem = await invoiceItemRepository.FindByInvoiceAndComponentId(invoice.Id, component.Id);
                if (foundItem is null)
                    await invoiceItemRepository.Create(new InvoiceItemModel { Component = component, ComponentId = component.Id, Invoice = invoice, InvoiceId = invoice.Id, PricePerPiece = data.Price, Quantity = data.Quantity });
            }
        }

        public async Task<FindCountsForWeekResponseDto> FindCountsForWeek()
        {
            var startOfWeek = DateOnly.FromDateTime((DateTime.Now.StartOfWeek()));
            var endOfWeek = startOfWeek.AddDays(7);
            var counts = new List<FindCountForDayResponseDto>();
            for (var date = startOfWeek; date < endOfWeek; date.AddDays(1))
            {
                int count = await invoiceRepository.FindCountForTheDate(date);
                counts.Add(new FindCountForDayResponseDto(date.DayOfWeek.ToString(), count));
            }
            return new FindCountsForWeekResponseDto(counts);
        }

        public async Task<PaginatedResult<InvoiceSearchResponseDto>> FindFilteredInvoices(string? componentInfo, string? providerId, string? dateIssued, int pageNumber, int pageSize)
        {
            Guid? id = null;
            if (Guid.TryParse(providerId, out var tempId))
            {
                id = tempId;
            }
            DateOnly? date = null;
            if (DateOnly.TryParse(dateIssued, out var tempDate))
            {
                date = tempDate;
            }
            var result = await invoiceRepository.FindFiltered(componentInfo, id, date, pageNumber, pageSize);
            return new Shared.PaginatedResult<InvoiceSearchResponseDto>
            {
                Items = result.Items.Select(invoice =>
                    new InvoiceSearchResponseDto(
                        invoice.Id,
                        invoice.DateIssued,
                        new InvoiceSearchProviderResponseDto(invoice.Provider.Name, invoice.Provider.Adress, invoice.Provider.PhoneNumber),
                        invoice.Items.Select(
                            item => new InvoiceSearchComponentResponseDto(
                                item.Component.Id, item.Component.Name, item.Component.Identifier, item.Quantity, item.PricePerPiece
                                )
                            ).ToList()
                        )
                    ).ToList(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = result.TotalCount
            };
        }
    }
}
