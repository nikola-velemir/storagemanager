using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.Product.Command;
using StoreManager.Infrastructure.Product.DTO;

namespace StoreManager.Infrastructure.Product.Controller;

[ApiController]
[Authorize]
[Route("api/products")]
public class ProductController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductCreateRequestDto request)
    {
        await mediator.Send(new CreateProductCommand(request.Name, request.Identifier, request.Description,
            request.Components));
        return Ok();
    }

    [HttpGet("filtered")]
    public async Task<IActionResult> FindFiltered([FromQuery] string? productInfo, [FromQuery] int pageNumber,
        [FromQuery] int pageSize, [FromQuery] string? dateCreated)
    {
        var result = await mediator.Send(new FindFilteredProductsQuery(productInfo, dateCreated, pageNumber, pageSize));
        return Ok(result);
    }

    [HttpGet("info/{id}")]
    public async Task<IActionResult> FindProductInfo([FromRoute] string id)
    {
        var result = await mediator.Send(new FindProductInfoQuery(id));
        return Ok(result);
    }
}