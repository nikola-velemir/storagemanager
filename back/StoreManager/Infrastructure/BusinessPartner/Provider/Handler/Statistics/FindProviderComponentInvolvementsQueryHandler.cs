using MediatR;
using StoreManager.Infrastructure.BusinessPartner.Provider.Command.Statistics;
using StoreManager.Infrastructure.BusinessPartner.Provider.DTO.Statistics;
using StoreManager.Infrastructure.BusinessPartner.Provider.Repository;

namespace StoreManager.Infrastructure.BusinessPartner.Provider.Handler.Statistics
{
    public class FindProviderComponentInvolvementsQueryHandler(IProviderRepository providerRepository)
        : IRequestHandler<FindProviderComponentInvolvementsQuery, ProviderComponentInvolvementResponsesDto>
    {
        public async Task<ProviderComponentInvolvementResponsesDto> Handle(FindProviderComponentInvolvementsQuery request, CancellationToken cancellationToken)
        {
            var providers = await providerRepository.FindAll();
            var responses = new List<ProviderComponentInvolvementResponseDto>();
            foreach (var provider in providers)
            {
                var count = await providerRepository.FindComponentCountForProvider(provider);
                responses.Add(new ProviderComponentInvolvementResponseDto(provider.Id, provider.Name, count));

            }
            return new ProviderComponentInvolvementResponsesDto(responses);
        }
    }
}
