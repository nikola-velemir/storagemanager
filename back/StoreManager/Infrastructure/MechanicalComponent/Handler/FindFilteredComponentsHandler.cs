using DocumentFormat.OpenXml.Wordprocessing;
using MediatR;
using StoreManager.Infrastructure.MechanicalComponent.Command;
using StoreManager.Infrastructure.MechanicalComponent.DTO.Search;
using StoreManager.Infrastructure.MechanicalComponent.Repository;
using StoreManager.Infrastructure.Shared;
using static Supabase.Gotrue.Constants;

namespace StoreManager.Infrastructure.MechanicalComponent.Handler
{
    public class FindFilteredComponentsHandler : IRequestHandler<FindFilteredComponentsQuery, PaginatedResult<MechanicalComponentSearchResponseDTO>>
    {
        private IMechanicalComponentRepository _repository;
        public FindFilteredComponentsHandler(IMechanicalComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<MechanicalComponentSearchResponseDTO>> Handle(FindFilteredComponentsQuery request, CancellationToken cancellationToken)
        {
            Guid? id = null;
            if (Guid.TryParse(request.ProviderId, out var tempId))
            {
                id = tempId;
            }

            var result = await _repository.FindFiltered(id, request.ComponentInfo, request.PageNumber, request.PageSize);
            return new PaginatedResult<MechanicalComponentSearchResponseDTO>
            {
                Items = result.Items.Select(mc =>
                new MechanicalComponentSearchResponseDTO(
                    mc.Id,
                    mc.Identifier,
                    mc.Name,
                    mc.Items.Select(ii =>
                    new MechanicalComponentSearchInvoiceResponseDTO(
                        ii.Invoice.Id,
                        ii.Invoice.DateIssued,
                       new MechanicalComponentSearchProviderResponseDTO(
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
