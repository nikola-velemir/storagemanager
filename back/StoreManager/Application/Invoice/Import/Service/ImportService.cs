using StoreManager.Application.Invoice.Import.DTO.Search;
using StoreManager.Application.Invoice.Import.DTO.Statistics;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Application.MechanicalComponent.Repository;
using StoreManager.Application.Shared;
using StoreManager.Domain.BusinessPartner.Shared;
using StoreManager.Domain.Document.Model;
using StoreManager.Domain.Utils;
using StoreManager.Infrastructure.Invoice.Import.Model;
using StoreManager.Infrastructure.Invoice.Import.Repository.Specification;
using StoreManager.Infrastructure.Invoice.Import.Service;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Application.Invoice.Import.Service
{
    public class ImportService(
        IMechanicalComponentRepository mechanicalComponentRepository,
        IImportRepository importRepository)
        : IImportService
    {
        private static string FormatAddress(Address address)
        {
            return address.City + ", " + address.Street + " " + address.StreetNumber;
        }

        public async Task<ThisWeekInvoiceCountResponseDto> CountInvoicesThisWeek()
        {
            return new ThisWeekInvoiceCountResponseDto(await importRepository.CountImportsThisWeek());
        }

        public async Task Create(Guid id, List<ExtractionMetadata> metadata)
        {
            var invoice = await importRepository.FindByDocumentId(id);
            if (invoice is null)
            {
                return;
            }

            var components = await mechanicalComponentRepository.CreateFromExtractionMetadataAsync(metadata);
            foreach (var data in metadata)
            {
                var component = await mechanicalComponentRepository.FindByIdentifierAsync(data.Identifier);
                if (component is null)
                {
                    continue;
                }

                var foundItem = await importRepository.FindByImportAndComponentIdAsync(invoice.Id, component.Id);
                if (foundItem is not null) continue;
                
                invoice.AddItem(new ImportItemModel
                {
                    Component = component,
                    ComponentId = component.Id, 
                    Import = invoice,
                    ImportId = invoice.Id,
                    PricePerPiece = data.Price,
                    Quantity = data.Quantity
                });
                await importRepository.UpdateAsync(invoice);
            }
        }

        public async Task<FindCountsForWeekResponseDto> FindCountsForWeek()
        {
            var startOfWeek = DateOnly.FromDateTime(DateTime.UtcNow.StartOfWeek());
            var endOfWeek = startOfWeek.AddDays(7);
            var counts = new List<FindCountForDayResponseDto>();
            for (var date = startOfWeek; date < endOfWeek; date.AddDays(1))
            {
                int count = await importRepository.FindCountForTheDateAsync(date);
                counts.Add(new FindCountForDayResponseDto(date.DayOfWeek.ToString(), count));
            }

            return new FindCountsForWeekResponseDto(counts);
        }

        public async Task<PaginatedResult<ImportInvoiceSearchResponseDto>> FindFilteredInvoices(string? componentInfo,
            string? providerId, string? dateIssued, int pageNumber, int pageSize)
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

            var result = await importRepository.FindFiltered(new ImportWithProviderItemsComponent(), componentInfo, id,
                date, pageNumber, pageSize);
            return new PaginatedResult<ImportInvoiceSearchResponseDto>
            {
                Items = result.Items.Select(invoice =>
                    new ImportInvoiceSearchResponseDto(
                        invoice.Id,
                        invoice.DateIssued,
                        new ImportInvoiceSearchProviderResponseDto(invoice.Provider.Name,
                            Utils.FormatAddress(invoice.Provider.Address),
                            invoice.Provider.PhoneNumber),
                        invoice.Items.Select(item => new ImportInvoiceSearchComponentResponseDto(
                                item.Component.Id, item.Component.Name, item.Component.Identifier, item.Quantity,
                                item.PricePerPiece
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