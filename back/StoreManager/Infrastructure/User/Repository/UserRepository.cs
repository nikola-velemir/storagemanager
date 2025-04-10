
using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Infrastructure.User.Repository
{
    public class UserRepository(WarehouseDbContext context) : IUserRepository
    {
        private readonly DbSet<UserModel> _users = context.Users;

        public async Task<UserModel> Create(UserModel user)
        {
            var saved = await _users.AddAsync(user);
            await context.SaveChangesAsync();
            return saved.Entity;
        }

        public async Task<UserModel> FindByUsername(string username)
        {
            return await context.Users.FirstOrDefaultAsync(i => i.Username.Equals(username))
                ?? throw new InvalidOperationException("Not found");
        }
    }
}
