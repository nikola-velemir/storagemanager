using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Application.BusinessPartner.Base.Command;
using StoreManager.Application.BusinessPartner.Base.DTO;

namespace StoreManager.Presentation.BusinessPartner.Controller;

[ApiController]
[Route("api/business-partners")]
public class BusinessPartnerController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBusinessPartnerRequest request)
    {
        await mediator.Send(new CreateBusinessPartnerCommand(request.Name, request.PhoneNumber,
            request.Role, request.City, request.Street, request.StreetNumber));
        return Ok();
    }
}