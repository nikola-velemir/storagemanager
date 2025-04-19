using MediatR;

namespace StoreManager.Application.BusinessPartner.Exporter.Command;

public record CreateExporterCommand(string Name, string Address, string PhoneNumber) : IRequest;