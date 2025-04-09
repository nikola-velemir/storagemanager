using MediatR;
using StoreManager.Infrastructure.Provider.Command.Statistics;
using StoreManager.Infrastructure.Provider.DTO.Statistics;
using StoreManager.Infrastructure.Provider.Repository;

namespace StoreManager.Infrastructure.Provider.Handler.Statistics
{
    public class FindProviderInvoiceInvolementsQueryHandler : IRequestHandler<FindProviderInvoiceInvolementsQuery, ProviderInvoiceInvolvementResponsesDTO>
    {
        private IProviderRepository _providerRepository;
        public FindProviderInvoiceInvolementsQueryHandler(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }
        public async Task<ProviderInvoiceInvolvementResponsesDTO> Handle(FindProviderInvoiceInvolementsQuery request, CancellationToken cancellationToken)
        {
            var providers = await _providerRepository.FindAll();
            var responses = new List<ProviderInvoiceInvolementResponseDTO>();
            foreach (var provider in providers)
            {
                var count = await _providerRepository.FindInvoiceCountForProvider(provider);
                responses.Add(new ProviderInvoiceInvolementResponseDTO(provider.Id, provider.Name, count));
            }
            return new ProviderInvoiceInvolvementResponsesDTO(responses);
        }
    }
}
