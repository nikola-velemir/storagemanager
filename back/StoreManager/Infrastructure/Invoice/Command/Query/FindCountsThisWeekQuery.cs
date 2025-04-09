using MediatR;
using StoreManager.Infrastructure.Invoice.DTO;

namespace StoreManager.Infrastructure.Invoice.Command.Query
{
    public record class FindCountsThisWeekQuery() : IRequest<FindCountsForWeekResponseDTO>;
}
