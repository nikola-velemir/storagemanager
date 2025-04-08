using MediatR;
using StoreManager.Infrastructure.Auth.DTO;

namespace StoreManager.Infrastructure.Auth.Command
{
    public record RefreshAuthentificationQuery(string RefreshToken) : IRequest<LoginResponseDTO?>;
    
}
