namespace StoreManager.Infrastructure.Auth.DTO
{
    public sealed record LoginResponseDto(string AccessToken, string RefreshToken, string Role);
}