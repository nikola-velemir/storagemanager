using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.Invoice.Export.DTO;

namespace StoreManager.Application.Invoice.Export.Command;

public record CreateExportCommand(string ProviderId, List<CreateExportRequestProductDto> Products) : IRequest<Result>;