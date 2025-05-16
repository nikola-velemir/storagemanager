using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Application.BusinessPartner.Base.Command;
using StoreManager.Application.BusinessPartner.Base.DTO;

namespace StoreManager.Presentation.BusinessPartner.Controller;

[ApiController]
[Route("api/business-partners")]
public class BusinessPartnerController(IMediator mediator) : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBusinessPartnerRequest request)
    {
        var response = await mediator.Send(new CreateBusinessPartnerCommand(request.Name, request.PhoneNumber,
            request.Role, request.City, request.Street, request.StreetNumber));
       
        return FromResult(response);
    }

    [HttpGet("info/{id}")]
    public async Task<IActionResult> Create(string id)
    {
        var result = await mediator.Send(new FindBusinessPartnerProfileQuery(id));
        return FromResult(result);

    }
}