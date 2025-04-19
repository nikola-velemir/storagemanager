using MediatR;
using StoreManager.Application.Product.Command;
using StoreManager.Application.Product.DTO;
using StoreManager.Application.Product.Repository;

namespace StoreManager.Application.Product.Handler;

public class FindProductByInvoiceIdQueryHandler(IProductRepository repository)
    : IRequestHandler<FindProductByInvoiceIdQuery, FindProductByInvoiceIdResponsesDto>
{
    public async Task<FindProductByInvoiceIdResponsesDto> Handle(FindProductByInvoiceIdQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.InvoiceId, out _))
            throw new InvalidCastException("Could not cast Invoice Id to Guid");
        var invoiceId = Guid.Parse(request.InvoiceId);
        var products = await repository.FindByInvoiceId(invoiceId);
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