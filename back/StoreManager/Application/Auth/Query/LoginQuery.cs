using MediatR;
using StoreManager.Application.Auth.DTO;
using StoreManager.Application.Common;

namespace StoreManager.Application.Auth.Query
{
    public record LoginQuery(string Username, string Password) : IRequest<Result<LoginResponseDto>>;

}
