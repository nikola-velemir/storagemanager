
using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Infrastructure.User.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly WarehouseDbContext _context;
        private readonly DbSet<UserModel> _users;
        public UserRepository(WarehouseDbContext context)
        {
            _context = context;
            _users = context.Users;
        }

        public UserModel Create(UserModel user)
        {
             var saved = _users.Add(user);
            _context.SaveChanges();
            return saved.Entity;
        }

        public UserModel FindByUsername(string username)
        {
            return _context.Users.FirstOrDefault(i => i.Username.Equals(username)) ?? throw new InvalidOperationException("Not found");
        }
    }
}
