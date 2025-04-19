using MediatR;
using StoreManager.Application.BusinessPartner.Provider.Command.Statistics;
using StoreManager.Application.BusinessPartner.Provider.DTO.Statistics;
using StoreManager.Application.BusinessPartner.Provider.Repository;

namespace StoreManager.Application.BusinessPartner.Provider.Handler.Statistics
{
    public class FindProviderComponentInvolvementsQueryHandler(IProviderRepository providerRepository)
        : IRequestHandler<FindProviderComponentInvolvementsQuery, ProviderComponentInvolvementResponsesDto>
    {
        public async Task<ProviderComponentInvolvementResponsesDto> Handle(FindProviderComponentInvolvementsQuery request, CancellationToken cancellationToken)
        {
            var providers = await providerRepository.FindAllAsync();
            var responses = new List<ProviderComponentInvolvementResponseDto>();
            foreach (var provider in providers)
            {
                var count = await providerRepository.FindComponentCountForProviderAsync(provider);
                responses.Add(new ProviderComponentInvolvementResponseDto(provider.Id, provider.Name, count));

            }
            return new ProviderComponentInvolvementResponsesDto(responses);
        }
    }
}
