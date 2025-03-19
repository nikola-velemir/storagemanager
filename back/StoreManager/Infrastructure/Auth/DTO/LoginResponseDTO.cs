namespace StoreManager.Infrastructure.Auth.DTO
{
    public sealed record class LoginResponseDTO(string accessToken, string refreshToken, string role);
}
