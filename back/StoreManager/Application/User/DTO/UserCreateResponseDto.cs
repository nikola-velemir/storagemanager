namespace StoreManager.Application.User.DTO
{
    public sealed record UserCreateResponseDto(
        string Username,
        string Password,
        string FirstName,
        string LastName,
        string Role);
}
