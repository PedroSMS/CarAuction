using Ardalis.Result.AspNetCore;
using CarAuction.Application.Commands.CreateVehicle;
using CarAuction.Application.Queries.GetVehicles;
using CarAuction.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.API.Controllers;

[Route("api/vehicles")]
[ApiController]
public class VehiclesController : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> Get([FromServices] IMediator mediator, [FromQuery] GetVehiclesQueryRequest request)
    {
        var result = await mediator.Send(new GetVehiclesQuery(request));

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Vehicle>> Create([FromServices]IMediator mediator, [FromBody] CreateVehicleCommandRequest request)
    {
        var result = await mediator.Send(request.ToCommand());

        return this.ToActionResult(result);
    }
}