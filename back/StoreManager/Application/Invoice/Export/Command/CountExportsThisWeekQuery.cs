using MediatR;
using StoreManager.Application.Invoice.Import.DTO.Statistics;

namespace StoreManager.Application.Invoice.Export.Command;

public record CountExportsThisWeekQuery() :  IRequest<ThisWeekInvoiceCountResponseDto>;