using MediatR;
using StoreManager.Infrastructure.Provider.Command.Search;
using StoreManager.Infrastructure.Provider.DTO.Search;
using StoreManager.Infrastructure.Provider.Repository;

namespace StoreManager.Infrastructure.Provider.Handler.Search
{
    public class FindAllProviderHandler : IRequestHandler<FindAllProvidersQuery, ProviderFindResponsesDTO>
    {
        private IProviderRepository _providerRepository;
        public FindAllProviderHandler(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<ProviderFindResponsesDTO> Handle(FindAllProvidersQuery request, CancellationToken cancellationToken)
        {
            var providers = await _providerRepository.FindAll();
            var responses = providers.Select(p => new ProviderFindResponseDTO(p.Id, p.Name, p.Adress, p.PhoneNumber)).ToList();
            return new ProviderFindResponsesDTO(responses);
        }
    }
}
