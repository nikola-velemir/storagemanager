using MediatR;
using StoreManager.Infrastructure.Provider.Command.Statistics;
using StoreManager.Infrastructure.Provider.DTO.Statistics;
using StoreManager.Infrastructure.Provider.Repository;

namespace StoreManager.Infrastructure.Provider.Handler.Statistics
{
    public class FindProviderComponentInvolvementsQueryHandler : IRequestHandler<FindProviderComponentInvolvementsQuery, ProviderComponentInvolvementResponsesDTO>
    {
        private IProviderRepository _providerRepository;
        public FindProviderComponentInvolvementsQueryHandler(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<ProviderComponentInvolvementResponsesDTO> Handle(FindProviderComponentInvolvementsQuery request, CancellationToken cancellationToken)
        {
            var providers = await _providerRepository.FindAll();
            var responses = new List<ProviderComponentInvolvementResponseDTO>();
            foreach (var provider in providers)
            {
                var count = await _providerRepository.FindComponentCountForProvider(provider);
                responses.Add(new ProviderComponentInvolvementResponseDTO(provider.Id, provider.Name, count));

            }
            return new ProviderComponentInvolvementResponsesDTO(responses);
        }
    }
}
