using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.Product.Batch.Command;
using StoreManager.Application.Product.Batch.DTO;
using StoreManager.Application.Product.Batch.Error;
using StoreManager.Application.Product.Blueprint.Repository;
using StoreManager.Domain;
using StoreManager.Domain.Product.Batch.Model;
using StoreManager.Domain.Product.Batch.Repository;
using StoreManager.Domain.Product.Batch.Service;
using StoreManager.Infrastructure.MiddleWare.Exceptions;

namespace StoreManager.Application.Product.Batch.Handler;

public class CreateProductBatchCommandHandler(
    IProductBatchRepository batchRepository,
    IUnitOfWork unitOfWork,
    IProductBatchCheckService productBatchCheckService,
    IProductBlueprintRepository productBlueprintRepository)
    : IRequestHandler<CreateProductBatchCommand, Result<CreateProductBatchResponseDto>>
{
    public async Task<Result<CreateProductBatchResponseDto>> Handle(CreateProductBatchCommand request,
        CancellationToken cancellationToken)
    {
        var productId = ParseGuid(request.ProductId);
        if (productId == null)
            throw new InvalidCastException("Cant cast guid!");
        var productBlueprint = await productBlueprintRepository.FindByIdAsync(productId.Value);
        if (productBlueprint == null)
            throw new NotFoundException("Not found product blueprint!");
        
        var checkResult = productBatchCheckService.CreateBatch(productBlueprint, request.Quantity);
        if (checkResult.IsFailure)
            return ProductBatchErrors.StockLimitExceeded;
        
        var batch = new ProductBatch
        {
            Blueprint = productBlueprint,
            BlueprintId = productBlueprint.Id,
            Id = Guid.NewGuid(),
            Quantity = request.Quantity
        };

        await batchRepository.CreateAsync(batch);
        await unitOfWork.CommitAsync(cancellationToken);
        var response = new CreateProductBatchResponseDto(batch.Id);
        return Result.Success(response);
    }

    private static Guid? ParseGuid(string guid)
    {
        if (!Guid.TryParse(guid, out var guidParsed))
            return null;
        return guidParsed;
    }
}