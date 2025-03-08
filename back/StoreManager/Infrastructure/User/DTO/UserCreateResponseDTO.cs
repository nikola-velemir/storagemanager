namespace StoreManager.Infrastructure.User.DTO
{
    public sealed record class UserCreateResponseDTO(
        string Username,
        string Password,
        string FirstName,
        string LastName,
        string Role);
}
