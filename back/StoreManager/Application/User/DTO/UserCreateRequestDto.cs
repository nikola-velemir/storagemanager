namespace StoreManager.Application.User.DTO
{
    public sealed record UserCreateRequestDto(
        string Username, 
        string Password,
        string FirstName,
        string LastName,
        string Role
    );
}
