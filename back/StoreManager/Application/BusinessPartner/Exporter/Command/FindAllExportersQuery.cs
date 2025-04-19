using MediatR;
using StoreManager.Application.BusinessPartner.Exporter.DTO;

namespace StoreManager.Application.BusinessPartner.Exporter.Command;

public record FindAllExportersQuery() : IRequest<FindExporterResponsesDto>;