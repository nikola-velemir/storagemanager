using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Application.Product.Command;
using StoreManager.Application.Product.DTO;

namespace StoreManager.Presentation.Product.Controller;

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

    [HttpGet("find-by-invoice/{invoiceId}")]
    public async Task<IActionResult> FindProductByInvoice([FromRoute] string invoiceId)
    {
        var result = await mediator.Send(new FindProductByInvoiceIdQuery(invoiceId));
        return Ok(result);
    }
}