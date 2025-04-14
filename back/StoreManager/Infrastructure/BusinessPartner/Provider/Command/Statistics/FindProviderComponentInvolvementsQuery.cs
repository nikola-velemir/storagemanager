using MediatR;
using StoreManager.Infrastructure.BusinessPartner.Provider.DTO.Statistics;

namespace StoreManager.Infrastructure.BusinessPartner.Provider.Command.Statistics
{
    public record FindProviderComponentInvolvementsQuery() : IRequest<ProviderComponentInvolvementResponsesDto>;
}
