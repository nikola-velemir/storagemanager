using MediatR;
using StoreManager.Infrastructure.Provider.DTO.Search;

namespace StoreManager.Infrastructure.Provider.Command.Search
{
    public record FindAllProvidersQuery() : IRequest<ProviderFindResponsesDto>;
}
