using MediatR;
using Microsoft.AspNetCore.Identity;
using StoreManager.Application.Auth.DTO;
using StoreManager.Application.Auth.Query;
using StoreManager.Application.Auth.Tokens;
using StoreManager.Application.Auth.Tokens.RefreshToken;
using StoreManager.Application.User.Repository;
using StoreManager.Domain;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Application.Auth.Handler
{
    public class LoginQueryHandler(
        IUnitOfWork unitOfWork,
        IPasswordHasher<Domain.User.Model.User> passwordHasher,
        IUserRepository userRepository,
        IAccessTokenGenerator tokenGenerator,
        IRefreshTokenRepository refreshTokenRepository)
        : IRequestHandler<LoginQuery, LoginResponseDto?>
    {
        public async Task<LoginResponseDto?> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            Validate(request);
            var user = await userRepository.FindByUsernameAsync(request.Username);
            if (!VerifyPassword(user, request.Password))
            {
                throw new UnauthorizedAccessException("Invalid password");
            }


            var role = user.Role.ToString();
            var accessToken = tokenGenerator.GenerateToken(request.Username, role);

            var refreshToken = await refreshTokenRepository.CreateAsync(user);

            await unitOfWork.CommitAsync(cancellationToken);

            return new LoginResponseDto(accessToken, refreshToken.Token, role);
        }

        private bool VerifyPassword(Domain.User.Model.User user, string providedPassword)
        {
            var result = passwordHasher.VerifyHashedPassword(user, user.Password, providedPassword);
            return result == PasswordVerificationResult.Success;
        }

        private static void Validate(LoginQuery request)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrWhiteSpace(request.Username))
                errors.Add("Username is required");
            if (string.IsNullOrEmpty(request.Password) || string.IsNullOrWhiteSpace(request.Password))
                errors.Add("Password is required");
            if (errors.Count != 0)
                throw new ValidationException(string.Join(" ", errors));
        }
    }
}