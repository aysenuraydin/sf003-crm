using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Features.OfferStatuses.Commands.CreateOfferStatus;
using Sm.Crm.Application.Features.OfferStatuses.Commands.DeleteOfferStatus;
using Sm.Crm.Application.Features.OfferStatuses.Commands.UpdateOfferStatus;
using Sm.Crm.Application.Features.OfferStatuses.Queries.GetOfferStatus;
using Sm.Crm.Application.Features.OfferStatuses.Queries.GetOfferStatusById;

namespace Sm.Crm.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OfferStatusController : ControllerBase
{
    private readonly IMediator _mediator;

    public OfferStatusController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _mediator.Send(new GetAllOfferStatusQuery());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _mediator.Send(new GetOfferStatusByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateOfferStatusCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateOfferStatusCommand command)
    {
        if (id == command.Id)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        return Ok(false);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _mediator.Send(new DeleteOfferStatusCommand(id));
        return Ok(response);
    }
}