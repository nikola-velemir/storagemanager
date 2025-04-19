using MediatR;
using StoreManager.Application.MechanicalComponent.Command.Search;
using StoreManager.Application.MechanicalComponent.DTO.Search;
using StoreManager.Application.MechanicalComponent.Repository;
using StoreManager.Application.Shared;
using StoreManager.Infrastructure.Shared;
using static Supabase.Gotrue.Constants;

namespace StoreManager.Application.MechanicalComponent.Handler.Search
{
    public class FindFilteredComponentsHandler(IMechanicalComponentRepository repository)
        : IRequestHandler<FindFilteredComponentsQuery, PaginatedResult<MechanicalComponentSearchResponseDto>>
    {
        public async Task<PaginatedResult<MechanicalComponentSearchResponseDto>> Handle(FindFilteredComponentsQuery request, CancellationToken cancellationToken)
        {
            Guid? id = null;
            if (Guid.TryParse(request.ProviderId, out var tempId))
            {
                id = tempId;
            }

            var result = await repository.FindFilteredAsync(id, request.ComponentInfo, request.PageNumber, request.PageSize);
            return new PaginatedResult<MechanicalComponentSearchResponseDto>
            {
                Items = result.Items.Select(mc =>
                new MechanicalComponentSearchResponseDto(
                    mc.Id,
                    mc.Identifier,
                    mc.Name,
                    mc.Items.Select(ii =>
                    new MechanicalComponentSearchInvoiceResponseDto(
                        ii.Import.Id,
                        ii.Import.DateIssued,
                       new MechanicalComponentSearchProviderResponseDto(
                           ii.Import.Provider.Id,
                           ii.Import.Provider.Name,
                           ii.Import.Provider.Address,
                           ii.Import.Provider.PhoneNumber
                           )
                    )).ToList()
                    )
                )
                .ToList(),
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = result.TotalCount
            };
        }
    }
}
