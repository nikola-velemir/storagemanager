using MediatR;
using StoreManager.Infrastructure.Provider.Command.Statistics;
using StoreManager.Infrastructure.Provider.DTO.Statistics;
using StoreManager.Infrastructure.Provider.Repository;

namespace StoreManager.Infrastructure.Provider.Handler.Statistics
{
    public class FindProviderInvoiceInvolvementsQueryHandler(IProviderRepository providerRepository)
        : IRequestHandler<FindProviderInvoiceInvolvementsQuery, ProviderInvoiceInvolvementResponsesDto>
    {
        public async Task<ProviderInvoiceInvolvementResponsesDto> Handle(FindProviderInvoiceInvolvementsQuery request, CancellationToken cancellationToken)
        {
            var providers = await providerRepository.FindAll();
            var responses = new List<ProviderInvoiceInvolvementResponseDto>();
            foreach (var provider in providers)
            {
                var count = await providerRepository.FindInvoiceCountForProvider(provider);
                responses.Add(new ProviderInvoiceInvolvementResponseDto(provider.Id, provider.Name, count));
            }
            return new ProviderInvoiceInvolvementResponsesDto(responses);
        }
    }
}
