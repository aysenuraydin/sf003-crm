using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Features.Notifications.Commands.CreateNotification;
using Sm.Crm.Application.Features.Notifications.Commands.DeleteNotification;
using Sm.Crm.Application.Features.Notifications.Commands.UpdateNotification;
using Sm.Crm.Application.Features.Notifications.Queries.GetNotification;
using Sm.Crm.Application.Features.Notifications.Queries.GetNotificationById;

namespace Sm.Crm.WebApi.Controllers;
public class NotificationController : FeatureController
{
    private readonly IMediator _mediator;

    public NotificationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetPaginatedNotificationsQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _mediator.Send(new GetNotificationByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateNotificationCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateNotificationCommand command)
    {
        if (id.Equals(command.id))
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        return Ok(false);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _mediator.Send(new DeleteNotificationCommand(id));
        return Ok(response);
    }
}
