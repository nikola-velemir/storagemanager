using StoreManager.Infrastructure.Auth.DTO;
using StoreManager.Infrastructure.Auth.TokenGenerator;
using StoreManager.Infrastructure.User.Model;
using StoreManager.Infrastructure.User.Repository;

namespace StoreManager.Infrastructure.Auth.Service
{
    public class LoginService : ILoginService
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUserRepository _userRepository;
        public LoginService(ITokenGenerator tokenGenerator,IUserRepository userRepository)
        {
            _tokenGenerator = tokenGenerator;
            _userRepository = userRepository;
        }
        public LoginResponseDTO? Authenticate(string username,string password)
        {
            UserModel user = _userRepository.FindByUsername(username);
            if(user.Password != password) { throw new UnauthorizedAccessException("Invalid password"); }

            

            var token = _tokenGenerator.GenerateToken(username);
            var role = "admin";

            return new LoginResponseDTO(token, role);

        }
    }
}
