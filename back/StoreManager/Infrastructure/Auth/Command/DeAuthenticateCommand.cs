using MediatR;

namespace StoreManager.Infrastructure.Auth.Command
{
    public record DeAuthenticateCommand(string AccessToken) :IRequest;
}
