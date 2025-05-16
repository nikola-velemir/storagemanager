using MediatR;
using StoreManager.Application.Auth.DTO;
using StoreManager.Application.Common;

namespace StoreManager.Application.Auth.Query
{
    public record RefreshAuthenticationQuery(string RefreshToken) : IRequest<Result<LoginResponseDto>>;
    
}
