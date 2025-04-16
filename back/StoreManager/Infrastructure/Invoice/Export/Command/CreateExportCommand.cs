using MediatR;
using StoreManager.Infrastructure.Invoice.Export.DTO;

namespace StoreManager.Infrastructure.Invoice.Command;

public record CreateExportCommand(string ProviderId, List<CreateExportRequestProductDto> Products) : IRequest;