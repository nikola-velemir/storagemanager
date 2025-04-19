namespace StoreManager.Application.Auth.DTO
{
    public sealed record LoginResponseDto(string AccessToken, string RefreshToken, string Role);
}