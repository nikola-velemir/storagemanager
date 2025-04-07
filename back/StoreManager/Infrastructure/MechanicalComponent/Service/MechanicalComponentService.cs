
using StoreManager.Infrastructure.MechanicalComponent.DTO;
using StoreManager.Infrastructure.MechanicalComponent.Repository;
using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.MechanicalComponent.Service
{
    public class MechanicalComponentService : IMechanicalComponentService
    {
        private readonly IMechanicalComponentRepository _repository;
        public MechanicalComponentService(IMechanicalComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<MechanicalComponentFindResponseDTO>> FindFiltered(string? providerId, string? componentInfo, int pageNumber, int pageSize)
        {
            Guid? id = null;
            if (Guid.TryParse(providerId, out var tempId))
            {
                id = tempId;
            }

            var result = await _repository.FindFiltered(id, componentInfo, pageNumber, pageSize);
            return new PaginatedResult<MechanicalComponentFindResponseDTO>
            {
                Items = result.Items.Select(mc =>
                new MechanicalComponentFindResponseDTO(
                    mc.Id,
                    mc.Identifier,
                    mc.Name,
                    mc.Items.Select(ii =>
                    new MechanicalComponentInvoiceResponseDTO(
                        ii.Invoice.Id,
                        ii.Invoice.DateIssued,
                       new MechanicalComponentProviderResponseDTO(
                           ii.Invoice.Provider.Id,
                           ii.Invoice.Provider.Name,
                           ii.Invoice.Provider.Adress,
                           ii.Invoice.Provider.PhoneNumber
                           )
                       )).ToList()
                    )
                )
                .ToList(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = result.TotalCount
            };
        }
    }
}
