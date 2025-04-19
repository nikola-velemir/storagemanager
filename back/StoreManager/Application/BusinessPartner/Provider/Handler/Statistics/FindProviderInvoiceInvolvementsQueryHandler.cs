using MediatR;
using StoreManager.Application.BusinessPartner.Provider.Command.Statistics;
using StoreManager.Application.BusinessPartner.Provider.DTO.Statistics;
using StoreManager.Application.BusinessPartner.Provider.Repository;

namespace StoreManager.Application.BusinessPartner.Provider.Handler.Statistics
{
    public class FindProviderInvoiceInvolvementsQueryHandler(IProviderRepository providerRepository)
        : IRequestHandler<FindProviderInvoiceInvolvementsQuery, ProviderInvoiceInvolvementResponsesDto>
    {
        public async Task<ProviderInvoiceInvolvementResponsesDto> Handle(FindProviderInvoiceInvolvementsQuery request, CancellationToken cancellationToken)
        {
            var providers = await providerRepository.FindAllAsync();
            var responses = new List<ProviderInvoiceInvolvementResponseDto>();
            foreach (var provider in providers)
            {
                var count = await providerRepository.FindInvoiceCountForProviderAsync(provider);
                responses.Add(new ProviderInvoiceInvolvementResponseDto(provider.Id, provider.Name, count));
            }
            return new ProviderInvoiceInvolvementResponsesDto(responses);
        }
    }
}
