﻿using CarAuction.Application.Commands.CreateCar;
using CarAuction.Application.Queries.GetCars;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.API.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get([FromServices] IMediator mediator, [FromQuery] GetCarsQueryRequest request)
        {
            var result = await mediator.Send(new GetCarsQuery(request));

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromServices]IMediator mediator, [FromBody] CreateCarCommandRequest request)
        {
            await mediator.Send(request.ToCommand());

            return Created();
        }
    }
}
