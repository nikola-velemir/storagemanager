

using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Infrastructure.User.Repository
{
    public interface IUserRepository
    {
        Task<UserModel> FindByUsername(string username);
        Task<UserModel> Create(UserModel user);
    }
}
