using MediatR;
using StoreManager.Application.BusinessPartner.Provider.DTO.Search;

namespace StoreManager.Application.BusinessPartner.Provider.Command.Search
{
    public record FindAllProvidersQuery() : IRequest<ProviderFindResponsesDto>;
}
