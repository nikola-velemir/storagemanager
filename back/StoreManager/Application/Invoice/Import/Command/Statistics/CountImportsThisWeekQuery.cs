using MediatR;
using StoreManager.Application.Invoice.Import.DTO.Statistics;

namespace StoreManager.Application.Invoice.Import.Command.Statistics
{
    public record CountImportsThisWeekQuery() : IRequest<ThisWeekInvoiceCountResponseDto>;
}
