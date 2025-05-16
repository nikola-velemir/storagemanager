using MediatR;
using StoreManager.Application.BusinessPartner.Provider.Command.Search;
using StoreManager.Application.BusinessPartner.Provider.DTO.Search;
using StoreManager.Application.BusinessPartner.Provider.Repository;
using StoreManager.Application.Common;

namespace StoreManager.Application.BusinessPartner.Provider.Handler.Search
{
    public class FindAllProviderHandler(IProviderRepository providerRepository)
        : IRequestHandler<FindAllProvidersQuery, Result<ProviderFindResponsesDto>>
    {
        public async Task<Result<ProviderFindResponsesDto>> Handle(FindAllProvidersQuery request,
            CancellationToken cancellationToken)
        {
            var providers = await providerRepository.FindAllAsync();
            var responses = providers.Select(p =>
                new ProviderFindResponseDto(p.Id, p.Name, Utils.FormatAddress(p.Address), p.PhoneNumber)).ToList();
            
            return Result.Success(new ProviderFindResponsesDto(responses));
        }
    }
}