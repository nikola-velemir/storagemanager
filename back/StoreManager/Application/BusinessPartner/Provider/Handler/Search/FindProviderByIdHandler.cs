using MediatR;
using StoreManager.Application.BusinessPartner.Base.Errors;
using StoreManager.Application.BusinessPartner.Provider.Command.Search;
using StoreManager.Application.BusinessPartner.Provider.DTO.Search;
using StoreManager.Application.BusinessPartner.Provider.Repository;
using StoreManager.Application.Common;

namespace StoreManager.Application.BusinessPartner.Provider.Handler.Search
{
    public class FindProviderByIdHandler(IProviderRepository providerRepository)
        : IRequestHandler<FindProviderByIdQuery, Result<ProviderFindResponseDto>>
    {
        public async Task<Result<ProviderFindResponseDto>> Handle(FindProviderByIdQuery request,
            CancellationToken cancellationToken)
        {
            if (Guid.TryParse(request.Id, out _))
            {
                return BusinessPartnerErrors.PartnerIdParseError;
            }

            var providerGuid = Guid.Parse(request.Id);
            var provider = await providerRepository.FindByIdAsync(providerGuid);
            if (provider is null)
            {
                return BusinessPartnerErrors.PartnerNotFoundError;
            }

            var response = new ProviderFindResponseDto(provider.Id, provider.Name,
                Utils.FormatAddress(provider.Address),
                provider.PhoneNumber);
            return Result.Success(response);
        }
    }
}