using StoreManager.Infrastructure.User.Repository;

namespace StoreManager.Infrastructure.User.Service
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

    }
}
