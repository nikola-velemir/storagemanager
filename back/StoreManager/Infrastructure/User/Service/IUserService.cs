using StoreManager.Infrastructure.User.DTO;
using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Infrastructure.User.Service
{
    public interface IUserService
    {

        public Task CreateUser(UserCreateRequestDTO user);
    }
}
