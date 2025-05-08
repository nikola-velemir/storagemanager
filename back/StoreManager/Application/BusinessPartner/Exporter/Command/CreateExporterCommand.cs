using MediatR;
using StoreManager.Application.Common;

namespace StoreManager.Application.BusinessPartner.Exporter.Command;

public record CreateExporterCommand(string Name, string Address, string PhoneNumber) : IRequest<Result>;