using MediatR;
using StoreManager.Infrastructure.Invoice.Import.DTO.Statistics;

namespace StoreManager.Infrastructure.Invoice.Import.Command.Statistics
{
    public record FindTotalInventoryValueQuery() : IRequest<TotalInventoryValueResponseDto>;
}
