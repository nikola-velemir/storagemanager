using StoreManager.Infrastructure.Shared;

namespace StoreManager.Infrastructure.MechanicalComponent.DTO
{
    public record class MechanicalComponentSearchResponsesDTO(PaginatedResult<MechanicalComponentFindResponseDTO> responses);
}
