using MediatR;
using StoreManager.Application.BusinessPartner.Provider.Command.Statistics;
using StoreManager.Application.BusinessPartner.Provider.DTO.Statistics;
using StoreManager.Application.BusinessPartner.Provider.Repository;
using StoreManager.Application.Common;

namespace StoreManager.Application.BusinessPartner.Provider.Handler.Statistics
{
    public class FindProviderComponentInvolvementsQueryHandler(IProviderRepository providerRepository)
        : IRequestHandler<FindProviderComponentInvolvementsQuery, Result<ProviderComponentInvolvementResponsesDto>>
    {
        public async Task<Result<ProviderComponentInvolvementResponsesDto>> Handle(
            FindProviderComponentInvolvementsQuery request, CancellationToken cancellationToken)
        {
            var providers = await providerRepository.FindAllAsync();
            var responses = new List<ProviderComponentInvolvementResponseDto>();
            foreach (var provider in providers)
            {
                var count = await providerRepository.FindComponentCountForProviderAsync(provider);
                responses.Add(new ProviderComponentInvolvementResponseDto(provider.Id, provider.Name, count));
            }

            return Result.Success(new ProviderComponentInvolvementResponsesDto(responses));
        }
    }
}