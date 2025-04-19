using MediatR;
using StoreManager.Application.Invoice.Import.Command.Search;
using StoreManager.Application.Invoice.Import.DTO.Search;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Application.Shared;
using StoreManager.Infrastructure.Invoice.Import.Repository;
using StoreManager.Infrastructure.Invoice.Import.Repository.Specification;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Application.Invoice.Import.Handler.Search
{
    public class FilterImportQueryHandler(IImportRepository importRepository)
        : IRequestHandler<FilterImportInvoicesQuery, PaginatedResult<ImportInvoiceSearchResponseDto>>
    {
        public async Task<PaginatedResult<ImportInvoiceSearchResponseDto>> Handle(FilterImportInvoicesQuery request, CancellationToken cancellationToken)
        {
            Guid? id = null;
            if (Guid.TryParse(request.ProviderId, out var tempId))
            {
                id = tempId;
            }
            DateOnly? date = null;
            if (DateOnly.TryParse(request.DateIssued, out var tempDate))
            {
                date = tempDate;
            }
            var result = await importRepository.FindFiltered(new ImportWithProviderItemsComponent(),request.ComponentInfo, id, date, request.PageNumber, request.PageSize);
            return new PaginatedResult<ImportInvoiceSearchResponseDto>
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
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = result.TotalCount
            };
        }
    }
}
