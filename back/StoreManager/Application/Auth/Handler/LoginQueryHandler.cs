using MediatR;
using StoreManager.Application.Auth.DTO;
using StoreManager.Application.Auth.Query;
using StoreManager.Application.Auth.Tokens;
using StoreManager.Application.Auth.Tokens.RefreshToken;
using StoreManager.Application.User.Repository;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Application.Auth.Handler
{
    public class LoginQueryHandler(
        IUserRepository userRepository,
        IAccessTokenGenerator tokenGenerator,
        IRefreshTokenRepository refreshTokenRepository)
        : IRequestHandler<LoginQuery, LoginResponseDto?>
    {
        public async Task<LoginResponseDto?> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            Validate(request);
            var user = await userRepository.FindByUsername(request.Username);
            if (user.Password != request.Username)
            {
                throw new UnauthorizedAccessException("Invalid password");
            }


            var role = user.Role.ToString();
            var accessToken = tokenGenerator.GenerateToken(request.Username, role);

            var refreshToken = await refreshTokenRepository.Create(user);

            return new LoginResponseDto(accessToken, refreshToken.Token, role);
        }

        public static void Validate(LoginQuery request)
        {
            var errors = new List<string>();
            if(string.IsNullOrEmpty(request.Username) || string.IsNullOrWhiteSpace(request.Username))
                errors.Add("Username is required");
            if(string.IsNullOrEmpty(request.Password) || string.IsNullOrWhiteSpace(request.Password))
                errors.Add("Password is required");
            if (errors.Count != 0)
                throw new ValidationException(string.Join(" ", errors));
        }
    }
}