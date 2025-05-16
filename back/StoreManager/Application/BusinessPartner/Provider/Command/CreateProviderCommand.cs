using MediatR;
using StoreManager.Application.BusinessPartner.Provider.DTO.Search;
using StoreManager.Application.Common;

namespace StoreManager.Application.BusinessPartner.Provider.Command;

public record CreateProviderCommand(string Name, string Address, string PhoneNumber)
    : IRequest<Result<ProviderFindResponseDto>>;