

using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Application.User.Repository
{
    public interface IUserRepository
    {
        Task<UserModel> FindByUsernameAsync(string username);
        Task<UserModel> CreateAsync(UserModel user);
    }
}
