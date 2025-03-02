using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Infrastructure.User.DTO
{
    public record class UserCreateRequestDTO(
        string Username, 
        string Password,
        string FirstName,
        string LastName,
        string Role
    );
}
