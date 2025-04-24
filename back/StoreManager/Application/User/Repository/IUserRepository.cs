

using StoreManager.Domain.User.Model;
using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Application.User.Repository
{
    public interface IUserRepository
    {
        Task<Domain.User.Model.User> FindByUsernameAsync(string username);
        Task<Domain.User.Model.User> CreateAsync(Domain.User.Model.User user);
    }
}
