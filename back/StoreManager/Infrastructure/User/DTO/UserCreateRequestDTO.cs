using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Infrastructure.User.DTO
{
    public sealed record class UserCreateRequestDTO(
        string Username, 
        string Password,
        string FirstName,
        string LastName,
        string Role
    );
}
