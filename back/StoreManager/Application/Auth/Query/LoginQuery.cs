using MediatR;
using StoreManager.Application.Auth.DTO;

namespace StoreManager.Application.Auth.Query
{
    public record LoginQuery(string Username, string Password) : IRequest<LoginResponseDto?>;

}
