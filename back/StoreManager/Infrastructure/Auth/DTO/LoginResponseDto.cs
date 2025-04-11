namespace StoreManager.Infrastructure.Auth.DTO
{
    public sealed record LoginResponseDto(string accessToken, string refreshToken, string role);
}