using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Infrastructure.User.DTO
{
    public sealed record UserCreateRequestDto(
        string Username, 
        string Password,
        string FirstName,
        string LastName,
        string Role
    );
}
