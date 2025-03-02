
using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Infrastructure.User.Repository
{
    public class UserRepository : IUserRepository
    {
        private List<UserModel> users = new List<UserModel>
        {
            new("admin","password"),
            new("kita","banana")
        };
        public UserModel FindByUsername(string username)
        {
            return users.FirstOrDefault(user => user.Username.Equals(username)) ??
                 throw new InvalidOperationException("Not found");
        }
    }
}
