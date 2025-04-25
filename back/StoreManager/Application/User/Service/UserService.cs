using Microsoft.AspNetCore.Identity;
using StoreManager.Application.User.DTO;
using StoreManager.Application.User.Repository;
using StoreManager.Domain;
using StoreManager.Domain.User.Model;
using StoreManager.Domain.User.Service;
using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Application.User.Service
{
    public class UserService(
        IPasswordHasher<Domain.User.Model.User> passwordHasher,
        IUserRepository repository,
        IUnitOfWork unitOfWork) : IUserService
    {
        public string HashPassword(Domain.User.Model.User user, string plainText)
        {
            return passwordHasher.HashPassword(user, plainText);
        }

        public async Task<UserCreateResponseDto> CreateUser(UserCreateRequestDto request)
        {
            if (!Enum.TryParse<UserRole>(request.Role, true, out var role))
            {
                throw new BadHttpRequestException("Invalid role");
            }

            var user = new Domain.User.Model.User(request.Username,
                request.Password, request.FirstName, request.LastName, role);

            var hashedPassword = HashPassword(user, request.Password);
            user.Password = hashedPassword;

            var createdUser = await repository.CreateAsync(user);
            await unitOfWork.SaveChangesAsync();

            var response = new UserCreateResponseDto(createdUser.Username, createdUser.Password, createdUser.FirstName,
                createdUser.LastName, createdUser.Role.ToString());

            return response;
        }
    }
}