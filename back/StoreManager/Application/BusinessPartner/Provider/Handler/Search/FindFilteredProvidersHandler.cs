using MediatR;
using StoreManager.Application.BusinessPartner.Provider.Command.Search;
using StoreManager.Application.BusinessPartner.Provider.DTO.Search;
using StoreManager.Application.BusinessPartner.Provider.Repository;
using StoreManager.Application.Shared;

namespace StoreManager.Application.BusinessPartner.Provider.Handler.Search
{
    public class FindFilteredProvidersHandler(IProviderRepository providerRepository)
        : IRequestHandler<FindFilteredProvidersQuery, PaginatedResult<ProviderSearchResponseDto>>
    {
        public async Task<PaginatedResult<ProviderSearchResponseDto>> Handle(FindFilteredProvidersQuery request, CancellationToken cancellationToken)
        {
            var result = await providerRepository.FindFilteredAsync(request.ProviderName, request.PageNumber, request.PageSize);
            return new PaginatedResult<ProviderSearchResponseDto>
            {
                Items = result.Items.Select(p =>
                new ProviderSearchResponseDto(
                    p.Id,
                    p.Name,
                    p.Address,
                    p.PhoneNumber,
                    p.Imports.Select(i => new ProviderInvoiceSearchResponseDto(
                        i.Id,
                        i.DateIssued)
                    ).ToList()
                )
            ).ToList()
            };
        }
    }
}
