using MediatR;
using StoreManager.Infrastructure.Invoice.Command.Search;
using StoreManager.Infrastructure.Invoice.DTO.Search;
using StoreManager.Infrastructure.Invoice.Repository;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Invoice.Handler.Search
{
    public class FilterInvoicesQueryHandler(IInvoiceRepository invoiceRepository)
        : IRequestHandler<FilterInvoicesQuery, PaginatedResult<InvoiceSearchResponseDto>>
    {
        public async Task<PaginatedResult<InvoiceSearchResponseDto>> Handle(FilterInvoicesQuery request, CancellationToken cancellationToken)
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
            var result = await invoiceRepository.FindFiltered(request.ComponentInfo, id, date, request.PageNumber, request.PageSize);
            return new PaginatedResult<InvoiceSearchResponseDto>
            {
                Items = result.Items.Select(invoice =>
                    new InvoiceSearchResponseDto(
                        invoice.Id,
                        invoice.DateIssued,
                        new InvoiceSearchProviderDto(invoice.Provider.Name, invoice.Provider.Adress, invoice.Provider.PhoneNumber),
                        invoice.Items.Select(
                            item => new InvoiceSearchComponentDto(
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
