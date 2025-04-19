using MediatR;
using StoreManager.Application.BusinessPartner.Provider.DTO.Statistics;

namespace StoreManager.Application.BusinessPartner.Provider.Command.Statistics
{
    public record  FindProviderInvoiceInvolvementsQuery() :IRequest<ProviderInvoiceInvolvementResponsesDto>;
}
