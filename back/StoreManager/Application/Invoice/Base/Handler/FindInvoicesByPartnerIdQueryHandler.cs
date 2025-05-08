using MediatR;
using StoreManager.Application.BusinessPartner.Base.Errors;
using StoreManager.Application.BusinessPartner.Base.Repository;
using StoreManager.Application.Common;
using StoreManager.Application.Invoice.Base.Command;
using StoreManager.Application.Invoice.Base.DTO;
using StoreManager.Application.Invoice.Base.Error;
using StoreManager.Application.Invoice.Export.Repository;
using StoreManager.Application.Invoice.Import.Repository;
using StoreManager.Domain.BusinessPartner.Base.Model;
using StoreManager.Domain.Invoice.Base.Repository;
using StoreManager.Domain.Invoice.Import.Specification;
using StoreManager.Infrastructure.Invoice.Base;
using StoreManager.Infrastructure.Invoice.Import.Repository.Specification;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Application.Invoice.Base.Handler;

public class FindInvoicesByPartnerIdQueryHandler(
    IExportRepository exportRepository,
    IImportRepository importRepository,
    IBusinessPartnerRepository businessPartnerRepository)
    : IRequestHandler<FindInvoicesByPartnerIdQuery, Result<List<InvoiceFindResponseDto>>>
{
    public async Task<Result<List<InvoiceFindResponseDto>>> Handle(FindInvoicesByPartnerIdQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.Id, out var partnerId))
            return InvoiceErrors.InvoiceIdParseError;

        var partner = await businessPartnerRepository.FindById(partnerId);
        if (partner == null)
            return BusinessPartnerErrors.PartnerNotFoundError;

        switch (partner.Type)
        {
            case BusinessPartnerType.Exporter:
            {
                var invoices = await exportRepository.FindByExporterIdAsync(partner.Id);
                return Result.Success(invoices.Select(p => new InvoiceFindResponseDto(p.Id, p.DateIssued)).ToList());
            }
            case BusinessPartnerType.Provider:
            {
                var invoices = await importRepository.FindByProviderId(new ImportBlank(), partner.Id);
                return Result.Success(invoices.Select(p => new InvoiceFindResponseDto(p.Id, p.DateIssued)).ToList());
            }
            default:
                return BusinessPartnerErrors.InvalidBusinessPartnerTypeError;
        }
    }
}