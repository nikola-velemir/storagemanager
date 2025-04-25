using StoreManager.Application.User.DTO;

namespace StoreManager.Domain.User.Service
{
    public interface IUserService
    {
        public Task<UserCreateResponseDto> CreateUser(UserCreateRequestDto user);
    }
}
