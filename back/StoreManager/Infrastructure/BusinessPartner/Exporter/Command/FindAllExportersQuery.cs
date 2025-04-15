using MediatR;
using StoreManager.Infrastructure.BusinessPartner.Exporter.DTO;

namespace StoreManager.Infrastructure.BusinessPartner.Exporter.Command;

public record FindAllExportersQuery() : IRequest<FindExporterResponsesDto>;