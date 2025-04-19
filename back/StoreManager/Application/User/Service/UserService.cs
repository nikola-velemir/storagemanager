using StoreManager.Application.User.DTO;
using StoreManager.Application.User.Repository;
using StoreManager.Infrastructure.User.Model;
using StoreManager.Infrastructure.User.Service;

namespace StoreManager.Application.User.Service
{
    public class UserService(IUserRepository repository) : IUserService
    {
        public async Task<UserCreateResponseDto> CreateUser(UserCreateRequestDto request)
        {
            if (!Enum.TryParse<UserRole>(request.Role, true, out var role))
            {
                throw new BadHttpRequestException("Invalid role");
            }
            

            var createdUser = await repository.Create(new UserModel(request.Username, request.Password, request.FirstName, request.LastName, role));
            var response = new UserCreateResponseDto(createdUser.Username, createdUser.Password, createdUser.FirstName, createdUser.LastName, createdUser.Role.ToString());
            return response;
        }
    }
}
