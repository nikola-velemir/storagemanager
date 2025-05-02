using MediatR;
using StoreManager.Application.Product.Blueprint.Command;
using StoreManager.Application.Product.Blueprint.DTO;
using StoreManager.Application.Product.Blueprint.Repository;

namespace StoreManager.Application.Product.Blueprint.Handler;

public class FindProductByInvoiceIdQueryHandler(IProductBlueprintRepository blueprintRepository)
    : IRequestHandler<FindProductBlueprintsByInvoiceIdQuery, FindProductByInvoiceIdResponsesDto>
{
    public async Task<FindProductByInvoiceIdResponsesDto> Handle(FindProductBlueprintsByInvoiceIdQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.InvoiceId, out _))
            throw new InvalidCastException("Could not cast Invoice Id to Guid");
        var invoiceId = Guid.Parse(request.InvoiceId);
        var products = await blueprintRepository.FindByInvoiceIdAsync(invoiceId);
        return new FindProductByInvoiceIdResponsesDto(
            products.Select(p => new FindProductByInvoiceIdResponseDto(
                    p.Id, 
                    p.Name, 
                    p.Identifier,
                    p.DateCreated,
                    p.Exports.First(ee=>ee.ExportId.Equals(invoiceId)).Quantity,
                    p.Exports.First(ee=>ee.ExportId.Equals(invoiceId)).PricePerPiece))
                .ToList()
        );
    }
}