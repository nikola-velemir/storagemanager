namespace StoreManager.Infrastructure.Auth.DTO
{
    public sealed record class LoginRequestDto(string username, string password);
}
