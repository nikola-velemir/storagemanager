using MediatR;
using StoreManager.Application.Common;

namespace StoreManager.Application.Auth.Command
{
    public record DeAuthenticateCommand(string AccessToken) :IRequest<Result>;
}
