using MediatR;
using StoreManager.Infrastructure.Provider.Command;
using StoreManager.Infrastructure.Provider.DTO;
using StoreManager.Infrastructure.Provider.Repository;

namespace StoreManager.Infrastructure.Provider.Handler
{
    public class FindProviderInvolementsQueryHandler : IRequestHandler<FindProviderInvolementsQuery, ProviderInvolvementResponsesDTO>
    {
        private IProviderRepository _providerRepository;
        public FindProviderInvolementsQueryHandler(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }
        public async Task<ProviderInvolvementResponsesDTO> Handle(FindProviderInvolementsQuery request, CancellationToken cancellationToken)
        {
            var providers = await _providerRepository.FindAll();
            var responses = new List<ProviderInvolementResponseDTO>();
            foreach (var provider in providers)
            {
                var count = await _providerRepository.FindInvoiceCountForProvider(provider);
                responses.Add(new ProviderInvolementResponseDTO(provider.Id, provider.Name, count));
            }
            return new ProviderInvolvementResponsesDTO(responses);
        }
    }
}
