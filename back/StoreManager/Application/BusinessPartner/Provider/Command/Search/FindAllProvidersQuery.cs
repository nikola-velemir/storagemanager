using MediatR;
using StoreManager.Application.BusinessPartner.Provider.DTO.Search;
using StoreManager.Application.Common;

namespace StoreManager.Application.BusinessPartner.Provider.Command.Search
{
    public record FindAllProvidersQuery() : IRequest<Result<ProviderFindResponsesDto>>;
}
