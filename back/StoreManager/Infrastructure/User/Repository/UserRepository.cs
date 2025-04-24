using Microsoft.EntityFrameworkCore;
using StoreManager.Application.User.Repository;
using StoreManager.Domain.User.Model;
using StoreManager.Infrastructure.Context;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Infrastructure.User.Repository
{
    public class UserRepository(WarehouseDbContext context) : IUserRepository
    {
        private readonly DbSet<Domain.User.Model.User> _users = context.Users;

        public async Task<Domain.User.Model.User> CreateAsync(Domain.User.Model.User user)
        {
            var saved = await _users.AddAsync(user);
            await context.SaveChangesAsync();
            return saved.Entity;
        }

        public async Task<Domain.User.Model.User> FindByUsernameAsync(string username)
        {
            return await context.Users.FirstOrDefaultAsync(i => i.Username.Equals(username))
                ?? throw new InvalidOperationException("Not found");
        }
    }
}
