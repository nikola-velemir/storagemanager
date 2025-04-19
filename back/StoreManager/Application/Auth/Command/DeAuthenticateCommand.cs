using MediatR;

namespace StoreManager.Application.Auth.Command
{
    public record DeAuthenticateCommand(string AccessToken) :IRequest;
}
