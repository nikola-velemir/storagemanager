using MediatR;
using StoreManager.Infrastructure.Provider.DTO.Statistics;

namespace StoreManager.Infrastructure.Provider.Command.Statistics
{
    public record  FindProviderInvoiceInvolvementsQuery() :IRequest<ProviderInvoiceInvolvementResponsesDto>;
}
