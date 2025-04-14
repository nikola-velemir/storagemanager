using StoreManager.Infrastructure.Document.Model;
using StoreManager.Infrastructure.Invoice.Import.DTO.Search;
using StoreManager.Infrastructure.Invoice.Import.DTO.Statistics;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Invoice.Import.Repository;
using StoreManager.Infrastructure.MechanicalComponent.Repository;
using StoreManager.Infrastructure.Shared;
using StoreManager.Infrastructure.Utils;

namespace StoreManager.Infrastructure.Invoice.Import.Service
{
    public class ImportService(
        IImportItemRepository importItemRepository,
        IMechanicalComponentRepository mechanicalComponentRepository,
        IImportRepository importRepository)
        : IImportService
    {
        public async Task<ThisWeekInvoiceCountResponseDto> CountInvoicesThisWeek()
        {
            return new ThisWeekInvoiceCountResponseDto(await importRepository.CountImportsThisWeek());
        }

        public async Task Create(Guid id, List<ExtractionMetadata> metadata)
        {
            var invoice = await importRepository.FindByDocumentId(id);
            if (invoice is null) { return; }
            var components = await mechanicalComponentRepository.CreateFromExtractionMetadata(metadata);
            foreach (var data in metadata)
            {
                var component = await mechanicalComponentRepository.FindByIdentifier(data.Identifier);
                if (component is null) { continue; }
                var foundItem = await importItemRepository.FindByImportAndComponentId(invoice.Id, component.Id);
                if (foundItem is null)
                    await importItemRepository.Create(new ImportItemModel { Component = component, ComponentId = component.Id, Import = invoice, ImportId = invoice.Id, PricePerPiece = data.Price, Quantity = data.Quantity });
            }
        }

        public async Task<FindCountsForWeekResponseDto> FindCountsForWeek()
        {
            var startOfWeek = DateOnly.FromDateTime(DateTime.UtcNow.StartOfWeek());
            var endOfWeek = startOfWeek.AddDays(7);
            var counts = new List<FindCountForDayResponseDto>();
            for (var date = startOfWeek; date < endOfWeek; date.AddDays(1))
            {
                int count = await importRepository.FindCountForTheDate(date);
                counts.Add(new FindCountForDayResponseDto(date.DayOfWeek.ToString(), count));
            }
            return new FindCountsForWeekResponseDto(counts);
        }

        public async Task<PaginatedResult<ImportInvoiceSearchResponseDto>> FindFilteredInvoices(string? componentInfo, string? providerId, string? dateIssued, int pageNumber, int pageSize)
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
            var result = await importRepository.FindFiltered(componentInfo, id, date, pageNumber, pageSize);
            return new Shared.PaginatedResult<ImportInvoiceSearchResponseDto>
            {
                Items = result.Items.Select(invoice =>
                    new ImportInvoiceSearchResponseDto(
                        invoice.Id,
                        invoice.DateIssued,
                        new ImportInvoiceSearchProviderResponseDto(invoice.Provider.Name, invoice.Provider.Address, invoice.Provider.PhoneNumber),
                        invoice.Items.Select(
                            item => new ImportInvoiceSearchComponentResponseDto(
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
