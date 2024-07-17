using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProgiApp.Services.Querys.Calculations;

namespace ProgiApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculationsController : ControllerBase
{
    private readonly IMediator mediator;

    public CalculationsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> CalculatePrice()
    {
        var response = await mediator.Send(new GetCalculatedValuesQuery());
        return Ok(response);
    }
}
