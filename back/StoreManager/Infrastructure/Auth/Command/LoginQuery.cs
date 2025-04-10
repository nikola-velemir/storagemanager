using MediatR;
using StoreManager.Infrastructure.Auth.DTO;

namespace StoreManager.Infrastructure.Auth.Command
{
    public record LoginQuery(string Username, string Password) : IRequest<LoginResponseDto?>;

}
