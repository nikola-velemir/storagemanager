using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Infrastructure.Product.Command;
using StoreManager.Infrastructure.Product.DTO;

namespace StoreManager.Infrastructure.Product.Controller;

[ApiController]
[Route("api/products")]
public class ProductController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductCreateRequestDto request)
    {
        await mediator.Send(new CreateProductCommand(request.Name, request.Description, request.Components));
        return Ok();
    }
}