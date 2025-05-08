using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.Invoice.Base.Error;
using StoreManager.Application.Product.Blueprint.Command;
using StoreManager.Application.Product.Blueprint.DTO;
using StoreManager.Application.Product.Blueprint.Repository;

namespace StoreManager.Application.Product.Blueprint.Handler;

public class FindProductByInvoiceIdQueryHandler(IProductBlueprintRepository blueprintRepository)
    : IRequestHandler<FindProductBlueprintsByInvoiceIdQuery, Result<FindProductByInvoiceIdResponsesDto>>
{
    public async Task<Result<FindProductByInvoiceIdResponsesDto>> Handle(FindProductBlueprintsByInvoiceIdQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.InvoiceId, out _))
            return InvoiceErrors.InvoiceIdParseError;

        var invoiceId = Guid.Parse(request.InvoiceId);
        var products = await blueprintRepository.FindByInvoiceIdAsync(invoiceId);
        return Result.Success(new FindProductByInvoiceIdResponsesDto(
            products.Select(p => new FindProductByInvoiceIdResponseDto(
                    p.Id,
                    p.Name,
                    p.Identifier,
                    p.DateCreated,
                    p.Exports.First(ee => ee.ExportId.Equals(invoiceId)).Quantity,
                    p.Exports.First(ee => ee.ExportId.Equals(invoiceId)).PricePerPiece))
                .ToList()
        ));
    }
}