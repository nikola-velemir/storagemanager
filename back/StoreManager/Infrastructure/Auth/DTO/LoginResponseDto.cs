namespace StoreManager.Infrastructure.Auth.DTO
{
    public sealed record class LoginResponseDto(string accessToken, string refreshToken, string role);
}
