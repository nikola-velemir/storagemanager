using MediatR;
using StoreManager.Application.BusinessPartner.Exporter.DTO;

namespace StoreManager.Application.BusinessPartner.Exporter.Command;

public record FindInvoiceInvolvementsQuery() : IRequest<ExporterInvoiceInvolvementResponsesDto>;