using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgiApp.Domain.Models;
using ProgiApp.Services.Commands.Vehicles;
using ProgiApp.Services.Querys.Vehicles;

namespace ProgiApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly IMediator mediator;

    public VehiclesController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] Vehicle vehicle)
    {
        await mediator.Send(new InsertVehicleCommand(vehicle));
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await mediator.Send(new GetAllVehiclesQuery());
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] int id)
    {
        await mediator.Send(new DeleteVehicleCommand(id));
        return Ok();
    }
}
