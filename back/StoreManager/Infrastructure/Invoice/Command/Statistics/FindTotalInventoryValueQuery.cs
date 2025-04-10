using MediatR;
using StoreManager.Infrastructure.Invoice.DTO.Statistics;

namespace StoreManager.Infrastructure.Invoice.Command.Statistics
{
    public record FindTotalInventoryValueQuery() : IRequest<TotalInvetoryValueResponseDto>;
}
