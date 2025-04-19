using MediatR;
using StoreManager.Application.BusinessPartner.Provider.DTO.Search;

namespace StoreManager.Application.BusinessPartner.Provider.Command.Search
{
    public record FindProviderByIdQuery(string Id) : IRequest<ProviderFindResponseDto?>;
}
