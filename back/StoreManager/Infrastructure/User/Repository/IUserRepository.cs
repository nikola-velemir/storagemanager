

using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Infrastructure.User.Repository
{
    public interface IUserRepository
    {
        UserModel FindByUsername(string username);
        UserModel Create(UserModel user);
    }
}
