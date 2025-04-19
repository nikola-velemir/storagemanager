using MediatR;
using StoreManager.Application.BusinessPartner.Provider.DTO.Statistics;

namespace StoreManager.Application.BusinessPartner.Provider.Command.Statistics;

public record FindProviderComponentInvolvementsQuery() : IRequest<ProviderComponentInvolvementResponsesDto>;