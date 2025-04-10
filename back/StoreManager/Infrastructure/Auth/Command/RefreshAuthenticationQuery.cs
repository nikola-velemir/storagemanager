using MediatR;
using StoreManager.Infrastructure.Auth.DTO;

namespace StoreManager.Infrastructure.Auth.Command
{
    public record RefreshAuthenticationQuery(string RefreshToken) : IRequest<LoginResponseDto?>;
    
}
