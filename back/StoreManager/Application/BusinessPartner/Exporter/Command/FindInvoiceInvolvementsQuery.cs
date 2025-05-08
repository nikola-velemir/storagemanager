using MediatR;
using StoreManager.Application.BusinessPartner.Exporter.DTO;
using StoreManager.Application.Common;

namespace StoreManager.Application.BusinessPartner.Exporter.Command;

public record FindInvoiceInvolvementsQuery() : IRequest<Result<ExporterInvoiceInvolvementResponsesDto>>;