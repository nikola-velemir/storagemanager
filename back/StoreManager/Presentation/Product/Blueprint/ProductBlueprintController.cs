using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Application.Product.Blueprint.Command;
using StoreManager.Application.Product.Blueprint.DTO;

namespace StoreManager.Presentation.Product.Blueprint;

[ApiController]
[Authorize]
[Route("api/products")]
public class ProductBlueprintController(IMediator mediator) : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductCreateRequestDto request)
    {
        var result = await mediator.Send(new CreateProductBlueprintCommand(request.Name, request.Identifier,
            request.Description,
            request.Components));
        return FromResult(result);
    }

    [HttpGet("filtered")]
    public async Task<IActionResult> FindFiltered([FromQuery] string? productInfo, [FromQuery] int pageNumber,
        [FromQuery] int pageSize, [FromQuery] string? dateCreated)
    {
        var result =
            await mediator.Send(new FindFilteredProductBlueprintsQuery(productInfo, dateCreated, pageNumber, pageSize));
        return FromResult(result);
    }

    [HttpGet("info/{id}")]
    public async Task<IActionResult> FindProductInfo([FromRoute] string id)
    {
        var result = await mediator.Send(new FindProductBlueprintInfoQuery(id));
        return FromResult(result);
    }

    [HttpGet("find-by-invoice/{invoiceId}")]
    public async Task<IActionResult> FindProductByInvoice([FromRoute] string invoiceId)
    {
        var result = await mediator.Send(new FindProductBlueprintsByInvoiceIdQuery(invoiceId));
        return FromResult(result);
    }

    [HttpGet("partner/{partnerId}")]
    public async Task<IActionResult> FindProductByPartner([FromRoute] string partnerId)
    {
        var result = await mediator.Send(new FindProductBlueprintsByPartnerQuery(partnerId));
        return FromResult(result);
    }
}