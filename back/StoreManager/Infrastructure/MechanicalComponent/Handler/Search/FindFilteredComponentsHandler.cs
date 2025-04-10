using DocumentFormat.OpenXml.Wordprocessing;
using MediatR;
using StoreManager.Infrastructure.MechanicalComponent.Command.Search;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Search;
using StoreManager.Infrastructure.MechanicalComponent.Repository;
using StoreManager.Infrastructure.Shared;
using static Supabase.Gotrue.Constants;

namespace StoreManager.Infrastructure.MechanicalComponent.Handler.Search
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

            var result = await repository.FindFiltered(id, request.ComponentInfo, request.PageNumber, request.PageSize);
            return new PaginatedResult<MechanicalComponentSearchResponseDto>
            {
                Items = result.Items.Select(mc =>
                new MechanicalComponentSearchResponseDto(
                    mc.Id,
                    mc.Identifier,
                    mc.Name,
                    mc.Items.Select(ii =>
                    new MechanicalComponentSearchInvoiceResponseDto(
                        ii.Invoice.Id,
                        ii.Invoice.DateIssued,
                       new MechanicalComponentSearchProviderResponseDto(
                           ii.Invoice.Provider.Id,
                           ii.Invoice.Provider.Name,
                           ii.Invoice.Provider.Adress,
                           ii.Invoice.Provider.PhoneNumber
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
