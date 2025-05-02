using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Application.Product.Batch.Command;

namespace StoreManager.Presentation.Product.Batch;

public static class ProductBatchApi
{
    public static void MapProductBatchApi(this WebApplication app)
    {
        var productBatches = app.MapGroup("api/product-batches").RequireAuthorization();
        productBatches.MapPost("/", async ([FromBody] CreateProductBatchCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            if (result.IsFailure)
                return Results.Json(
                    new
                    {
                        result.Error.Name,
                        result.Error.Description
                    },
                    statusCode: result.Error.StatusCode
                );
            
            return Results.Ok(result.Value);
        });
    }
}