
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

        public async Task<UserModel> Create(UserModel user)
        {
            var saved = await _users.AddAsync(user);
            await _context.SaveChangesAsync();
            return saved.Entity;
        }

        public async Task<UserModel> FindByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(i => i.Username.Equals(username))
                ?? throw new InvalidOperationException("Not found");
        }
    }
}
