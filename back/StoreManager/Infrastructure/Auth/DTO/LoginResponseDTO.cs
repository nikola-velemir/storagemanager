namespace StoreManager.Infrastructure.Auth.DTO
{
    public sealed record class LoginResponseDTO(string access_token,string role);
}
