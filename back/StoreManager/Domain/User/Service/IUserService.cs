using StoreManager.Application.User.DTO;
using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Infrastructure.User.Service
{
    public interface IUserService
    {
        public Task<UserCreateResponseDto> CreateUser(UserCreateRequestDto user);
    }
}
