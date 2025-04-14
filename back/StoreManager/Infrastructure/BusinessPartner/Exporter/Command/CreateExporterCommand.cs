using MediatR;

namespace StoreManager.Infrastructure.BusinessPartner.Exporter.Command;

public record CreateExporterCommand(string Name, string Address, string PhoneNumber) : IRequest;