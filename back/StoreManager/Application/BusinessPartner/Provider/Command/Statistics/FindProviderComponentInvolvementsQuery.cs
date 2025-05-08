using MediatR;
using StoreManager.Application.BusinessPartner.Provider.DTO.Statistics;
using StoreManager.Application.Common;

namespace StoreManager.Application.BusinessPartner.Provider.Command.Statistics;

public record FindProviderComponentInvolvementsQuery() : IRequest<Result<ProviderComponentInvolvementResponsesDto>>;