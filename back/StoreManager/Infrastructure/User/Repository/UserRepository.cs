using Microsoft.EntityFrameworkCore;
using StoreManager.Application.User.Repository;
using StoreManager.Domain.User.Model;
using StoreManager.Infrastructure.Context;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Infrastructure.User.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DbSet<Domain.User.Model.User> _users ;

        public UserRepository(WarehouseDbContext context)
        {
            _users = context.Users;
        }
        public async Task<Domain.User.Model.User> CreateAsync(Domain.User.Model.User user)
        {
            var saved = await _users.AddAsync(user);
            return saved.Entity;
        }

        public async Task<Domain.User.Model.User> FindByUsernameAsync(string username)
        {
            return await _users.FirstOrDefaultAsync(i => i.Username.Equals(username))
                ?? throw new InvalidOperationException("Not found");
        }
    }
}
