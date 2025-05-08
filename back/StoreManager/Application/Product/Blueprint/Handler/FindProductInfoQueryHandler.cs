using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.Product.Blueprint.Command;
using StoreManager.Application.Product.Blueprint.DTO;
using StoreManager.Application.Product.Blueprint.Errors;
using StoreManager.Application.Product.Blueprint.Repository;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Application.Product.Blueprint.Handler;

public class FindProductInfoQueryHandler(IProductBlueprintRepository productBlueprintRepository)
    : IRequestHandler<FindProductBlueprintInfoQuery, Result<ProductInfoResponseDto>>
{
    public async Task<Result<ProductInfoResponseDto>> Handle(FindProductBlueprintInfoQuery request, CancellationToken cancellationToken)
    {
        Guid? productGuid = null;
        if (!Guid.TryParse(request.Id, out _))
        {
            throw new InvalidCastException("Could not cast guid");
        }

        productGuid = Guid.Parse(request.Id);
        var product = await productBlueprintRepository.FindByIdAsync(productGuid.Value);
        if (product is null)
        {
            return ProductBlueprintErrors.ProductBlueprintNotFound;
        }
        var response = new ProductInfoResponseDto(
            product.Name,
            product.Description,
            product.Identifier,
            product.DateCreated,
            product.Components.Select(p =>
                    new ProductInfoComponentResponseDto(
                        p.Component.Id,
                        p.Component.Name,
                        p.Component.Identifier,
                        p.UsedQuantity))
                .ToList(),
            product.Exports.Select(e =>
            {
                var exporter = e.Export.Exporter;
                var exporterDto = new FindProductInfoExporterResponseDto(exporter.Id, exporter.Name,
                    Utils.FormatAddress(exporter.Address), exporter.PhoneNumber);
                return new ProductInfoExportResponseDto(e.ExportId, e.Export.DateIssued, exporterDto);
            }).ToList());

        return Result.Success(response);
    }
}