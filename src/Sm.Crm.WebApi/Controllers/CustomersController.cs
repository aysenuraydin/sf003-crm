using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sm.Crm.Application.Features.Customers.Commands.CreateCustomer;
using Sm.Crm.Application.Features.Customers.Commands.DeleteCustomer;
using Sm.Crm.Application.Features.Customers.Commands.UpdateCustomer;
using Sm.Crm.Application.Features.Customers.Commands.UploadProfileImage;
using Sm.Crm.Application.Features.Customers.Queries.GetAllCustomers;
using Sm.Crm.Application.Features.Customers.Queries.GetCustomerById;

namespace Sm.Crm.WebApi.Controllers;

public class CustomersController : FeatureController
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetPaginatedCustomersQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        var response = await _mediator.Send(new GetCustomerByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateCustomerCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(long id, UpdateCustomerCommand command)
    {
        if (id == command.Id)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        return Ok(false);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var response = await _mediator.Send(new DeleteCustomerCommand(id));
        return Ok(response);
    }

    [HttpPost("uploadprofileimage")]
    public async Task<IActionResult> UploadProfileImage(UploadProfileImageCommand command)
    {
        var profileImage = command.ProfileImage;

        string[] validExtensions = new[] { ".jpg", ".png" };
        var fileExtension = Path.GetExtension(profileImage.FileName);
        var fileName = Path.GetRandomFileName() + fileExtension;
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

        if (profileImage == null || profileImage.Length == 0)
            return BadRequest("Please select a file!");
        if (profileImage.Length > 5 * 1024 * 1024)
            return BadRequest("Please select maximum 5MB file!");
        if (!validExtensions.Contains(fileExtension))
            return BadRequest("Please select image file with extensions like '.jpg or .png'!");

        using var stream = new FileStream(filePath, FileMode.Create);
        await profileImage.CopyToAsync(stream);

        command.ProfileImageUrl = fileName;

        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpGet("downloadprofileimage")]
    public IActionResult DownloadProfileImage(string fileName)
    {
        if (fileName == "")
            return BadRequest("Please select a file!");

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);
        if (!System.IO.File.Exists(filePath))
            return NotFound("File is not exist!");

        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return File(stream, "application/octet-stream", fileName);
    }
}