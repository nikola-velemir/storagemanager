using DocumentFormat.OpenXml.Wordprocessing;
using MediatR;
using StoreManager.Infrastructure.Provider.Command.Search;
using StoreManager.Infrastructure.Provider.DTO;
using StoreManager.Infrastructure.Provider.DTO.Search;
using StoreManager.Infrastructure.Provider.Repository;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.Provider.Handler.Search
{
    public class FindFilteredProvidersHandler : IRequestHandler<FindFilteredProvidersQuery, PaginatedResult<ProviderSearchResponseDTO>>
    {
        private IProviderRepository _providerRepository;
        public FindFilteredProvidersHandler(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<PaginatedResult<ProviderSearchResponseDTO>> Handle(FindFilteredProvidersQuery request, CancellationToken cancellationToken)
        {
            var result = await _providerRepository.FindFiltered(request.ProviderName, request.PageNumber, request.PageSize);
            return new PaginatedResult<ProviderSearchResponseDTO>
            {
                Items = result.Items.Select(p =>
                new ProviderSearchResponseDTO(
                    p.Id,
                    p.Name,
                    p.Adress,
                    p.PhoneNumber,
                    p.Invoices.Select(i => new ProviderInvoiceSearchResponseDTO(
                        i.Id,
                        i.DateIssued)
                    ).ToList()
                )
            ).ToList()
            };
        }
    }
}
