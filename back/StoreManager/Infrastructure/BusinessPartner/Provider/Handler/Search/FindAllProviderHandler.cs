using MediatR;
using StoreManager.Infrastructure.BusinessPartner.Provider.Command.Search;
using StoreManager.Infrastructure.BusinessPartner.Provider.DTO.Search;
using StoreManager.Infrastructure.BusinessPartner.Provider.Repository;

namespace StoreManager.Infrastructure.BusinessPartner.Provider.Handler.Search
{
    public class FindAllProviderHandler(IProviderRepository providerRepository)
        : IRequestHandler<FindAllProvidersQuery, ProviderFindResponsesDto>
    {
        public async Task<ProviderFindResponsesDto> Handle(FindAllProvidersQuery request, CancellationToken cancellationToken)
        {
            var providers = await providerRepository.FindAll();
            var responses = providers.Select(p => new ProviderFindResponseDto(p.Id, p.Name, p.Address, p.PhoneNumber)).ToList();
            return new ProviderFindResponsesDto(responses);
        }
    }
}
