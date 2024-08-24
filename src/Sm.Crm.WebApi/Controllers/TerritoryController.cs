using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Features.Sales.Queries.GetSale;
using Sm.Crm.Application.Features.Territories.Commands.CreateTerritory;
using Sm.Crm.Application.Features.Territories.Commands.DeleteTerritory;
using Sm.Crm.Application.Features.Territories.Commands.UptadeTerritory;
using Sm.Crm.Application.Features.Territories.Queries;
using Sm.Crm.Application.Features.Territories.Queries.GetTerritory;
using Sm.Crm.Application.Features.Territories.Queries.GetTerritoryById;

namespace Sm.Crm.WebApi.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TerritoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public TerritoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetPaginatedTerritoryQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _mediator.Send(new GetTerritoryByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateTerritoryCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateTerritoryCommand command)
    {
        if (id == command.RegionId)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        return Ok(false);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _mediator.Send(new DeleteTerritoryCommand(id));
        return Ok(response);
    }
}