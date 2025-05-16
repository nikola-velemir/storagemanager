using MediatR;
using StoreManager.Application.BusinessPartner.Base.Errors;
using StoreManager.Application.Common;
using StoreManager.Application.Product.Blueprint.Command;
using StoreManager.Application.Product.Blueprint.DTO;
using StoreManager.Application.Product.Blueprint.Repository;

namespace StoreManager.Application.Product.Blueprint.Handler;

public class FindProductsByPartnerQueryHandler(IProductBlueprintRepository productBlueprintRepository)
    : IRequestHandler<FindProductBlueprintsByPartnerQuery, Result< List<ProductSearchResponseDto>>>
{
    public async Task<Result< List<ProductSearchResponseDto>>> Handle(FindProductBlueprintsByPartnerQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.Id, out var exporterId))
            return BusinessPartnerErrors.PartnerIdParseError;

        var products = await productBlueprintRepository.FindByExporterIdAsync(exporterId);
        var response = products.Select(p => new ProductSearchResponseDto(p.Id, p.Name, p.Identifier, p.DateCreated)).ToList();
        return Result.Success(response);
    }
}