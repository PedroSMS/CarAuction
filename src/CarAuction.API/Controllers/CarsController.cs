using CarAuction.Application.Commands.AddCar;
using CarAuction.Application.Queries.GetCarById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.API.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromServices] IMediator mediator,
            [FromRoute] Guid id)
        {
            var result = await mediator.Send(new GetCarByIdQuery(id));
            
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromServices]IMediator mediator, 
            [FromBody] CreateCarCommandRequest request)
        {
            var result = await mediator.Send(request.ToCommand());

            return Created();
        }
    }
}
