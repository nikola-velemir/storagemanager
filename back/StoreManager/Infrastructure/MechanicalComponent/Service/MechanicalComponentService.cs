
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

        public async Task<PaginatedResult<MechanicalComponentFindResponseDTO>> FindFiltered(int pageNumber, int pageSize)
        {
            var result = await _repository.FindFiltered(pageNumber, pageSize);
            return new PaginatedResult<MechanicalComponentFindResponseDTO>
            {
                Items = result.Items.Select(mc =>
                new MechanicalComponentFindResponseDTO(mc.Id, mc.Identifier, mc.Name))
                .ToList(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = result.TotalCount
            };
        }
    }
}
