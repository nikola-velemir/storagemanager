using MediatR;
using StoreManager.Infrastructure.BusinessPartner.Provider.DTO.Search;

namespace StoreManager.Infrastructure.BusinessPartner.Provider.Command.Search
{
    public record FindProviderByIdQuery(string Id) : IRequest<ProviderFindResponseDto?>;
}
