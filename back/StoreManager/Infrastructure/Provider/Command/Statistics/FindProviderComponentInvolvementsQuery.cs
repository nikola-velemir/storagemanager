using MediatR;
using StoreManager.Infrastructure.Provider.DTO.Statistics;

namespace StoreManager.Infrastructure.Provider.Command.Statistics
{
    public record FindProviderComponentInvolvementsQuery() : IRequest<ProviderComponentInvolvementResponsesDto>;
}
