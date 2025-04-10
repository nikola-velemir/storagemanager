using DocumentFormat.OpenXml.Wordprocessing;
using MediatR;
using StoreManager.Infrastructure.Provider.Command.Search;
using StoreManager.Infrastructure.Provider.DTO;
using StoreManager.Infrastructure.Provider.DTO.Search;
using StoreManager.Infrastructure.Provider.Repository;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Provider.Handler.Search
{
    public class FindFilteredProvidersHandler(IProviderRepository providerRepository)
        : IRequestHandler<FindFilteredProvidersQuery, PaginatedResult<ProviderSearchResponseDto>>
    {
        public async Task<PaginatedResult<ProviderSearchResponseDto>> Handle(FindFilteredProvidersQuery request, CancellationToken cancellationToken)
        {
            var result = await providerRepository.FindFiltered(request.ProviderName, request.PageNumber, request.PageSize);
            return new PaginatedResult<ProviderSearchResponseDto>
            {
                Items = result.Items.Select(p =>
                new ProviderSearchResponseDto(
                    p.Id,
                    p.Name,
                    p.Adress,
                    p.PhoneNumber,
                    p.Invoices.Select(i => new ProviderInvoiceSearchResponseDto(
                        i.Id,
                        i.DateIssued)
                    ).ToList()
                )
            ).ToList()
            };
        }
    }
}
