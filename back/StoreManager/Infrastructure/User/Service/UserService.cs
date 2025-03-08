using Microsoft.AspNetCore.Http.HttpResults;
using StoreManager.Infrastructure.User.DTO;
using StoreManager.Infrastructure.User.Model;
using StoreManager.Infrastructure.User.Repository;

namespace StoreManager.Infrastructure.User.Service
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserCreateResponseDTO> CreateUser(UserCreateRequestDTO request)
        {
            if (!Enum.TryParse<UserRole>(request.Role, true, out var role))
            {
                throw new BadHttpRequestException("Invalid role");
            }
            var createdUser = await _repository.Create(new UserModel(request.Username, request.Password, request.FirstName, request.LastName, role));
            var response = new UserCreateResponseDTO(createdUser.Username, createdUser.Password, createdUser.FirstName, createdUser.LastName, createdUser.Role.ToString());
            return response;
        }
    }
}
