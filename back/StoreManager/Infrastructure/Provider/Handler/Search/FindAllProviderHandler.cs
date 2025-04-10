using MediatR;
using StoreManager.Infrastructure.Provider.Command.Search;
using StoreManager.Infrastructure.Provider.DTO.Search;
using StoreManager.Infrastructure.Provider.Repository;

namespace StoreManager.Infrastructure.Provider.Handler.Search
{
    public class FindAllProviderHandler(IProviderRepository providerRepository)
        : IRequestHandler<FindAllProvidersQuery, ProviderFindResponsesDto>
    {
        public async Task<ProviderFindResponsesDto> Handle(FindAllProvidersQuery request, CancellationToken cancellationToken)
        {
            var providers = await providerRepository.FindAll();
            var responses = providers.Select(p => new ProviderFindResponseDto(p.Id, p.Name, p.Adress, p.PhoneNumber)).ToList();
            return new ProviderFindResponsesDto(responses);
        }
    }
}
