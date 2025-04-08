using DocumentFormat.OpenXml.Wordprocessing;
using MediatR;
using StoreManager.Infrastructure.Invoice.DTO;
using StoreManager.Infrastructure.Invoice.Query;
using StoreManager.Infrastructure.Invoice.Repository;
using StoreManager.Infrastructure.Shared;
using static Supabase.Gotrue.Constants;

namespace StoreManager.Infrastructure.Invoice.Handler
{
    public class FilterInvoicesQueryHandler : IRequestHandler<FilterInvoicesQuery, PaginatedResult<InvoiceSearchResponseDTO>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public FilterInvoicesQueryHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<PaginatedResult<InvoiceSearchResponseDTO>> Handle(FilterInvoicesQuery request, CancellationToken cancellationToken)
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
            var result = await _invoiceRepository.FindFiltered(request.ComponentInfo, id, date, request.PageNumber, request.PageSize);
            return new PaginatedResult<InvoiceSearchResponseDTO>
            {
                Items = result.Items.Select(invoice =>
                    new InvoiceSearchResponseDTO(
                        invoice.Id,
                        invoice.DateIssued,
                        new InvoiceSearchProviderDTO(invoice.Provider.Name, invoice.Provider.Adress, invoice.Provider.PhoneNumber),
                        invoice.Items.Select(
                            item => new InvoiceSearchComponentDTO(
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
