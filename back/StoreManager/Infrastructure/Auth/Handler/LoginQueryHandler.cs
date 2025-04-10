using MediatR;
using StoreManager.Infrastructure.Auth.Command;
using StoreManager.Infrastructure.Auth.DTO;
using StoreManager.Infrastructure.Auth.Tokens.AcessToken.Generator;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Repository;
using StoreManager.Infrastructure.User.Model;
using StoreManager.Infrastructure.User.Repository;

namespace StoreManager.Infrastructure.Auth.Handler
{
    public class LoginQueryHandler(
        IUserRepository userRepository,
        IAccessTokenGenerator tokenGenerator,
        IRefreshTokenRepository refreshTokenRepository)
        : IRequestHandler<LoginQuery, LoginResponseDto?>
    {
        public async Task<LoginResponseDto?> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            UserModel user = await userRepository.FindByUsername(request.Username);
            if (user.Password != request.Username)
            {
                throw new UnauthorizedAccessException("Invalid password");
            }


            var role = user.Role.ToString();
            var accessToken = tokenGenerator.GenerateToken(request.Username, role);

            var refreshToken = await refreshTokenRepository.Create(user);

            return new LoginResponseDto(accessToken, refreshToken.Token, role);
        }
    }
}