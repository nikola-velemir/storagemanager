namespace StoreManager.Infrastructure.Game.DTO
{
    public sealed record class GameResponseDTO(
        int Id,
        string Name,
        string Genre, 
        decimal Price,
        DateOnly ReleaseDate);
}
