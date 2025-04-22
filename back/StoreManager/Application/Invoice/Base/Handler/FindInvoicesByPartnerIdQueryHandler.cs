using MediatR;
using StoreManager.Application.BusinessPartner.Base.Repository;
using StoreManager.Application.Invoice.Base.Command;
using StoreManager.Application.Invoice.Base.DTO;
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
    : IRequestHandler<FindInvoicesByPartnerIdQuery, List<InvoiceFindResponseDto>>
{
    public async Task<List<InvoiceFindResponseDto>> Handle(FindInvoicesByPartnerIdQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.Id, out var partnerId))
            throw new InvalidCastException("Cant cast");

        var partner = await businessPartnerRepository.FindById(partnerId) ??
                      throw new NotFoundException("Partner not found");


        switch (partner.Type)
        {
            case BusinessPartnerType.Exporter:
            {
                var invoices = await exportRepository.FindByExporterIdAsync(partner.Id);
                return invoices.Select(p => new InvoiceFindResponseDto(p.Id, p.DateIssued)).ToList();
            }
            case BusinessPartnerType.Provider:
            {
                var invoices = await importRepository.FindByProviderId(new ImportBlank(), partner.Id);
                return invoices.Select(p => new InvoiceFindResponseDto(p.Id, p.DateIssued)).ToList();
            }
            default:
                throw new Exception();
        }
    }
}