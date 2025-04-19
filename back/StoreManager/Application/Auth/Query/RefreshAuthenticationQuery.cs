using MediatR;
using StoreManager.Application.Auth.DTO;

namespace StoreManager.Application.Auth.Query
{
    public record RefreshAuthenticationQuery(string RefreshToken) : IRequest<LoginResponseDto?>;
    
}
