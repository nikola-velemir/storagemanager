namespace StoreManager.Infrastructure.MechanicalComponent.DTO.Quantity
{
    public sealed record MechanicalComponentTopFiveQuantityResponsesDto(
        List<MechanicalComponentTopFiveQuantityResponseDto> components);
}